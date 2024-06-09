using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.ClientDomain.Mappers;
using ResQueBackEnd.Managers.MenuDomain.Mappers;
using ResQueBackEnd.Managers.RestaurantDomain.Mappers;
using ResQueBackEnd.Managers.ReviewDomain;
using ResQueDal.DishDomain;
using ResQueDal.ReviewDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.DishDomain.Mappers
{
    public class DishDtoMapper : IDishDtoMapper
    {
        private readonly IMenuDtoMapper _menuDtoMapper;

        public DishDtoMapper(IMenuDtoMapper menuDtoMapper)
        {
            _menuDtoMapper = menuDtoMapper ??
                throw new ArgumentNullException(nameof(menuDtoMapper));
        }

        public Task<Result<DishDto>> MapToDto(Dish entity)
        {
            return Task.FromResult(Result.Ok(new DishDto(entity.Id,
                entity.MenuId,
                entity.Menu is null ? null : _menuDtoMapper.MapToDto(entity.Menu).Result.Value,
                entity.Description,
                entity.PhotoUrl,
                entity.Price,
                entity.Name)));
        }

        public Task<List<DishDto>> MapToDtos(List<Dish> entities)
        {
            var mappedEntities = entities.Select(user => MapToDto(user).Result.Value);

            return Task.FromResult(new List<DishDto>(mappedEntities.ToList()));
        }

        public Task<Result<Dish>> MapToEntity(DishDto dto)
        {
            return Task.FromResult(Result.Ok(new Dish(dto.Id,
                dto.MenuId,
                dto.Menu is null ? null : _menuDtoMapper.MapToEntity(dto.Menu).Result.Value,
                dto.Description,
                dto.PhotoUrl,
                dto.Price,
                dto.Name)));
        }
    }
}
