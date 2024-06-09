using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using LimeTec.Corus.ApiGateway.AdministrationService.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ResQue.Controllers.Common;
using ResQue.Infrastructure;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace ResQue.Controllers.Users
{
    /// <summary>
    /// API Controller for users.
    /// </summary>
    [Route("users")]
    public sealed class UsersController : CustomControllerBase
    {
        private readonly IUserManager _userManager;
        //private readonly UserData _userData;

        public UsersController(IUserManager userManager)//,
            //IUserDataService userDataService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));

            //if (userDataService == null)
            //{
            //    throw new ArgumentNullException(nameof(userDataService));
            //}

            //_userData = userDataService.GetCurrentUserData();
        }

        /// <summary>
        /// Creates the specified User.
        /// </summary>
        /// <param name="incomeDocument">The income document.</param>
        /// <param name="userDto">The User.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        /// <returns>Created User.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ReturnObject<UserDtoWithPossibleActions>>> Create([FromBody] UserDto userDto)
        {
            Result<UserDto> createResult = await _userManager.Create(userDto);

            if (createResult.Success)
            {
                Uri createdUserLocationUri = new Uri(Request.GetDisplayUrl());

                UserDtoWithPossibleActions user = new UserDtoWithPossibleActions(createResult.Value);

                return Created(createdUserLocationUri, new ReturnObject<UserDtoWithPossibleActions>(user, "", ""));
            }
            else
            {
                return new ReturnObject<UserDtoWithPossibleActions>(null, createResult.ErrorMessage, createResult.ErrorKey);
            }
        }

        /// <summary>
        /// Gets the Users.
        /// </summary>
        /// <param name="paginationOptions">The pagination options.</param>
        /// <param name="multipleFilteringOptions">The multiple filtering options.</param>
        /// <param name="sortingOptions">The sorting options.</param>
        /// <param name="include">Which resource to include.</param>
        /// <returns>Users.</returns>
        [HttpGet()]
        [ServiceFilter(typeof(ParseRequestParamsAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReturnObject<List<UserDtoWithPossibleActions>>>> Get([FromQuery] PaginationOptions paginationOptions,
           [FromQuery] List<FilteringOptions> multipleFilteringOptions,
           [FromQuery] SortingOptions sortingOptions)
        {
            if (paginationOptions == null)
            {
                IEnumerable<UserDto> users = await _userManager.GetItems(multipleFilteringOptions,
                    null,
                    sortingOptions);

                List<UserDtoWithPossibleActions> usersWithPossibleActions = users
                    .Select(x => new UserDtoWithPossibleActions(x))
                    .ToList();

                return new ReturnObject<List<UserDtoWithPossibleActions>>(usersWithPossibleActions, "", "");
            }
            else
            {
                PaginatedList<UserDto> paginatedUsers =
                    await _userManager.GetPaginatedItems(paginationOptions,
                        multipleFilteringOptions,
                        null,
                        sortingOptions);

                PaginatedList<UserDtoWithPossibleActions> paginatedUsersWithPossibleActions =
                    new PaginatedList<UserDtoWithPossibleActions>(
                        paginatedUsers.Items.Select(x => new UserDtoWithPossibleActions(x)).ToList(),
                        paginatedUsers.PageNumber,
                        paginatedUsers.PageSize,
                        paginatedUsers.TotalItems,
                        paginatedUsers.TotalPageCount);

                return new ReturnObject<List<UserDtoWithPossibleActions>>(paginatedUsersWithPossibleActions.Items, "", "");
            }
        }

        /// <summary>
        /// Gets the User.
        /// </summary>
        /// <param name="userNumber">The User number.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        /// <param name="include">Which resource to include.</param>
        /// <returns>Contract.</returns>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReturnObject<UserDtoWithPossibleActions>>> Get([FromRoute] int userId,
            [FromQuery] IEnumerable<string> relatedItems)
        {
            Maybe<UserDto> user = await _userManager.GetById(userId, relatedItems);

            if (user.HasNoValue)
            {
                return new ReturnObject<UserDtoWithPossibleActions>(null, "failed", "failed");
            }

            UserDtoWithPossibleActions userDto = new UserDtoWithPossibleActions(user.Value);

            return new ReturnObject<UserDtoWithPossibleActions>(userDto, "", "");
        }

        /// <summary>
        /// Updates the specified User.
        /// </summary>
        /// <param name="userNumber">The User number.</param>
        /// <param name="tenantId">The tenant identifier of Role that is requested.</param>
        /// <param name="incomeDocument">The income document.</param>
        /// <param name="updatedValues">The updated values.</param>
        /// <returns>Updated User.</returns>
        [HttpPatch("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReturnObject<UserDtoWithPossibleActions>>> Update([FromRoute] int userId,
            [FromRoute] int tenantId,
            [FromQuery] IDictionary<string, object> updatedValues)
        {
            Result<UserDto> updateResult =
                await _userManager.UpdateById(userId,
                    new Dictionary<string, object>(JsonElementConverter<UserDto>.ConvertUpdatedValues(updatedValues)));

            if (updateResult.Success)
            {
                Uri currentRequestUri = new Uri(Request.GetDisplayUrl());

                return new ReturnObject<UserDtoWithPossibleActions>(new UserDtoWithPossibleActions(updateResult.Value), "", "");
            }
            else
            {
                return CreateErrorActionResult(new Document());
            }
        }

        /// <summary>
        /// Deletes the User.
        /// </summary>
        /// <param name="userNumber">The User number.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([FromRoute] int userId)
        {
            Result deleteResult = await _userManager
                .DeleteById(userId);

            if (deleteResult.Success)
            {
                return NoContent();
            }

            return CreateErrorActionResult(new Document());
        }

        ///// <summary>
        ///// Changes the password.
        ///// </summary>
        ///// <param name="userNumber">The user number.</param>
        ///// <param name="tenantId">The tenant identifier of User that is requested.</param>
        ///// <param name="document">The document.</param>
        ///// <param name="userPassword">The user password.</param>
        ///// <returns>The result of change password operation.</returns>
        //[HttpPatch("tenants/{tenantId}/users/{userNumber}/password")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<ActionResult<Document>> ChangePassword([FromRoute] string userNumber,
        //    [FromRoute] int tenantId,
        //    [FromQuery] Document document,
        //    [FromCustom] UserPasswordDto userPassword)
        //{

        //}
    }
}