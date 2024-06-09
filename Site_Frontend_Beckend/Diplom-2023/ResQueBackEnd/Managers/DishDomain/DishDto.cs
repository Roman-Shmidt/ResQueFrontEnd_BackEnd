using ResQueBackEnd.Common.Dto;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueDal.MenuDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.DishDomain
{
    public class DishDto : BaseDto, IDto
    {
        public DishDto(int id, 
            int menuId,
            MenuDto menu, 
            string description, 
            string photoUrl, 
            decimal price, 
            string name)
        {
            Id = id;
            MenuId = menuId;
            Menu = menu;
            Description = description;
            PhotoUrl = photoUrl;
            Price = price;
            Name = name;
        }

        public DishDto()
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
