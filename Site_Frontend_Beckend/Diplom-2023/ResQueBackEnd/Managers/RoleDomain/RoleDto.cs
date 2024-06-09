using ResQueBackEnd.Common.Dto;
using ResQueBackEnd.Managers.UserDomain;
using ResQueDal.RoleDomain;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.RoleDomain
{
    public class RoleDto : BaseDto, IDto
    {
        public RoleDto(int id,
        string number,
        string name,
        bool mainRole = false)
        {
            Id = id;
            Number = number;
            MainRole = mainRole;
            Name = name;
        }

        private RoleDto()
        {
            
        }

        public int Id { get; private set; }

        public string Number { get; private set; }

        public bool MainRole { get; private set; }

        public string Name { get; private set; }

        public ICollection<RoleClaimDto> Claims { get; private set; } =
            new List<RoleClaimDto>();

        public ICollection<UserRoleDto> UserRoles { get; private set; } =
            new List<UserRoleDto>();
    }
}
