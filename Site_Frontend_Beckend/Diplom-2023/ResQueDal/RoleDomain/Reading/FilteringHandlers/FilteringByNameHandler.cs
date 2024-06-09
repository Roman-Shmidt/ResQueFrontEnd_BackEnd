using ResQueDal.Common.Reading.Filtering;
using System.Linq;
using System.Threading.Tasks;

namespace ResQueDal.RoleDomain.Reading.FilteringHandlers;

public sealed class FilteringByNameHandler : IFilteringHandler<Role>
{
    public FilteringByNameHandler()
    {

    }

    public string HandlerFor => "Name";

    public Task<IQueryable<Role>> Handle(
        FilteringData<Role> filteringData)
    {
        return Task.FromResult(filteringData.EntitiesToFilter);
    }
}