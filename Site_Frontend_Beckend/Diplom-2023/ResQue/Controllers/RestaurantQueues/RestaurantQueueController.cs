using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ResQue.Controllers.Common;
using ResQue.Controllers.RestaurantQueues;
using ResQue.Infrastructure;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.RestaurantQueueDomain;
using System.Reflection.Metadata;

namespace ResQue.Controllers.RestaurantQueue
{
    [Route("queues")]
    public class RestaurantQueueController : CustomControllerBase
    {
        private readonly IRestaurantQueueManager _restaurantQueueManager;
        //private readonly UserData _userData;

        public RestaurantQueueController(IRestaurantQueueManager restaurantQueueManager)//,
                                                                                        //IUserDataService userDataService)
        {
            _restaurantQueueManager = restaurantQueueManager ?? throw new ArgumentNullException(nameof(restaurantQueueManager));

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
        /// <param name="restaurantQueueDto">The User.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        /// <returns>Created User.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ReturnObject<RestaurantQueueDtoWithPossibleActions>>> Create([FromBody] RestaurantQueueDto restaurantQueueDto)
        {
            Result<RestaurantQueueDto> createResult = await _restaurantQueueManager.Create(restaurantQueueDto);

            if (createResult.Success)
            {
                Uri createdUserLocationUri = new Uri(Request.GetDisplayUrl());

                RestaurantQueueDtoWithPossibleActions user = new RestaurantQueueDtoWithPossibleActions(createResult.Value);

                return Created(createdUserLocationUri, new ReturnObject<RestaurantQueueDtoWithPossibleActions>(user, "", ""));
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
        public async Task<ActionResult<ReturnObject<List<RestaurantQueueDtoWithPossibleActions>>>> Get([FromQuery] PaginationOptions paginationOptions,
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
                IEnumerable<RestaurantQueueDto> users = await _restaurantQueueManager.GetItems(filters,
                    null,
                    sortingOptions);

                List<RestaurantQueueDtoWithPossibleActions> usersWithPossibleActions = users
                    .Select(x => new RestaurantQueueDtoWithPossibleActions(x))
                    .ToList();

                return new ReturnObject<List<RestaurantQueueDtoWithPossibleActions>>(usersWithPossibleActions, "", "");
            }
            else
            {
                PaginatedList<RestaurantQueueDto> paginatedUsers =
                    await _restaurantQueueManager.GetPaginatedItems(paginationOptions,
                        filters,
                        null,
                        sortingOptions);

                PaginatedList<RestaurantQueueDtoWithPossibleActions> paginatedUsersWithPossibleActions =
                    new PaginatedList<RestaurantQueueDtoWithPossibleActions>(
                        paginatedUsers.Items.Select(x => new RestaurantQueueDtoWithPossibleActions(x)).ToList(),
                        paginatedUsers.PageNumber,
                        paginatedUsers.PageSize,
                        paginatedUsers.TotalItems,
                        paginatedUsers.TotalPageCount);

                return new ReturnObject<List<RestaurantQueueDtoWithPossibleActions>>(paginatedUsersWithPossibleActions.Items, "", "");
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
        public async Task<ActionResult<ReturnObject<RestaurantQueueDtoWithPossibleActions>>> Get([FromRoute] int userId,
            [FromQuery] IEnumerable<string> relatedItems)
        {
            Maybe<RestaurantQueueDto> user = await _restaurantQueueManager.GetById(userId, relatedItems);

            if (user.HasNoValue)
            {
                return new ReturnObject<RestaurantQueueDtoWithPossibleActions>(null, "failed", "failed");
            }

            RestaurantQueueDtoWithPossibleActions userDto = new RestaurantQueueDtoWithPossibleActions(user.Value);

            return new ReturnObject<RestaurantQueueDtoWithPossibleActions>(userDto, "", "");
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
        public async Task<ActionResult<ReturnObject<RestaurantQueueDtoWithPossibleActions>>> Update([FromRoute] int userId,
            [FromRoute] int tenantId,
            [FromBody] IDictionary<string, object> updatedValues)
        {
            Result<RestaurantQueueDto> updateResult =
                await _restaurantQueueManager.UpdateById(userId,
                    new Dictionary<string, object>(JsonElementConverter<RestaurantQueueDto>.ConvertUpdatedValues(updatedValues)));

            if (updateResult.Success)
            {
                Uri currentRequestUri = new Uri(Request.GetDisplayUrl());

                return new ReturnObject<RestaurantQueueDtoWithPossibleActions>(new RestaurantQueueDtoWithPossibleActions(updateResult.Value), "", "");
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
            Result deleteResult = await _restaurantQueueManager
                .DeleteById(userId);

            if (deleteResult.Success)
            {
                return NoContent();
            }

            return CreateErrorActionResult(new Document());
        }
    }
}
