using Infrastructure.FunctionalStyleResult;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.UserDomain.Mappers
{
    public class UserDtoMapper : IUserDtoMapper
    {
        public Task<Result<UserDto>> MapToDto(User entity)
        {
            return Task.FromResult(Result.Ok(new UserDto(entity.Id,
                entity.Number,
                entity.PasswordChanged,
                string.Empty,
                entity.IsActive,
                entity.UserType,
                entity.UserName,
                entity.Email,
                entity.FirstName,
                entity.LastName,
                entity.Language)));
        }

        public Task<List<UserDto>> MapToDtos(List<User> entities)
        {
            var mappedEntities = entities.Select(user => MapToDto(user).Result.Value);

            return Task.FromResult(new List<UserDto>(mappedEntities.ToList()));
        }

        public Task<Result<User>> MapToEntity(UserDto dto)
        {
            return Task.FromResult(Result.Ok(new User(dto.Number,
                dto.PasswordChanged,
                dto.IsActive,
                dto.UserType,
                null,
                dto.UserName,
                dto.Email,
                dto.FirstName,
                dto.LastName,
                null,
                dto.Language)));
        }
    }
}
