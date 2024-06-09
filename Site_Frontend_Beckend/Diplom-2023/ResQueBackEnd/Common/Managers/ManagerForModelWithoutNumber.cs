using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Common.Dto;
using ResQueBackEnd.Common.Mappers;
using ResQueDal.Common.Repositories;
using ResQueDal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ResQueBackEnd.Common.Managers
{
    public abstract class ManagerForModelWithoutNumber<TDto, TEntity> : Manager<TDto, TEntity>,
       IManagerForModelWithoutNumber<TDto>
       where TDto : BaseDto
       where TEntity : class, IEntity<int>
    {
        protected ManagerForModelWithoutNumber(IRepository<TEntity> repository,
            IDtoMapper<TDto, TEntity> mapper)
            : base(repository, mapper)
        {
        }

        public virtual async Task<Maybe<TDto>> GetById(int id, IEnumerable<string> relatedItems)
        {
            Maybe<TEntity> entity = await Repository.GetByIdWithRelatedItems(id, relatedItems);

            if (entity.HasNoValue)
            {
                return null;
            }

            Result<TDto> dtoMappingResult = await Mapper.MapToDto(entity.Value);

            if (dtoMappingResult.Failure)
            {
                return Maybe<TDto>.Empty;
            }

            return dtoMappingResult.Value;
        }

        public virtual async Task<IEnumerable<TDto>> GetByIds(HashSet<int> ids, IEnumerable<string> relatedItems)
        {
            // TODO: In future maybe it is better to use Bulk Read. 
            // TODO: More info: https://github.com/borisdj/EFCore.BulkExtensions#read-example 
            List<TEntity> entities = await Repository
                .GetAllWithRelatedItems(relatedItems)
                .Where(entity => ids.Contains(entity.Id))
                .ToListAsync();

            return await Mapper.MapToDtos(entities);
        }

        public virtual async Task<Result<TDto>> UpdateById(int id, Dictionary<string, object> updatedValues)
        {
            Result<TEntity> updateResult =
                await Repository.UpdateById(id, updatedValues);

            if (updateResult.Failure)
            {
                return Result.Fail<TDto>(updateResult.ErrorMessage, updateResult.ErrorKey);
            }

            Result<TDto> mappingResult = await Mapper.MapToDto(updateResult.Value);

            if (mappingResult.Failure)
            {
                return mappingResult;
            }

            return Result.Ok(mappingResult.Value);
        }

        public virtual Task<Result> DeleteById(int id)
        {
            return Repository.DeleteById(id);
        }
    }
}
