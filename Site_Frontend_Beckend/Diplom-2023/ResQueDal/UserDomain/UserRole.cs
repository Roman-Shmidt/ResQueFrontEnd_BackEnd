using Microsoft.AspNetCore.Identity;
using ResQueDal.RoleDomain;

namespace ResQueDal.UserDomain;

public sealed class UserRole : IdentityUserRole<int>
{
    public UserRole(int userId, int roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }

    public UserRole()
    {
        // just for EF.
    }

    /// <summary>
    /// Gets or sets the role.
    /// </summary>
    public Role Role { get; private set; }

    /// <summary>
    /// Gets or sets the user.
    /// </summary>
    public User User { get; private set; }
}
