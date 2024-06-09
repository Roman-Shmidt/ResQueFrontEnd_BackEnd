using ResQue.Infrastructure;
using ResQueBackEnd.Managers.RoleDomain;
using ResQueBackEnd.Managers.UserDomain;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace LimeTec.Corus.ApiGateway.AdministrationService.Users
{
    /// <summary>
    /// User DTO with possible actions.
    /// </summary>
    public sealed class UserDtoWithPossibleActions : BaseDtoWithPossibleActions
    {
        private List<RoleDto> _roles;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDtoWithPossibleActions"/> class.
        /// </summary>
        /// <param name="userDto">The user DTO.</param>
        public UserDtoWithPossibleActions(UserDto userDto)
        {
            Id = userDto.Id;
            Number = userDto.Number;
            PasswordChanged = userDto.PasswordChanged;
            IsActive = userDto.IsActive;
            UserName = string.IsNullOrEmpty(userDto.UserName) ? userDto.Email : userDto.UserName;
            FirstName = userDto.FirstName;
            LastName = userDto.LastName;
            Language = userDto.Language;
            Email = userDto.Email;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserDtoWithPossibleActions"/> class.
        /// </summary>
        public UserDtoWithPossibleActions()
        {
        }

        public int Id { get; set; }

        /// <summary>
        /// Gets the number.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets the tenant identifier.
        /// </summary>
        public int TenantId { get; set; }

        /// <summary>
        /// Value indicating whether user changed password.
        /// </summary>
        public bool PasswordChanged { get; set; }

        /// <summary>
        /// Value indicating whether this instance is active.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// The email.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// The first name.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The language.
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// The user type.
        /// </summary>
        public string UserType { get; set; }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        public IReadOnlyList<RoleDto> Roles
        {
            get => _roles;
            private set => _roles = value?.ToList();
        }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        public string UniqueId
        {
            get => $"{TenantId}-{Number}";
            private set
            {
            }
        }
    }
}