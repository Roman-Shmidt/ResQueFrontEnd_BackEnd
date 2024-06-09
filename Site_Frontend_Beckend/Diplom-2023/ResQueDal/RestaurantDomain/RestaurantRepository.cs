using ResQueDal.Common.Reading;
using ResQueDal.Common.Reading.Filtering;
using ResQueDal.Common.Repositories;
using ResQueDal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.RestaurantDomain
{
    public class RestaurantRepository : Repository<Restaurant>,
        IRestaurantRepository
    {
        public RestaurantRepository(ResQueDbContext dbContext, 
            Reader<Restaurant> reader,
            FilteringByIdHandler<Restaurant> filteringByIdHandler,
            FilteringByUserIdHandler<Restaurant> filteringByUserIdHandler) 
            : base(dbContext, reader)
        {
            AddClientFilteringHandlerForProperty(filteringByIdHandler);
            AddClientFilteringHandlerForProperty(filteringByUserIdHandler);
        }
    }
}
