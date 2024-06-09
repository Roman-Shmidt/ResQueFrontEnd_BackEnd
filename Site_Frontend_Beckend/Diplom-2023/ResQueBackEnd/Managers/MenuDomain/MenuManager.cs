using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using ResQueBackEnd.Common.Managers;
using ResQueBackEnd.Common.Mappers;
using ResQueBackEnd.Managers.MenuDomain.Mappers;
using ResQueDal.Common.Repositories;
using ResQueDal.MenuDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.MenuDomain
{
    public class MenuManager : ManagerForModelWithoutNumber<MenuDto,Menu>, IMenuManager
    {
        public MenuManager(IMenuRepository repository, IMenuDtoMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<Result<MenuDto>> Create(MenuDto dto)
        {
            return base.Create(dto);
        }

        public override Task<Result> DeleteById(int id)
        {
            return base.DeleteById(id);
        }

        public override Task<Maybe<MenuDto>> GetById(int id, IEnumerable<string> relatedItems)
        {
            return base.GetById(id, relatedItems);
        }

        public override Task<IEnumerable<MenuDto>> GetByIds(HashSet<int> ids, IEnumerable<string> relatedItems)
        {
            return base.GetByIds(ids, relatedItems);
        }

        public override Task<IEnumerable<MenuDto>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetItems(multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<PaginatedList<MenuDto>> GetPaginatedItems(PaginationOptions paginationOptions, IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetPaginatedItems(paginationOptions, multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<Result<MenuDto>> UpdateById(int id, Dictionary<string, object> updatedValues)
        {
            return base.UpdateById(id, updatedValues);
        }
    }
}
