using ResQueBackEnd.Managers.DishDomain;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.RestaurantDomain;

namespace ResQue.Controllers.Menus
{
    public class MenuDtoWithPossibleActions
    {
        public MenuDtoWithPossibleActions(MenuDto menuDto)
        {
            Id = menuDto.Id;
            RestaurantId = menuDto.RestaurantId;
            Restaurant = menuDto.Restaurant;
            PhotoUrl = menuDto.PhotoUrl;
            Name = menuDto.Name;
        }

        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public RestaurantDto Restaurant { get; set; }

        public string PhotoUrl { get; set; }

        public string Name { get; set; }

        public DishDto[] Dishes { get; set; }
    }
}
