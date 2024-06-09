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

namespace ResQueDal.QueueDomain
{
    public class QueueRepository : Repository<RestaurantQueue>,
        IQueueRepository
    {
        public QueueRepository(ResQueDbContext dbContext, 
            Reader<RestaurantQueue> reader,
            FilteringByIdHandler<RestaurantQueue> filteringByIdHandler,
            FilteringByRestaurantIdHandler<RestaurantQueue> filteringByRestaurantIdHandler,
            FilteringByClientIdHandler<RestaurantQueue> filteringByClientIdHandler)
            : base(dbContext, reader)
        {
            AddClientFilteringHandlerForProperty(filteringByIdHandler);
            AddClientFilteringHandlerForProperty(filteringByRestaurantIdHandler);
            AddClientFilteringHandlerForProperty(filteringByClientIdHandler);
        }
    }
}
