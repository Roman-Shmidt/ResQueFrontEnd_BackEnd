using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ResQue.Controllers.Common;
using ResQue.Controllers.Dishes;
using ResQue.Infrastructure;
using ResQueBackEnd.Managers.DishDomain;
using ResQueBackEnd.Managers.MenuDomain;
using System.Reflection.Metadata;

namespace ResQue.Controllers.Dish
{
    [Route("dishes")]
    public class DishController : CustomControllerBase
    {
        private readonly IDishManager _dishManager;
        //private readonly UserData _userData;

        public DishController(IDishManager dishManager)//,
                                                        //IUserDataService userDataService)
        {
            _dishManager = dishManager ?? throw new ArgumentNullException(nameof(dishManager));

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
        /// <param name="dishDto">The User.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        /// <returns>Created User.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ReturnObject<DishDtoWithPossibleActions>>> Create([FromBody] DishDto dishDto)
        {
            Result<DishDto> createResult = await _dishManager.Create(dishDto);

            if (createResult.Success)
            {
                Uri createdUserLocationUri = new Uri(Request.GetDisplayUrl());

                DishDtoWithPossibleActions user = new DishDtoWithPossibleActions(createResult.Value);

                return Created(createdUserLocationUri, new ReturnObject<DishDtoWithPossibleActions>(user, "", ""));
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
        public async Task<ActionResult<ReturnObject<List<DishDtoWithPossibleActions>>>> Get([FromQuery] PaginationOptions paginationOptions,
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
                IEnumerable<DishDto> users = await _dishManager.GetItems(filters,
                    null,
                    sortingOptions);

                List<DishDtoWithPossibleActions> usersWithPossibleActions = users
                    .Select(x => new DishDtoWithPossibleActions(x))
                    .ToList();

                return new ReturnObject<List<DishDtoWithPossibleActions>>(usersWithPossibleActions, "", "");
            }
            else
            {
                PaginatedList<DishDto> paginatedUsers =
                    await _dishManager.GetPaginatedItems(paginationOptions,
                        filters,
                        null,
                        sortingOptions);

                PaginatedList<DishDtoWithPossibleActions> paginatedUsersWithPossibleActions =
                    new PaginatedList<DishDtoWithPossibleActions>(
                        paginatedUsers.Items.Select(x => new DishDtoWithPossibleActions(x)).ToList(),
                        paginatedUsers.PageNumber,
                        paginatedUsers.PageSize,
                        paginatedUsers.TotalItems,
                        paginatedUsers.TotalPageCount);

                return new ReturnObject<List<DishDtoWithPossibleActions>>(paginatedUsersWithPossibleActions.Items, "", "");
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
        public async Task<ActionResult<ReturnObject<DishDtoWithPossibleActions>>> Get([FromRoute] int userId,
            [FromQuery] IEnumerable<string> relatedItems)
        {
            Maybe<DishDto> user = await _dishManager.GetById(userId, relatedItems);

            if (user.HasNoValue)
            {
                return new ReturnObject<DishDtoWithPossibleActions>(null, "failed", "failed");
            }

            DishDtoWithPossibleActions userDto = new DishDtoWithPossibleActions(user.Value);

            return new ReturnObject<DishDtoWithPossibleActions>(userDto, "", "");
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
        public async Task<ActionResult<ReturnObject<DishDtoWithPossibleActions>>> Update([FromRoute] int userId,
            [FromRoute] int tenantId,
            [FromBody] IDictionary<string, object> updatedValues)
        {
            Result<DishDto> updateResult =
                await _dishManager.UpdateById(userId,
                    new Dictionary<string, object>(JsonElementConverter<DishDto>.ConvertUpdatedValues(updatedValues)));

            if (updateResult.Success)
            {
                Uri currentRequestUri = new Uri(Request.GetDisplayUrl());

                return new ReturnObject<DishDtoWithPossibleActions>(new DishDtoWithPossibleActions(updateResult.Value), "", "");
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
            Result deleteResult = await _dishManager
                .DeleteById(userId);

            if (deleteResult.Success)
            {
                return NoContent();
            }

            return CreateErrorActionResult(new Document());
        }
    }
}
