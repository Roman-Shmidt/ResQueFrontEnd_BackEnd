using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.ClientDomain.Mappers;
using ResQueBackEnd.Managers.RestaurantDomain.Mappers;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueDal.ReservationDomain;
using ResQueDal.ReviewDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.ReservationDomain.Mappers
{
    public class ReservationDtoMapper : IReservationDtoMapper
    {
        private readonly IClientDtoMapper _clientDtoMapper;
        private readonly IRestaurantDtoMapper _restaurantDtoMapper;

        public ReservationDtoMapper(IClientDtoMapper clientDtoMapper,
            IRestaurantDtoMapper restaurantDtoMapper)
        {
            _clientDtoMapper = clientDtoMapper ??
                throw new ArgumentNullException(nameof(clientDtoMapper));

            _restaurantDtoMapper = restaurantDtoMapper ??
                throw new ArgumentNullException(nameof(restaurantDtoMapper));
        }

        public Task<Result<ReservationDto>> MapToDto(Reservation entity)
        {
            return Task.FromResult(Result.Ok(new ReservationDto(entity.Id,
                entity.RestaurantId,
                entity.Restaurant is null ? null : _restaurantDtoMapper.MapToDto(entity.Restaurant).Result.Value,
                entity.ClientId,
                entity.Client is null ? null : _clientDtoMapper.MapToDto(entity.Client).Result.Value,
                entity.Description,
                entity.Name,
                entity.CompanySize,
                entity.DateAndTime,
                entity.IsReservationApproved,
                entity.IsReservationCompleted)));
        }

        public Task<List<ReservationDto>> MapToDtos(List<Reservation> entities)
        {
            var mappedEntities = entities.Select(user => MapToDto(user).Result.Value);

            return Task.FromResult(new List<ReservationDto>(mappedEntities.ToList()));
        }

        public Task<Result<Reservation>> MapToEntity(ReservationDto dto)
        {
            return Task.FromResult(Result.Ok(new Reservation(dto.Id,
                dto.RestaurantId,
                dto.Restaurant is null ? null : _restaurantDtoMapper.MapToEntity(dto.Restaurant).Result.Value,
                dto.ClientId,
                dto.Client is null ? null : _clientDtoMapper.MapToEntity(dto.Client).Result.Value,
                dto.Description,
                dto.Name,
                dto.CompanySize,
                dto.DateAndTime,
                dto.IsReservationApproved,
                dto.IsReservationCompleted)));
        }
    }
}
