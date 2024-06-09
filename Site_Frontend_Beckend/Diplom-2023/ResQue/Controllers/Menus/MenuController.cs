using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ResQue.Controllers.Common;
using ResQue.Controllers.Menus;
using ResQue.Infrastructure;
using ResQueBackEnd.Managers.MenuDomain;
using System.Reflection.Metadata;

namespace ResQue.Controllers.Menu
{
    [Route("menus")]
    public class MenuController : CustomControllerBase
    {
        private readonly IMenuManager _menuManager;
        //private readonly UserData _userData;

        public MenuController(IMenuManager menuManager)//,
                                                        //IUserDataService userDataService)
        {
            _menuManager = menuManager ?? throw new ArgumentNullException(nameof(menuManager));

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
        /// <param name="menuDto">The User.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        /// <returns>Created User.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ReturnObject<MenuDtoWithPossibleActions>>> Create([FromBody] MenuDto menuDto)
        {
            Result<MenuDto> createResult = await _menuManager.Create(menuDto);

            if (createResult.Success)
            {
                Uri createdUserLocationUri = new Uri(Request.GetDisplayUrl());

                MenuDtoWithPossibleActions user = new MenuDtoWithPossibleActions(createResult.Value);

                return Created(createdUserLocationUri, new ReturnObject<MenuDtoWithPossibleActions>(user, "", ""));
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
        [ServiceFilter(typeof(ParseRequestParamsAttribute))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReturnObject<List<MenuDtoWithPossibleActions>>>> Get([FromQuery] PaginationOptions paginationOptions,
           [FromQuery] string attributeName,
           [FromQuery] string value,
           [FromQuery] int comparisonType,
           [FromQuery] SortingOptions sortingOptions)
        {
            FilteringOptions filteringOptions = new FilteringOptions(attributeName, value, ComparisonType.Equal);
            List<FilteringOptions> filters = new List<FilteringOptions>();
            filters.Add(filteringOptions);

            if (paginationOptions == null)
            {
                IEnumerable<MenuDto> users = await _menuManager.GetItems(filters,
                    null,
                    sortingOptions);

                List<MenuDtoWithPossibleActions> usersWithPossibleActions = users
                    .Select(x => new MenuDtoWithPossibleActions(x))
                    .ToList();

                return new ReturnObject<List<MenuDtoWithPossibleActions>>(usersWithPossibleActions, "", "");
            }
            else
            {
                PaginatedList<MenuDto> paginatedUsers =
                    await _menuManager.GetPaginatedItems(paginationOptions,
                        filters,
                        null,
                        sortingOptions);

                PaginatedList<MenuDtoWithPossibleActions> paginatedUsersWithPossibleActions =
                    new PaginatedList<MenuDtoWithPossibleActions>(
                        paginatedUsers.Items.Select(x => new MenuDtoWithPossibleActions(x)).ToList(),
                        paginatedUsers.PageNumber,
                        paginatedUsers.PageSize,
                        paginatedUsers.TotalItems,
                        paginatedUsers.TotalPageCount);

                return new ReturnObject<List<MenuDtoWithPossibleActions>>(paginatedUsersWithPossibleActions.Items, "", "");
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
        public async Task<ActionResult<ReturnObject<MenuDtoWithPossibleActions>>> Get([FromRoute] int userId,
            [FromQuery] IEnumerable<string> relatedItems)
        {
            Maybe<MenuDto> user = await _menuManager.GetById(userId, relatedItems);

            if (user.HasNoValue)
            {
                return new ReturnObject<MenuDtoWithPossibleActions>(null, "failed", "failed");
            }

            MenuDtoWithPossibleActions userDto = new MenuDtoWithPossibleActions(user.Value);

            return new ReturnObject<MenuDtoWithPossibleActions>(userDto, "", "");
        }

        /// <summary>
        /// Updates the specified User.
        /// </summary>
        /// <param name="userNumber">The User number.</param>
        /// <param name="tenantId">The tenant identifier of Role that is requested.</param>
        /// <param name="incomeDocument">The income document.</param>
        /// <param name="updatedValues">The updated values.</param>
        /// <returns>Updated User.</returns>
        [HttpPatch("{menuId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ReturnObject<MenuDtoWithPossibleActions>>> Update([FromRoute] int menuId,
            [FromBody] IDictionary<string, object> updatedValues)
        {
            Result<MenuDto> updateResult =
                await _menuManager.UpdateById(menuId,
                    new Dictionary<string, object>(JsonElementConverter<MenuDto>.ConvertUpdatedValues(updatedValues)));

            if (updateResult.Success)
            {
                Uri currentRequestUri = new Uri(Request.GetDisplayUrl());

                return new ReturnObject<MenuDtoWithPossibleActions>(new MenuDtoWithPossibleActions(updateResult.Value), "", "");
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
        [HttpDelete("{number}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<ActionResult> Delete([FromRoute] int number)
        {
            Result deleteResult = await _menuManager
                .DeleteById(number);

            if (deleteResult.Success)
            {
                return NoContent();
            }

            return BadRequest("Видалення не вдалося.");
        }
    }
}
