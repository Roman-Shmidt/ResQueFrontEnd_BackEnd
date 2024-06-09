using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.UserDomain;

namespace ResQue.Infrastructure
{
    public interface IUserDataIdentificationService
    {
        Result<UserData> GetCurrentUserData(HttpContext httpContext);
    }
}
