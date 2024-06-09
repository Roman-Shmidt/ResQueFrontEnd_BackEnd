using Microsoft.AspNetCore.Identity;

namespace ResQueDal.UserDomain;

/// <summary>
/// Represents an authentication token for a user.
/// </summary>
public sealed class UserToken : IdentityUserToken<int>
{
}
