using ResQueBackEnd.Common.Dto;
using ResQueBackEnd.Managers.DishDomain;
using ResQueBackEnd.Managers.RestaurantDomain;
using ResQueDal.DishDomain;
using ResQueDal.RestaurantDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.MenuDomain
{
    public class MenuDto : BaseDto, IDto
    {
        public MenuDto(int id, 
            int restaurantId, 
            RestaurantDto restaurant,
            string photoUrl, 
            string name)
        {
            Id = id;
            RestaurantId = restaurantId;
            Restaurant = restaurant;
            PhotoUrl = photoUrl;
            Name = name;
        }

        public int Id { get; set; }

        public int RestaurantId { get; set; }

        public RestaurantDto? Restaurant { get; set; }

        public string PhotoUrl { get; set; }

        public string Name { get; set; }
    }
}
