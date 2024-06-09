using Microsoft.AspNetCore.Identity;
using ResQueDal.Common;
using System.ComponentModel.DataAnnotations;

namespace ResQueDal.UserDomain;

public sealed class User : IdentityUser<int>,
    IEntity<int>
{
    public User(string number,
        bool passwordChanged,
        bool isActive,
        UserType userType,
        string concurrencyStamp = "",
        string userName = "",
        string email = "",
        string firstName = "",
        string lastName = "",
        string securityStamp = "",
        string language = "")
    {
        Number = number;
        UserType = userType;
        PasswordChanged = passwordChanged;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        IsActive = isActive;
        UserName = userName;
        NormalizedUserName = userName?.ToUpperInvariant();
        Language = language;
        SecurityStamp = string.IsNullOrEmpty(securityStamp) ? Guid.NewGuid().ToString() : securityStamp;
        ConcurrencyStamp = string.IsNullOrEmpty(concurrencyStamp) ? Guid.NewGuid().ToString() : concurrencyStamp;

        if (!string.IsNullOrEmpty(email))
        {
            NormalizedEmail = email.ToUpperInvariant();
        }
    }

    private User()
    {
        // just for EF.
    }

    [Required]
    public string Number { get; private set; }

    /// <summary>
    /// The User-Role relationships (many-to-many).
    /// </summary>
    public ICollection<UserRole> UserRoles { get; private set; } =
        new List<UserRole>();

    /// <summary>
    /// Value indicating whether user changed password.
    /// </summary>
    [Required]
    public bool PasswordChanged { get; private set; }

    /// <summary>
    /// Value indicating whether this instance is active.
    /// </summary>
    [Required]
    public bool IsActive { get; private set; }

    /// <summary>
    /// The first name.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Gets or sets the last name.
    /// </summary>
    public string LastName { get; private set; }

    /// <summary>
    /// The language.
    /// </summary>
    public string Language { get; private set; }

    /// <summary>
    /// The user type.
    /// </summary>
    [Required]
    public UserType UserType { get; private set; }
}