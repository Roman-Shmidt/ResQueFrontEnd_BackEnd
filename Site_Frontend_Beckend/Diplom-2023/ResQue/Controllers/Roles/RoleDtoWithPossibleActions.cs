using ResQueBackEnd.Managers.RoleDomain;
using ResQueBackEnd.Managers.UserDomain;
using ResQueDal.RoleDomain;
using ResQueDal.UserDomain;

namespace ResQue.Controllers.Roles
{
    public class RoleDtoWithPossibleActions
    {
        public RoleDtoWithPossibleActions(RoleDto roleDto)
        {
            Id = roleDto.Id;
            Number = roleDto.Number;
            MainRole = roleDto.MainRole;
            Name = roleDto.Name;
        }

        private RoleDtoWithPossibleActions()
        {

        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Number { get; set; }

        public bool MainRole { get; set; }

        public ICollection<RoleClaimDto> Claims { get; set; } =
            new List<RoleClaimDto>();

        public ICollection<UserRoleDto> UserRoles { get; set; } =
            new List<UserRoleDto>();
    }
}
