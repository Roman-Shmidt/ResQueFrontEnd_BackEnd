

namespace ResQueDal.Common.Reading.Filtering;

public sealed class FilteringByIdHandler<TEntity> : IFilteringHandler<TEntity> 
    where TEntity : class, IEntity<int>
{
    public string HandlerFor => "Id";

    public Task<IQueryable<TEntity>> Handle(FilteringData<TEntity> filteringData)
    {
        IQueryable<TEntity> filteredEntities;


        filteredEntities = filteringData.EntitiesToFilter.Where(x => x.Id.ToString() == filteringData.FilteringOptions.Value);


        return Task.FromResult(filteredEntities);
    }
}
