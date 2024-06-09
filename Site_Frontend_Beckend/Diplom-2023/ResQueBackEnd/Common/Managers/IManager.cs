using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using ResQueBackEnd.Common.Dto;
using ResQueDal.Common.Repositories;
using ResQueDal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace ResQueBackEnd.Common.Managers
{
    public interface IManager<TDto>
        where TDto : class, IDto
    {
        Task<Result<TDto>> Create(TDto dto);

        Task<PaginatedList<TDto>> GetPaginatedItems(PaginationOptions paginationOptions,
            IEnumerable<FilteringOptions> multipleFilteringOptions,
            IEnumerable<string> relatedItems,
            SortingOptions sortingOptions);

        Task<IEnumerable<TDto>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions,
            IEnumerable<string> relatedItems,
            SortingOptions sortingOptions);
    }
}