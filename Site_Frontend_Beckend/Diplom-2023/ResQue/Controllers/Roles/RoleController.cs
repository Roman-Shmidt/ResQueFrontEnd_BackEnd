using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ResQue.Controllers.Common;
using ResQue.Controllers.Roles;
using ResQue.Infrastructure;
using ResQueBackEnd.Managers.RoleDomain;
using System.Reflection.Metadata;

namespace ResQue.Controllers.Role
{
    [Route("roles")]
    public class RoleController : CustomControllerBase
    {
        private readonly IRoleManager _roleManager;
        //private readonly UserData _userData;

        public RoleController(IRoleManager roleManager)//,
                                                        //IUserDataService userDataService)
        {
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));

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
        public async Task<ActionResult<ReturnObject<RoleDtoWithPossibleActions>>> Create([FromBody] RoleDto roleDto)
        {
            Result<RoleDto> createResult = await _roleManager.Create(roleDto);

            if (createResult.Success)
            {
                Uri createdUserLocationUri = new Uri(Request.GetDisplayUrl());

                RoleDtoWithPossibleActions user = new RoleDtoWithPossibleActions(createResult.Value);

                return Created(createdUserLocationUri, new ReturnObject<RoleDtoWithPossibleActions>(user, "", ""));
            }
            else
            {
                return CreateErrorActionResult(new Document());
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReturnObject<List<RoleDtoWithPossibleActions>>>> Get([FromQuery] PaginationOptions paginationOptions,
           [FromQuery] List<FilteringOptions> multipleFilteringOptions,
           [FromQuery] SortingOptions sortingOptions)
        {
            if (paginationOptions == null)
            {
                IEnumerable<RoleDto> users = await _roleManager.GetItems(multipleFilteringOptions,
                    null,
                    sortingOptions);

                List<RoleDtoWithPossibleActions> usersWithPossibleActions = users
                    .Select(x => new RoleDtoWithPossibleActions(x))
                    .ToList();

                return new ReturnObject<List<RoleDtoWithPossibleActions>>(usersWithPossibleActions, "", "");
            }
            else
            {
                PaginatedList<RoleDto> paginatedUsers =
                    await _roleManager.GetPaginatedItems(paginationOptions,
                        multipleFilteringOptions,
                        null,
                        sortingOptions);

                PaginatedList<RoleDtoWithPossibleActions> paginatedUsersWithPossibleActions =
                    new PaginatedList<RoleDtoWithPossibleActions>(
                        paginatedUsers.Items.Select(x => new RoleDtoWithPossibleActions(x)).ToList(),
                        paginatedUsers.PageNumber,
                        paginatedUsers.PageSize,
                        paginatedUsers.TotalItems,
                        paginatedUsers.TotalPageCount);

                return new ReturnObject<List<RoleDtoWithPossibleActions>>(paginatedUsersWithPossibleActions.Items, "", "");
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
        public async Task<ActionResult<ReturnObject<RoleDtoWithPossibleActions>>> Get([FromRoute] int userId,
            [FromQuery] IEnumerable<string> relatedItems)
        {
            Maybe<RoleDto> user = await _roleManager.GetById(userId, relatedItems);

            if (user.HasNoValue)
            {
                return new ReturnObject<RoleDtoWithPossibleActions>(null, "failed", "failed");
            }

            RoleDtoWithPossibleActions userDto = new RoleDtoWithPossibleActions(user.Value);

            return new ReturnObject<RoleDtoWithPossibleActions>(userDto, "", "");
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
        public async Task<ActionResult<ReturnObject<RoleDtoWithPossibleActions>>> Update([FromRoute] int userId,
            [FromRoute] int tenantId,
            [FromQuery] IDictionary<string, object> updatedValues)
        {
            Result<RoleDto> updateResult =
                await _roleManager.UpdateById(userId,
                    new Dictionary<string, object>(updatedValues));

            if (updateResult.Success)
            {
                Uri currentRequestUri = new Uri(Request.GetDisplayUrl());

                return new ReturnObject<RoleDtoWithPossibleActions>(new RoleDtoWithPossibleActions(updateResult.Value), "", "");
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
            Result deleteResult = await _roleManager
                .DeleteById(userId);

            if (deleteResult.Success)
            {
                return NoContent();
            }

            return CreateErrorActionResult(new Document());
        }
    }
}
