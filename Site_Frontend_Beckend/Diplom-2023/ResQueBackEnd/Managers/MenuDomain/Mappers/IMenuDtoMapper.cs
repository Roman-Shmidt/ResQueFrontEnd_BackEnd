using ResQueBackEnd.Common.Mappers;
using ResQueDal.MenuDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.MenuDomain.Mappers
{
    public interface IMenuDtoMapper : IDtoMapper<MenuDto, Menu>
    {
    }
}
