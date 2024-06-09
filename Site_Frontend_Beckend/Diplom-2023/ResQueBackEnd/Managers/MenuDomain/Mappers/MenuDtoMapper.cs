using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.RestaurantDomain.Mappers;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueDal.MenuDomain;
using ResQueDal.ReviewDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.MenuDomain.Mappers
{
    public class MenuDtoMapper : IMenuDtoMapper
    {
        private readonly IRestaurantDtoMapper _restaurantDtoMapper;

        public MenuDtoMapper(IRestaurantDtoMapper restaurantDtoMapper)
        {
            _restaurantDtoMapper = restaurantDtoMapper ??
                throw new ArgumentNullException(nameof(restaurantDtoMapper));
        }

        public Task<Result<MenuDto>> MapToDto(Menu entity)
        {
            return Task.FromResult(Result.Ok(new MenuDto(entity.Id,
                entity.RestaurantId,
                entity.Restaurant is null ? null : _restaurantDtoMapper.MapToDto(entity.Restaurant).Result.Value,
                entity.PhotoUrl,
                entity.Name)));
        }

        public Task<List<MenuDto>> MapToDtos(List<Menu> entities)
        {
            var mappedEntities = entities.Select(user => MapToDto(user).Result.Value);

            return Task.FromResult(new List<MenuDto>(mappedEntities.ToList()));
        }

        public Task<Result<Menu>> MapToEntity(MenuDto dto)
        {
            return Task.FromResult(Result.Ok(new Menu(dto.Id,
                dto.RestaurantId,
                dto.Restaurant is null ? null : _restaurantDtoMapper.MapToEntity(dto.Restaurant).Result.Value,
                null,
                dto.PhotoUrl,
                dto.Name)));
        }
    }
}
