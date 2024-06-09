using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.RestaurantDomain.Mappers;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueBackEnd.Managers.UserDomain.Mappers;
using ResQueDal.ClientDomain;
using ResQueDal.ReviewDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.ClientDomain.Mappers
{
    public class ClientDtoMapper : IClientDtoMapper
    {
        private readonly IUserDtoMapper _userDtoMapper;

        public ClientDtoMapper(IUserDtoMapper userDtoMapper)
        {
            _userDtoMapper = userDtoMapper ??
                throw new ArgumentNullException(nameof(userDtoMapper));
        }

        public Task<Result<ClientDto>> MapToDto(Client entity)
        {
            return Task.FromResult(Result.Ok(new ClientDto(entity.Id,
                entity.UserId,
                entity.User is null ? null : _userDtoMapper.MapToDto(entity.User).Result.Value,
                entity.CompanySize,
                entity.Rating,
                entity.Telephone)));
        }

        public Task<List<ClientDto>> MapToDtos(List<Client> entities)
        {
            var mappedEntities = entities.Select(user => MapToDto(user).Result.Value);

            return Task.FromResult(new List<ClientDto>(mappedEntities.ToList()));
        }

        public Task<Result<Client>> MapToEntity(ClientDto dto)
        {
            return Task.FromResult(Result.Ok(new Client(dto.Id,
                dto.UserId,
                dto.User is null ? null : _userDtoMapper.MapToEntity(dto.User).Result.Value,
                null,
                null,
                null,
                dto.CompanySize,
                dto.Rating,
                dto.Telephone)));
        }
    }
}
