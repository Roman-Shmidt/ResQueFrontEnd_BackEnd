using ResQueBackEnd.Managers.RoleDomain;
using ResQueDal.RoleDomain;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.UserDomain
{
    public class UserRoleDto
    {
        public UserRoleDto(int userId, 
            int roleId)
        {
            UserId = userId;
            RoleId = roleId;
        }

        public int UserId { get; set; }

        public int RoleId { get; set; }

        public RoleDto Role { get; private set; }

        public UserDto User { get; private set; }
    }
}
