using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using ResQueDal.Common.Reading.Sorting;
using ResQueDal.Common.Reading.Filtering;

namespace ResQueDal.Common.Reading;

public sealed class Reader<TEntity> : IReader<TEntity>
    where TEntity : class
{
    public const string PropertyGroupName = "property";

    public const string LanguageGroupName = "language";

    private readonly string _propertyInDifferentLanguagesPattern =
        $@"^(?<{PropertyGroupName}>.+)\.(?<{LanguageGroupName}>en|de|it|fr)$";

    private readonly Dictionary<string, Func<SortingData<TEntity>, Task<IQueryable<TEntity>>>> _sortingHandlers = new();
    private readonly Dictionary<string, Func<FilteringData<TEntity>, Task<IQueryable<TEntity>>>> _clientFilteringHandlers = new();
    private readonly Dictionary<string, Func<FilteringData<TEntity>, Task<IQueryable<TEntity>>>> _serverFilteringHandlers = new();

    public void AddSortingHandlerForProperty(ISortingHandler<TEntity> sortingHandler)
    {
        if (string.IsNullOrWhiteSpace(sortingHandler.HandlerFor))
        {
            throw new ArgumentNullException(nameof(sortingHandler.HandlerFor));
        }

        _sortingHandlers.Add(sortingHandler.HandlerFor, sortingHandler.Handle);
    }

    public void AddClientFilteringHandlerForProperty(IFilteringHandler<TEntity> filteringHandler)
    {
        if (string.IsNullOrWhiteSpace(filteringHandler.HandlerFor))
        {
            throw new ArgumentNullException(nameof(filteringHandler.HandlerFor));
        }

        _clientFilteringHandlers.Add(filteringHandler.HandlerFor, filteringHandler.Handle);
    }

    public void AddServerFilteringHandlerForProperty(IFilteringHandler<TEntity> filteringHandler)
    {
        if (string.IsNullOrWhiteSpace(filteringHandler.HandlerFor))
        {
            throw new ArgumentNullException(nameof(filteringHandler.HandlerFor));
        }

        _serverFilteringHandlers.Add(filteringHandler.HandlerFor, filteringHandler.Handle);
    }

    public async Task<PaginatedList<TEntity>> ApplyOptionsToEntities(IQueryable<TEntity> entities,
          IEnumerable<FilteringOptions> multipleFilteringOptions,
          SortingOptions sortingOptions,
          PaginationOptions paginationOptions)
    {
        IQueryable<TEntity> filteredAndSortedModels = await ApplyFilteringAndSortingToEntities(entities,
            multipleFilteringOptions,
            sortingOptions);

        return await ApplyPaginationToEntities(filteredAndSortedModels, paginationOptions);
    }

    public Task<PaginatedList<TEntity>> ApplyPaginationToEntities(IQueryable<TEntity> entities,
        PaginationOptions paginationOptions)
    {
        return PaginatedList<TEntity>.CreatePaginatedList(entities,
            paginationOptions.PageNumber,
            paginationOptions.PageSize);
    }

    public async Task<IQueryable<TEntity>> ApplyFilteringAndSortingToEntities(IQueryable<TEntity> entities,
        IEnumerable<FilteringOptions> multipleFilteringOptions,
        SortingOptions sortingOptions)
    {
        IReadOnlyList<KeyValuePair<string, FilteringOptions>> multipleFilteringOptionsWithPropertyNames =
            multipleFilteringOptions
                .Select(x =>
                    new KeyValuePair<string, FilteringOptions>(ConvertAttributeToPropertyName(x.AttributeName), x))
                .ToList();

        entities = await ApplyServerFiltering(entities,
            multipleFilteringOptionsWithPropertyNames);

        entities = await ApplySorting(entities, sortingOptions);

        entities = await ApplyClientFiltering(entities,
            multipleFilteringOptionsWithPropertyNames);

        return entities;
    }

    public async Task<IQueryable<TEntity>> ApplyClientFiltering(IQueryable<TEntity> entities,
        IEnumerable<KeyValuePair<string, FilteringOptions>> multipleFilteringOptionsWithPropertyNames)
    {
        if (multipleFilteringOptionsWithPropertyNames != null)
        {
            multipleFilteringOptionsWithPropertyNames = multipleFilteringOptionsWithPropertyNames
                .Where(x => _clientFilteringHandlers.Keys.Contains(x.Key))
                .ToList();

            if (!multipleFilteringOptionsWithPropertyNames.Any())
                return entities;

            if (multipleFilteringOptionsWithPropertyNames.Any(x => x.Value.IsEmpty))
                return Enumerable.Empty<TEntity>().AsQueryable();

            entities = (await entities.ToListAsync()).AsQueryable();

            foreach (var filteringOptionsWithPropertyName in multipleFilteringOptionsWithPropertyNames)
            {
                entities = await _clientFilteringHandlers[filteringOptionsWithPropertyName.Key]
                    .Invoke(new FilteringData<TEntity>(entities, filteringOptionsWithPropertyName.Value));
            }
        }

        return entities;
    }

    public async Task<IQueryable<TEntity>> ApplyServerFiltering(IQueryable<TEntity> entities,
        IEnumerable<KeyValuePair<string, FilteringOptions>> multipleFilteringOptionsWithPropertyNames)
    {
        if (multipleFilteringOptionsWithPropertyNames != null)
        {
            foreach (var filteringOptionsWithPropertyName in multipleFilteringOptionsWithPropertyNames)
            {
                if (filteringOptionsWithPropertyName.Value.IsEmpty)
                {
                    return Enumerable.Empty<TEntity>().AsQueryable();
                }

                entities = await Apply(entities, filteringOptionsWithPropertyName);
            }
        }

        return entities;

        Task<IQueryable<TEntity>> Apply(IQueryable<TEntity> entitiesToFilter,
            KeyValuePair<string, FilteringOptions> filteringOptionsWithPropertyName)
        {
            if (_serverFilteringHandlers.TryGetValue(filteringOptionsWithPropertyName.Key, out var serviceSideHandler))
            {
                return serviceSideHandler.Invoke(new FilteringData<TEntity>(entitiesToFilter,
                    filteringOptionsWithPropertyName.Value));
            }

            return Task.FromResult(entitiesToFilter);
        }
    }

    public async Task<IQueryable<TEntity>> ApplySorting(IQueryable<TEntity> entities,
        SortingOptions sortingOptions)
    {
        if (sortingOptions == null ||
            sortingOptions.NoSorting &&
            string.IsNullOrEmpty(sortingOptions.AttributeName))
        {
            return entities;
        }

        string propertyName = ConvertAttributeToPropertyName(sortingOptions.AttributeName);

        if (_sortingHandlers.TryGetValue(propertyName, out var handler)) 
        {
            return await handler.Invoke(new SortingData<TEntity>(entities, sortingOptions));
        }

        return entities;
    }

    private string ConvertAttributeToPropertyName(string attributeName)
    {
        var regex = new Regex(_propertyInDifferentLanguagesPattern);
        var match = regex.Match(attributeName);

        if (match.Success)
        {
            string propertyName = match.Groups[PropertyGroupName].Value;
            return propertyName;
        }

        return attributeName;
    }
}