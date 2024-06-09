using ResQueDal.Common.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.QueueDomain
{
    public interface IQueueRepository : IRepository<RestaurantQueue>
    {
    }
}
