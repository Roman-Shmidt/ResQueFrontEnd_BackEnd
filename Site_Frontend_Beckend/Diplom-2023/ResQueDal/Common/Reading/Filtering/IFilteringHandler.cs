using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.Common.Reading.Filtering
{
    public interface IFilteringHandler<TEntity>
    {
        string HandlerFor { get; }

        Task<IQueryable<TEntity>> Handle(FilteringData<TEntity> filteringData);
    }
}
