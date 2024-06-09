using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using ResQueBackEnd.Common.Managers;
using ResQueBackEnd.Common.Mappers;
using ResQueBackEnd.Managers.DishDomain.Mappers;
using ResQueDal.Common.Repositories;
using ResQueDal.DishDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.DishDomain
{
    public class DishManager : ManagerForModelWithoutNumber<DishDto,Dish>, IDishManager
    {
        public DishManager(IDishRepository repository, IDishDtoMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<Result<DishDto>> Create(DishDto dto)
        {
            return base.Create(dto);
        }

        public override Task<Result> DeleteById(int id)
        {
            return base.DeleteById(id);
        }

        public override Task<Maybe<DishDto>> GetById(int id, IEnumerable<string> relatedItems)
        {
            return base.GetById(id, relatedItems);
        }

        public override Task<IEnumerable<DishDto>> GetByIds(HashSet<int> ids, IEnumerable<string> relatedItems)
        {
            return base.GetByIds(ids, relatedItems);
        }

        public override Task<IEnumerable<DishDto>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetItems(multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<PaginatedList<DishDto>> GetPaginatedItems(PaginationOptions paginationOptions, IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetPaginatedItems(paginationOptions, multipleFilteringOptions,relatedItems, sortingOptions);
        }

        public override Task<Result<DishDto>> UpdateById(int id, Dictionary<string, object> updatedValues)
        {
            return base.UpdateById(id, updatedValues);
        }
    }
}
