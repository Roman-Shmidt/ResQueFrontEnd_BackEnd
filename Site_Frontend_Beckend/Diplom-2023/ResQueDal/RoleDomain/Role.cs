using Microsoft.AspNetCore.Identity;
using ResQueDal.Common;
using ResQueDal.UserDomain;
using System.ComponentModel.DataAnnotations;

namespace ResQueDal.RoleDomain;

public sealed class Role : IdentityRole<int>,
    IEntity<int>
{
    public Role(int id,
        string number,
        string name,
        bool mainRole = false)
    {
        Id = id;
        Number = number;
        MainRole = mainRole;
        Name = name;
    }

    private Role()
    {
        // just for EF.
    }

    [Required]
    public string Number { get; private set; }

    /// <summary>
    /// Value indicating whether this is general role for service.
    /// </summary>
    public bool MainRole { get; private set; }

    /// <summary>
    /// The claims.
    /// </summary>
    public ICollection<RoleClaim> Claims { get; private set; } =
        new List<RoleClaim>();

    /// <summary>
    /// The User-Role relationships (many-to-many).
    /// </summary>
    public ICollection<UserRole> UserRoles { get; private set; } =
        new List<UserRole>();
}