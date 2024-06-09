using ResQueDal.Common.Interfaces;

namespace ResQueDal.Common.Reading.Filtering;

public sealed class FilteringByClientIdHandler<TEntity> : IFilteringHandler<TEntity> where TEntity : class, IClientId
{
    public string HandlerFor => "ClientId";

    public Task<IQueryable<TEntity>> Handle(FilteringData<TEntity> filteringData)
    {
        IQueryable<TEntity> filteredEntities;


        filteredEntities = filteringData.EntitiesToFilter.Where(x => x.ClientId.ToString() == filteringData.FilteringOptions.Value);


        return Task.FromResult(filteredEntities);
    }
}
