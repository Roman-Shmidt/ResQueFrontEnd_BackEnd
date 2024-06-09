using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using ResQueBackEnd.Common.Managers;
using ResQueBackEnd.Common.Mappers;
using ResQueBackEnd.Managers.RestaurantDomain.Mappers;
using ResQueDal.Common.Repositories;
using ResQueDal.RestaurantDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RestaurantDomain
{
    public class RestaurantManager : ManagerForModelWithoutNumber<RestaurantDto,Restaurant>, IRestaurantManager
    {
        public RestaurantManager(IRestaurantRepository repository, IRestaurantDtoMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<Result<RestaurantDto>> Create(RestaurantDto dto)
        {
            return base.Create(dto);
        }

        public override Task<Result> DeleteById(int id)
        {
            return base.DeleteById(id);
        }

        public override Task<Maybe<RestaurantDto>> GetById(int id, IEnumerable<string> relatedItems)
        {
            return base.GetById(id, relatedItems);
        }

        public override Task<IEnumerable<RestaurantDto>> GetByIds(HashSet<int> ids, IEnumerable<string> relatedItems)
        {
            return base.GetByIds(ids, relatedItems);
        }

        public override Task<IEnumerable<RestaurantDto>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetItems(multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<PaginatedList<RestaurantDto>> GetPaginatedItems(PaginationOptions paginationOptions, IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetPaginatedItems(paginationOptions, multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<Result<RestaurantDto>> UpdateById(int id, Dictionary<string, object> updatedValues)
        {
            return base.UpdateById(id, updatedValues);
        }
    }
}
