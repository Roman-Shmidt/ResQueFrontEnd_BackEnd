using ResQueDal.Common.Interfaces;

namespace ResQueDal.Common.Reading.Filtering;

public sealed class FilteringByUserIdHandler<TEntity> : IFilteringHandler<TEntity> where TEntity : class, IUserId
{
    public string HandlerFor => "UserId";

    public Task<IQueryable<TEntity>> Handle(FilteringData<TEntity> filteringData)
    {
        IQueryable<TEntity> filteredEntities;


        filteredEntities = filteringData.EntitiesToFilter.Where(x => x.UserId.ToString() == filteringData.FilteringOptions.Value);


        return Task.FromResult(filteredEntities);
    }
}
