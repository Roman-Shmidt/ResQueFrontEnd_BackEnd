using Infrastructure.FunctionalStyleResult;
using System.Security.Claims;

namespace ResQue.Infrastructure
{
    public class UserDataIdentificationService : IUserDataIdentificationService
    {
        public Result<UserData> GetCurrentUserData(HttpContext httpContext)
        {
            return GetUserDataFromItsClaims(httpContext.User);
        }

        private Result<UserData> GetUserDataFromItsClaims(ClaimsPrincipal user)
        {
            return Result.Ok<UserData>(null);
            //Claim userNumberClaim = user.Claims.FirstOrDefault(claim => claim.Type == "sub");

            //if (userNumberClaim == null)
            //{
            //    return Result.Fail<UserData>($"'sub' is not found in User claims");
            //}

            //string userNumber = userNumberClaim.Value;
            //var userData = new UserData(userNumber);

            //bool isResetPasswordOfOtherUserAllowed = user.Claims.Any(c => c.Value.EndsWith(AuthorizationConstants.ResetPasswordOfOtherUserClaim));

            //userData.SetWhetherResetPasswordOfOtherUserAllowed(isResetPasswordOfOtherUserAllowed);

            //if (user.Claims.Any(c => c.Type == AuthorizationConstants.DelegatedByUserNumberRightName) &&
            //    user.Claims.Any(c => c.Type == AuthorizationConstants.DelegatedByUserNameRightName))
            //{
            //    if (!(loginAsOtherUser is null)
            //        && loginAsOtherUser.Value == bool.TrueString)
            //    {
            //        userData.SetLoginAsOtherUser();
            //    }
            //    else
            //    {
            //        return Result.Fail<UserData>($"'{AuthorizationConstants.LoginAsOtherUserOfSameTenantRightName}' or" +
            //                                     $" {AuthorizationConstants.LoginAsOtherUserOfOtherTenantRightName} should be set");
            //    }
            //}

            //return Result.Ok(userData);
        }
    }
}
