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

namespace ResQueDal.MenuDomain
{
    public class MenuRepository : Repository<Menu>,
        IMenuRepository
    {
        public MenuRepository(ResQueDbContext dbContext, 
            Reader<Menu> reader,
            FilteringByIdHandler<Menu> filteringByIdHandler,
            FilteringByRestaurantIdMandatoryHandler<Menu> filteringByRestaurantIdHandler)
            : base(dbContext, reader)
        {
            AddClientFilteringHandlerForProperty(filteringByIdHandler);
            AddClientFilteringHandlerForProperty(filteringByRestaurantIdHandler);
        }
    }
}
