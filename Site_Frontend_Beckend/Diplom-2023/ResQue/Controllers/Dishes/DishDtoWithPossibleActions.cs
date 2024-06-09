using ResQueBackEnd.Managers.DishDomain;
using ResQueBackEnd.Managers.MenuDomain;

namespace ResQue.Controllers.Dishes
{
    public class DishDtoWithPossibleActions
    {
        public DishDtoWithPossibleActions(DishDto dishDto)
        {
            Id = dishDto.Id;
            MenuId = dishDto.MenuId;
            Menu = dishDto.Menu;
            Description = dishDto.Description;
            PhotoUrl = dishDto.PhotoUrl;
            Price = dishDto.Price;
            Name = dishDto.Name;
        }

        public DishDtoWithPossibleActions()
        {

        }

        public int Id { get; set; }

        public int MenuId { get; set; }

        public MenuDto? Menu { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }
    }
}
