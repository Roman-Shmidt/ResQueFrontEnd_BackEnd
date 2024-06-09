using Microsoft.EntityFrameworkCore;
using ResQueDal.Common.Reading.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResQueDal.RoleDomain.Reading.SortingHandlers;

public sealed class SortingByNameHandler : ISortingHandler<Role>
{
    public SortingByNameHandler()
    {

    }

    public string HandlerFor => "Name";

    public async Task<IQueryable<Role>> Handle(SortingData<Role> sortingData)
    {
        IReadOnlyList<Role> currencies =
            await sortingData.EntitiesToSort.ToListAsync();

        return currencies.AsQueryable();
    }
}