using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.ClientDomain.Mappers;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueBackEnd.Managers.UserDomain.Mappers;
using ResQueDal.RestaurantDomain;
using ResQueDal.ReviewDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RestaurantDomain.Mappers
{
    public class RestaurantDtoMapper : IRestaurantDtoMapper
    {
        private readonly IUserDtoMapper _userDtoMapper;

        public RestaurantDtoMapper(IUserDtoMapper userDtoMapper)
        {
            _userDtoMapper = userDtoMapper ??
                throw new ArgumentNullException(nameof(userDtoMapper));
        }

        public Task<Result<RestaurantDto>> MapToDto(Restaurant entity)
        {
            return Task.FromResult(Result.Ok(new RestaurantDto(entity.Id,
                entity.UserId,
                entity.User is null ? null : _userDtoMapper.MapToDto(entity.User).Result.Value,
                entity.IsQueueOpen,
                entity.IsReservationOpen,
                entity.About,
                entity.Telephone,
                entity.Name,
                entity.Address,
                entity.Rating,
                entity.OpeningTime,
                entity.ClosingTime,
                entity.LongitudeGoogleMap,
                entity.LatitudeGoogleMap,
                entity.Image)));
        }

        public Task<List<RestaurantDto>> MapToDtos(List<Restaurant> entities)
        {
            var mappedEntities = entities.Select(user => MapToDto(user).Result.Value);

            return Task.FromResult(new List<RestaurantDto>(mappedEntities.ToList()));
        }

        public Task<Result<Restaurant>> MapToEntity(RestaurantDto dto)
        {
            return Task.FromResult(Result.Ok(new Restaurant(dto.Id,
                dto.UserId,
                dto.User is null ? null : _userDtoMapper.MapToEntity(dto.User).Result.Value,
                null,
                null,
                null,
                null,
                dto.IsQueueOpen,
                dto.IsReservationOpen,
                dto.About,
                dto.Telephone,
                dto.Name,
                dto.Address,
                dto.Rating,
                dto.OpeningTime,
                dto.ClosingTime,
                dto.LongitudeGoogleMap,
                dto.LatitudeGoogleMap,
                dto.Image)));
        }
    }
}
