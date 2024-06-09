using ResQueBackEnd.Common.Managers;
using ResQueBackEnd.Managers.ClientDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RestaurantQueueDomain
{
    public interface IRestaurantQueueManager : IManagerForModelWithoutNumber<RestaurantQueueDto>
    {
    }
}
