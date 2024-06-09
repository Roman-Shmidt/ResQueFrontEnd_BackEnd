using Infrastructure.FunctionalStyleResult;
using ResQueBackEnd.Managers.UserDomain;

namespace ResQue.Infrastructure
{
    public class UserDataService
    {
        private readonly HttpContext _httpContext;
        private readonly IUserDataIdentificationService _userDataIdentificationService;

        /// <summary> 
        /// Initializes a new instance of the <see cref="UserDataService"/> class. 
        /// </summary> 
        /// <param name="httpContextAccessor">The HTTP context accessor.</param> 
        /// <param name="userDataIdentificationService">The user data identification service.</param> 
        public UserDataService(IHttpContextAccessor httpContextAccessor,
            IUserDataIdentificationService userDataIdentificationService)
        {
            _httpContext = httpContextAccessor?.HttpContext ??
                           throw new ArgumentNullException(nameof(httpContextAccessor));

            _userDataIdentificationService = userDataIdentificationService ??
                                           throw new ArgumentNullException(nameof(userDataIdentificationService));
        }

        /// <summary> 
        /// Gets the information about current user. 
        /// </summary> 
        /// <returns>Information about current user.</returns> 
        public UserData GetCurrentUserData()
        {
            Result<UserData> userDataResult = _userDataIdentificationService.GetCurrentUserData(_httpContext);

            return userDataResult.Value;
        }
    }
}
