using Infrastructure.QueryParamsParser;
using System; 
using System.Linq; 
 
namespace ResQueDal.Common.Reading.Sorting;

public sealed class SortingData<TEntity>
{
    public SortingData(IQueryable<TEntity> entitiesToSort,
        SortingOptions sortingOptions)
    {
        EntitiesToSort = entitiesToSort ??
            throw new ArgumentNullException(nameof(entitiesToSort));
        SortingOptions = sortingOptions ??
            throw new ArgumentNullException(nameof(sortingOptions));
    }

    public IQueryable<TEntity> EntitiesToSort { get; }

    public SortingOptions SortingOptions { get; }
}