using ResQueBackEnd.Common.Mappers;
using ResQueDal.RoleDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RoleDomain.Mappers
{
    public interface IRoleDtoMapper : IDtoMapper<RoleDto, Role>
    {
    }
}
