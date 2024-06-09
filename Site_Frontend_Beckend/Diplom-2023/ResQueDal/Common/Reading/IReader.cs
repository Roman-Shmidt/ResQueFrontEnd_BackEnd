using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.EntityFrameworkCore;
using ResQueDal.Common.Reading.Filtering;
using ResQueDal.Common.Reading.Sorting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ResQueDal.Common.Reading
{
    public interface IReader<TEntity>
        where TEntity : class
    {
        void AddSortingHandlerForProperty(ISortingHandler<TEntity> sortingHandler);

        void AddClientFilteringHandlerForProperty(IFilteringHandler<TEntity> filteringHandler);

        void AddServerFilteringHandlerForProperty(IFilteringHandler<TEntity> filteringHandler);

        Task<PaginatedList<TEntity>> ApplyOptionsToEntities(IQueryable<TEntity> entities,
              IEnumerable<FilteringOptions> multipleFilteringOptions,
              SortingOptions sortingOptions,
              PaginationOptions paginationOptions);

        Task<PaginatedList<TEntity>> ApplyPaginationToEntities(IQueryable<TEntity> entities,
            PaginationOptions paginationOptions);

        Task<IQueryable<TEntity>> ApplyFilteringAndSortingToEntities(IQueryable<TEntity> entities,
            IEnumerable<FilteringOptions> multipleFilteringOptions,
            SortingOptions sortingOptions);

        Task<IQueryable<TEntity>> ApplyClientFiltering(IQueryable<TEntity> entities,
            IEnumerable<KeyValuePair<string, FilteringOptions>> multipleFilteringOptionsWithPropertyNames);

        Task<IQueryable<TEntity>> ApplyServerFiltering(IQueryable<TEntity> entities,
            IEnumerable<KeyValuePair<string, FilteringOptions>> multipleFilteringOptionsWithPropertyNames);

        Task<IQueryable<TEntity>> ApplySorting(IQueryable<TEntity> entities,
            SortingOptions sortingOptions);
    }
}
