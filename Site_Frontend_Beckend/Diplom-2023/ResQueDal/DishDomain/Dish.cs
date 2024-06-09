using ResQueDal.Common;
using ResQueDal.Common.Interfaces;
using ResQueDal.MenuDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.DishDomain
{
    public sealed class Dish : IEntity<int>,
        IMenuId
    {
        public Dish(int id, int menuId, Menu menu, string description, string photoUrl, decimal price, string name)
        {
            Id = id;
            MenuId = menuId;
            Menu = menu;
            Description = description;
            PhotoUrl = photoUrl;
            Price = price;
            Name = name;
        }

        public Dish() 
        { 
            //Just for EF.
        }

        public int Id { get; set; }

        public int MenuId { get; set; }

        public Menu Menu { get; set; }

        public string Description { get; set; }

        public string PhotoUrl { get; set; }

        public decimal Price { get; set; }

        public string Name { get; set; }
    }
}
