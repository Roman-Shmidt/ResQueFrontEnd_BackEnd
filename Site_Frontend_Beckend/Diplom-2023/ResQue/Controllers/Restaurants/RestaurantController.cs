using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ResQue.Controllers.Common;
using ResQue.Controllers.Restaurants;
using ResQue.Infrastructure;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.RestaurantDomain;
using System.Reflection.Metadata;

namespace ResQue.Controllers.Restaurant
{
    [Route("restaurants")]
    public class RestaurantController : CustomControllerBase
    {
        private readonly IRestaurantManager _restaurantManager;
        //private readonly UserData _userData;

        public RestaurantController(IRestaurantManager restaurantManager)//,
                                                        //IUserDataService userDataService)
        {
            _restaurantManager = restaurantManager ?? throw new ArgumentNullException(nameof(restaurantManager));

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
        /// <param name="restaurantDto">The User.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        /// <returns>Created User.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ReturnObject<RestaurantDtoWithPossibleActions>>> Create([FromBody] RestaurantDto restaurantDto)
        {
            Result<RestaurantDto> createResult = await _restaurantManager.Create(restaurantDto);

            if (createResult.Success)
            {
                Uri createdUserLocationUri = new Uri(Request.GetDisplayUrl());

                RestaurantDtoWithPossibleActions user = new RestaurantDtoWithPossibleActions(createResult.Value);

                return Created(createdUserLocationUri, new ReturnObject<RestaurantDtoWithPossibleActions>(user, "", ""));
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
        public async Task<ActionResult<ReturnObject<List<RestaurantDtoWithPossibleActions>>>> Get([FromQuery] PaginationOptions paginationOptions,
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
                IEnumerable<RestaurantDto> users = await _restaurantManager.GetItems(filters,
                    null,
                    sortingOptions);

                List<RestaurantDtoWithPossibleActions> usersWithPossibleActions = users
                    .Select(x => new RestaurantDtoWithPossibleActions(x))
                    .ToList();

                return new ReturnObject<List<RestaurantDtoWithPossibleActions>>(usersWithPossibleActions, "", "");
            }
            else
            {
                PaginatedList<RestaurantDto> paginatedUsers =
                    await _restaurantManager.GetPaginatedItems(paginationOptions,
                        filters,
                        null,
                        sortingOptions);

                PaginatedList<RestaurantDtoWithPossibleActions> paginatedUsersWithPossibleActions =
                    new PaginatedList<RestaurantDtoWithPossibleActions>(
                        paginatedUsers.Items.Select(x => new RestaurantDtoWithPossibleActions(x)).ToList(),
                        paginatedUsers.PageNumber,
                        paginatedUsers.PageSize,
                        paginatedUsers.TotalItems,
                        paginatedUsers.TotalPageCount);

                return new ReturnObject<List<RestaurantDtoWithPossibleActions>>(paginatedUsersWithPossibleActions.Items, "", "");
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
        public async Task<ActionResult<ReturnObject<RestaurantDtoWithPossibleActions>>> Get([FromRoute] int userId,
            [FromQuery] IEnumerable<string> relatedItems)
        {
            Maybe<RestaurantDto> user = await _restaurantManager.GetById(userId, relatedItems);

            if (user.HasNoValue)
            {
                return new ReturnObject<RestaurantDtoWithPossibleActions>(null, "failed", "failed");
            }

            RestaurantDtoWithPossibleActions userDto = new RestaurantDtoWithPossibleActions(user.Value);

            return new ReturnObject<RestaurantDtoWithPossibleActions>(userDto, "", "");
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
        public async Task<ActionResult<ReturnObject<RestaurantDtoWithPossibleActions>>> Update([FromRoute] int userId,
            [FromBody] IDictionary<string, object> updatedValues)
        {
            Result<RestaurantDto> updateResult =
                await _restaurantManager.UpdateById(userId,
                    new Dictionary<string, object>(JsonElementConverter<RestaurantDto>.ConvertUpdatedValues(updatedValues)));

            if (updateResult.Success)
            {
                Uri currentRequestUri = new Uri(Request.GetDisplayUrl());

                return new ReturnObject<RestaurantDtoWithPossibleActions>(new RestaurantDtoWithPossibleActions(updateResult.Value), "", "");
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
            Result deleteResult = await _restaurantManager
                .DeleteById(userId);

            if (deleteResult.Success)
            {
                return NoContent();
            }

            return CreateErrorActionResult(new Document());
        }
    }
}
