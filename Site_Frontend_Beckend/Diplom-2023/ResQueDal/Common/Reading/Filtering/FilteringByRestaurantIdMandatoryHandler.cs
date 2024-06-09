using ResQueDal.Common.Interfaces;

namespace ResQueDal.Common.Reading.Filtering;

public sealed class FilteringByRestaurantIdMandatoryHandler<TEntity> : IFilteringHandler<TEntity> where TEntity : class, IRestaurantIdNeeded
{
    public string HandlerFor => "RestaurantId";

    public Task<IQueryable<TEntity>> Handle(FilteringData<TEntity> filteringData)
    {
        IQueryable<TEntity> filteredEntities;


        filteredEntities = filteringData.EntitiesToFilter.Where(x => x.RestaurantId.ToString() == filteringData.FilteringOptions.Value);


        return Task.FromResult(filteredEntities);
    }
}
