using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.ClientDomain.Mappers;
using ResQueBackEnd.Managers.RestaurantDomain.Mappers;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueDal.QueueDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RestaurantQueueDomain.Mappers
{
    public class RestaurantQueueDtoMapper : IRestaurantQueueDtoMapper
    {
        private readonly IClientDtoMapper _clientDtoMapper;
        private readonly IRestaurantDtoMapper _restaurantDtoMapper;

        public RestaurantQueueDtoMapper(IClientDtoMapper clientDtoMapper,
            IRestaurantDtoMapper restaurantDtoMapper)
        {
            _clientDtoMapper = clientDtoMapper ??
                throw new ArgumentNullException(nameof(clientDtoMapper));

            _restaurantDtoMapper = restaurantDtoMapper ??
                throw new ArgumentNullException(nameof(restaurantDtoMapper));
        }

        public Task<Result<RestaurantQueueDto>> MapToDto(RestaurantQueue entity)
        {
            return Task.FromResult(Result.Ok(new RestaurantQueueDto(entity.Id,
                entity.RestaurantId,
                entity.Restaurant is null ? null : _restaurantDtoMapper.MapToDto(entity.Restaurant).Result.Value,
                entity.ClientId,
                entity.Client is null ? null : _clientDtoMapper.MapToDto(entity.Client).Result.Value,
                entity.CompanySize,
                entity.PlaceInQueue,
                entity.EstimatedTime)));
        }

        public Task<List<RestaurantQueueDto>> MapToDtos(List<RestaurantQueue> entities)
        {
            var mappedEntities = entities.Select(user => MapToDto(user).Result.Value);

            return Task.FromResult(new List<RestaurantQueueDto>(mappedEntities.ToList()));
        }

        public Task<Result<RestaurantQueue>> MapToEntity(RestaurantQueueDto dto)
        {
            return Task.FromResult(Result.Ok(new RestaurantQueue(dto.Id,
                dto.RestaurantId,
                dto.Restaurant is null ? null : _restaurantDtoMapper.MapToEntity(dto.Restaurant).Result.Value,
                dto.ClientId,
                dto.Client is null ? null : _clientDtoMapper.MapToEntity(dto.Client).Result.Value,
                dto.CompanySize,
                dto.PlaceInQueue,
                dto.EstimatedTime)));
        }
    }
}
