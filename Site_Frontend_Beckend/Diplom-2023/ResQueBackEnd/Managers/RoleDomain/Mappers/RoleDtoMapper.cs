using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.UserDomain;
using ResQueDal.RoleDomain;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RoleDomain.Mappers
{
    public class RoleDtoMapper : IRoleDtoMapper
    {
        public Task<Result<RoleDto>> MapToDto(Role entity)
        {
            return Task.FromResult(Result.Ok(new RoleDto(entity.Id,
                entity.Number,
                entity.Name,
                entity.MainRole)));
        }

        public Task<List<RoleDto>> MapToDtos(List<Role> entities)
        {
            var mappedEntities = entities.Select(user => MapToDto(user).Result.Value);

            return Task.FromResult(new List<RoleDto>(mappedEntities.ToList()));
        }

        public Task<Result<Role>> MapToEntity(RoleDto dto)
        {
            return Task.FromResult(Result.Ok(new Role(dto.Id,
                dto.Number,
                dto.Name,
                dto.MainRole)));
        }
    }
}
