using ResQueDal.Common.Interfaces;

namespace ResQueDal.Common.Reading.Filtering;

public sealed class FilteringByMenuIdHandler<TEntity> : IFilteringHandler<TEntity> where TEntity : class, IMenuId
{
    public string HandlerFor => "MenuId";

    public Task<IQueryable<TEntity>> Handle(FilteringData<TEntity> filteringData)
    {
        IQueryable<TEntity> filteredEntities;


        filteredEntities = filteringData.EntitiesToFilter.Where(x => x.MenuId.ToString() == filteringData.FilteringOptions.Value);


        return Task.FromResult(filteredEntities);
    }
}
