using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Common.Dto;
using ResQueDal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Common.Mappers
{
    public interface IDtoMapper<TDto, TEntity>
        where TEntity : class, IEntity<int>
        where TDto : class, IDto

    {
        Task<Result<TEntity>> MapToEntity(TDto dto);

        Task<Result<TDto>> MapToDto(TEntity entity);

        Task<List<TDto>> MapToDtos(List<TEntity> entities);
    }
}
