using ResQueDal.Common.Reading;
using ResQueDal.Common.Reading.Filtering;
using ResQueDal.Common.Repositories;
using ResQueDal.Infrastructure;
using ResQueDal.RestaurantDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.DishDomain
{
    public class DishRepository : Repository<Dish>,
        IDishRepository
    {
        public DishRepository(ResQueDbContext dbContext, 
            Reader<Dish> reader,
            FilteringByIdHandler<Dish> filteringByIdHandler,
            FilteringByMenuIdHandler<Dish> filteringByMenuIdHandler)
            : base(dbContext, reader)
        {
            AddClientFilteringHandlerForProperty(filteringByIdHandler);
            AddClientFilteringHandlerForProperty(filteringByMenuIdHandler);
        }
    }
}
