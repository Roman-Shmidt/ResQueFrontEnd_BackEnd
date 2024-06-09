using Microsoft.AspNetCore.Identity;
using ResQueDal.Common;
using System.ComponentModel.DataAnnotations;

namespace ResQueDal.RoleDomain;

public sealed class RoleClaim : IdentityRoleClaim<int>,
    IEntity<int>
{
    public RoleClaim()
    {
        // just for EF.
    }

    public RoleClaim(int roleId,
        string claimType,
        string claimValue,
        bool isActiveByDefault)
    {
        RoleId = roleId;
        ClaimType = claimType;
        ClaimValue = claimValue;
        IsActiveByDefault = isActiveByDefault;
    }

    /// <summary>
    /// Value indicating whether this instance is active by default.
    /// </summary>
    [Required]
    public bool IsActiveByDefault { get; private set; }
}
