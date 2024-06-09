using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RoleDomain
{
    public class RoleClaimDto
    {
        public RoleClaimDto(int id,
            int roleId,
            string claimType,
            string claimValue,
            bool isActiveByDefault)
        {
            RoleId = roleId;
            ClaimType = claimType;
            ClaimValue = claimValue;
            IsActiveByDefault = isActiveByDefault;
        }

        public int Id { get; set; }

        public int RoleId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }

        public bool IsActiveByDefault { get; private set; }
    }
}
