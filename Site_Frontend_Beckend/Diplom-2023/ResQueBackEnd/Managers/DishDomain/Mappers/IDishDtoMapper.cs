using ResQueBackEnd.Common.Mappers;
using ResQueDal.DishDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.DishDomain.Mappers
{
    public interface IDishDtoMapper : IDtoMapper<DishDto, Dish>
    {
    }
}
