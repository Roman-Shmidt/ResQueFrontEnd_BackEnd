using ResQueDal.Common;
using ResQueDal.Common.Interfaces;
using ResQueDal.DishDomain;
using ResQueDal.RestaurantDomain;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.MenuDomain;

public sealed class Menu : IEntity<int>,
    IRestaurantIdNeeded
{
    public Menu(int id, int restaurantId, Restaurant restaurant, ICollection<Dish> dishes, string photoUrl, string name)
    {
        Id = id;
        RestaurantId = restaurantId;
        Restaurant = restaurant;
        Dishes = dishes;
        PhotoUrl = photoUrl;
        Name = name;
    }

    public Menu()
    {
        //Just for EF.
    }

    public int Id { get; set; }

    public int RestaurantId { get; set; }

    public Restaurant Restaurant { get; set; }

    public ICollection<Dish> Dishes { get; private set; } =
        new List<Dish>();

    public string PhotoUrl { get; set; }

    public string Name { get; set; }
}
