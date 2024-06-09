using Infrastructure.QueryParamsParser;
using System; 
using System.Linq; 
 
namespace ResQueDal.Common.Reading.Filtering;

public sealed class FilteringData<TEntity>
{
    public FilteringData(IQueryable<TEntity> entitiesToFilter,
        FilteringOptions filteringOptions)
    {
        EntitiesToFilter = entitiesToFilter ??
            throw new ArgumentNullException(nameof(entitiesToFilter));
        FilteringOptions = filteringOptions ??
            throw new ArgumentNullException(nameof(filteringOptions));
    }

    public IQueryable<TEntity> EntitiesToFilter { get; }

    public FilteringOptions FilteringOptions { get; }
}