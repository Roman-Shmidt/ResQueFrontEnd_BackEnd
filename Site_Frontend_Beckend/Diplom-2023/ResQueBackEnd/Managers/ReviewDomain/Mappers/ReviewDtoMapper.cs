using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.ClientDomain.Mappers;
using ResQueBackEnd.Managers.RestaurantDomain.Mappers;
using ResQueBackEnd.Managers.UserDomain;
using ResQueDal.ReviewDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.ReviewDomain.Mappers
{
    public class ReviewDtoMapper : IReviewDtoMapper
    {
        private readonly IClientDtoMapper _clientDtoMapper;
        private readonly IRestaurantDtoMapper _restaurantDtoMapper;

        public ReviewDtoMapper(IClientDtoMapper clientDtoMapper,
            IRestaurantDtoMapper restaurantDtoMapper) 
        { 
            _clientDtoMapper = clientDtoMapper ??
                throw new ArgumentNullException(nameof(clientDtoMapper));

            _restaurantDtoMapper = restaurantDtoMapper ??
                throw new ArgumentNullException(nameof(restaurantDtoMapper));
        }

        public Task<Result<ReviewDto>> MapToDto(Review entity)
        {
            return Task.FromResult(Result.Ok(new ReviewDto(entity.Id,
                entity.ClientId,
                entity.Client is null ? null : _clientDtoMapper.MapToDto(entity.Client).Result.Value,
                entity.RestaurantId,
                entity.Restaurant is null ? null : _restaurantDtoMapper.MapToDto(entity.Restaurant).Result.Value,
                entity.Rating,
                entity.Description)));
        }

        public Task<List<ReviewDto>> MapToDtos(List<Review> entities)
        {
            var mappedEntities = entities.Select(user => MapToDto(user).Result.Value);

            return Task.FromResult(new List<ReviewDto>(mappedEntities.ToList()));
        }

        public Task<Result<Review>> MapToEntity(ReviewDto dto)
        {
            return Task.FromResult(Result.Ok(new Review(dto.Id,
                dto.ClientId,
                dto.Client is null ? null : _clientDtoMapper.MapToEntity(dto.Client).Result.Value,
                dto.RestaurantId,
                dto.Restaurant is null ? null : _restaurantDtoMapper.MapToEntity(dto.Restaurant).Result.Value,
                dto.Rating,
                dto.Description)));
        }
    }
}
