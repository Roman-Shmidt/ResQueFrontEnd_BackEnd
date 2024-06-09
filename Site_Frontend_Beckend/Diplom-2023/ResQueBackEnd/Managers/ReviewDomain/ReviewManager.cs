using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using ResQueBackEnd.Common.Managers;
using ResQueBackEnd.Common.Mappers;
using ResQueBackEnd.Managers.ReviewDomain.Mappers;
using ResQueDal.Common.Repositories;
using ResQueDal.ReviewDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.ReviewDomain
{
    public class ReviewManager : ManagerForModelWithoutNumber<ReviewDto,Review>, IReviewManager
    {
        public ReviewManager(IReviewRepository repository, IReviewDtoMapper mapper) : base(repository, mapper)
        {
        }

        public override Task<Result<ReviewDto>> Create(ReviewDto dto)
        {
            return base.Create(dto);
        }

        public override Task<Result> DeleteById(int id)
        {
            return base.DeleteById(id);
        }

        public override Task<Maybe<ReviewDto>> GetById(int id, IEnumerable<string> relatedItems)
        {
            return base.GetById(id, relatedItems);
        }

        public override Task<IEnumerable<ReviewDto>> GetByIds(HashSet<int> ids, IEnumerable<string> relatedItems)
        {
            return base.GetByIds(ids, relatedItems);
        }

        public override Task<IEnumerable<ReviewDto>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetItems(multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<PaginatedList<ReviewDto>> GetPaginatedItems(PaginationOptions paginationOptions, IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetPaginatedItems(paginationOptions, multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<Result<ReviewDto>> UpdateById(int id, Dictionary<string, object> updatedValues)
        {
            return base.UpdateById(id, updatedValues);
        }
    }
}
