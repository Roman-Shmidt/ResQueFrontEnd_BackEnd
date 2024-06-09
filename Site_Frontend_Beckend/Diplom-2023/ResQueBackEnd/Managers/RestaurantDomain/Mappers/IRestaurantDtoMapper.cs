using ResQueBackEnd.Common.Mappers;
using ResQueDal.RestaurantDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RestaurantDomain.Mappers
{
    public interface IRestaurantDtoMapper : IDtoMapper<RestaurantDto, Restaurant>
    {
    }
}
