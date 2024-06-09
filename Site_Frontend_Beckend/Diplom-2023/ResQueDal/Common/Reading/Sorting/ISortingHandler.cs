using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.Common.Reading.Sorting
{
    public interface ISortingHandler<TEntity>
    {
        string HandlerFor { get; }

        Task<IQueryable<TEntity>> Handle(SortingData<TEntity> sortingData);
    }
}
