using ResQueBackEnd.Common.Mappers;
using ResQueDal.QueueDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RestaurantQueueDomain.Mappers
{
    public interface IRestaurantQueueDtoMapper : IDtoMapper<RestaurantQueueDto, RestaurantQueue>
    {
    }
}
