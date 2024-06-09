using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Common.Dto;
using ResQueDal.Common.Repositories;
using ResQueDal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Common.Managers
{
    public interface IManagerForModelWithoutNumber<TDto> : IManager<TDto>
        where TDto : class, IDto
    {
        Task<Maybe<TDto>> GetById(int id, IEnumerable<string> relatedItems);

        Task<IEnumerable<TDto>> GetByIds(HashSet<int> ids, IEnumerable<string> relatedItems);

        Task<Result<TDto>> UpdateById(int id, Dictionary<string, object> updatedValues);

        Task<Result> DeleteById(int id);
    }
}
