using ResQueBackEnd.Common.Dto;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.UserDomain
{
    [DataContract]
    public class UserDto : BaseDto, IDto
    {
        public UserDto(int id,
        string number,
        bool passwordChanged,
        string password,
        bool isActive,
        UserType userType,
        string userName = "",
        string email = "",
        string firstName = "",
        string lastName = "",
        string language = "")
        {
            Id = id;
            Number = number;
            UserType = userType;
            PasswordChanged = passwordChanged;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            IsActive = isActive;
            UserName = userName;
            Language = language;
        }

        public UserDto()
        {

        }

        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Number { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// The User-Role relationships (many-to-many).
        /// </summary>
        [DataMember]
        public ICollection<UserRole> UserRoles { get; set; } =
            new List<UserRole>();

        /// <summary>
        /// Value indicating whether user changed password.
        /// </summary>
        [Required]
        [DataMember]
        public bool PasswordChanged { get; set; }

        /// <summary>
        /// The user password.
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// Value indicating whether this instance is active.
        /// </summary>
        [Required]
        [DataMember]
        public bool IsActive { get; set; }

        /// <summary>
        /// The first name.
        /// </summary>
        [DataMember]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [DataMember]
        public string LastName { get; set; }

        /// <summary>
        /// The language.
        /// </summary>
        [DataMember]
        public string Language { get; set; }

        /// <summary>
        /// The user type.
        /// </summary>
        [Required]
        [DataMember]
        public UserType UserType { get; set; }
    }
}
