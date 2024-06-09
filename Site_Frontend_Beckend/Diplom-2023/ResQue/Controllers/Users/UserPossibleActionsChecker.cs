//using System;
//using System.Linq;
//using System.Threading.Tasks;

//namespace LimeTec.Corus.ApiGateway.AdministrationService.Users
//{
//    /// <summary>
//    /// Contains methods for determination of possible actions for User taking into consideration
//    /// user rights.
//    /// </summary>
//    public sealed class UserPossibleActionsChecker : PossibleCrudActionsCheckerBase,
//        IUserPossibleActionsChecker
//    {
//        private readonly UserData _userData;

//        public UserPossibleActionsChecker(IUserDataService userDataService,
//            UserRights userRights)
//            : base(userRights,
//                new ServiceNamePath(AuthorizationConstants.AdministrationServiceRightName,
//                AuthorizationConstants.UserServiceRightName))
//        {
//            if (userDataService is null)
//            {
//                throw new ArgumentNullException(nameof(userDataService));
//            }

//            _userData = userDataService.GetCurrentUserData();
//        }

//        /// <summary>
//        /// Determines whether delete User is possible.
//        /// </summary>
//        /// <param name="user">The user.</param>
//        /// <returns>Result of verification.</returns>
//        public Task<bool> IsDeletePossible(UserDtoWithPossibleActions user)
//        {
//            if (UserManager.CheckIfUserIsSuperAdmin(user.Number, new TenantId(user.TenantId)) || UserManager.IsOwnUser(user.Number, new TenantId(user.TenantId), _userData))
//                return Task.FromResult(false);

//            return Task.FromResult(user.TenantId == _userData.TenantId
//                ? UserRightsInParticularService.ContainsRight(AuthorizationConstants.ClaimDeleteFromOwnTenantValue)
//                : UserRightsInParticularService.ContainsRight(AuthorizationConstants.ClaimDeleteFromOtherTenantValue));
//        }

//        public Task<bool> IsUpdatePossible(UserDtoWithPossibleActions user)
//        {
//            if (UserManager.IsOwnUser(user.Number, new TenantId(user.TenantId), _userData))
//                return Task.FromResult(UserRightsInParticularService.ContainsRight(AuthorizationConstants.ClaimUpdateOwnUserValue));

//            return Task.FromResult(user.TenantId == _userData.TenantId
//                ? UserRightsInParticularService.ContainsRight(AuthorizationConstants.ClaimUpdateOfOwnTenantValue)
//                : UserRightsInParticularService.ContainsRight(AuthorizationConstants.ClaimUpdateOfOtherTenantValue));
//        }
//    }
//}