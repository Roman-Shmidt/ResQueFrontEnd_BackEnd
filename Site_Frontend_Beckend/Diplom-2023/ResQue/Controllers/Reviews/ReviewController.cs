using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ResQue.Controllers.Common;
using ResQue.Controllers.Reviews;
using ResQue.Infrastructure;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.ReviewDomain;
using System.Reflection.Metadata;

namespace ResQue.Controllers.Review
{
    [Route("reviews")]
    public class ReviewController : CustomControllerBase
    {
        private readonly IReviewManager _reviewManager;
        //private readonly UserData _userData;

        public ReviewController(IReviewManager reviewManager)//,
                                                             //IUserDataService userDataService)
        {
            _reviewManager = reviewManager ?? throw new ArgumentNullException(nameof(reviewManager));

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
        /// <param name="reviewDto">The User.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        /// <returns>Created User.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ReturnObject<ReviewDtoWithPossibleActions>>> Create([FromBody] ReviewDto reviewDto)
        {
            Result<ReviewDto> createResult = await _reviewManager.Create(reviewDto);

            if (createResult.Success)
            {
                Uri createdUserLocationUri = new Uri(Request.GetDisplayUrl());

                ReviewDtoWithPossibleActions user = new ReviewDtoWithPossibleActions(createResult.Value);

                return Created(createdUserLocationUri, new ReturnObject<ReviewDtoWithPossibleActions>(user, "", ""));
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
        public async Task<ActionResult<ReturnObject<List<ReviewDtoWithPossibleActions>>>> Get([FromQuery] PaginationOptions paginationOptions,
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
                IEnumerable<ReviewDto> users = await _reviewManager.GetItems(filters,
                    null,
                    sortingOptions);

                List<ReviewDtoWithPossibleActions> usersWithPossibleActions = users
                    .Select(x => new ReviewDtoWithPossibleActions(x))
                    .ToList();

                return new ReturnObject<List<ReviewDtoWithPossibleActions>>(usersWithPossibleActions, "", "");
            }
            else
            {
                PaginatedList<ReviewDto> paginatedUsers =
                    await _reviewManager.GetPaginatedItems(paginationOptions,
                        filters,
                        null,
                        sortingOptions);

                PaginatedList<ReviewDtoWithPossibleActions> paginatedUsersWithPossibleActions =
                    new PaginatedList<ReviewDtoWithPossibleActions>(
                        paginatedUsers.Items.Select(x => new ReviewDtoWithPossibleActions(x)).ToList(),
                        paginatedUsers.PageNumber,
                        paginatedUsers.PageSize,
                        paginatedUsers.TotalItems,
                        paginatedUsers.TotalPageCount);

                return new ReturnObject<List<ReviewDtoWithPossibleActions>>(paginatedUsersWithPossibleActions.Items, "", "");
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
        public async Task<ActionResult<ReturnObject<ReviewDtoWithPossibleActions>>> Get([FromRoute] int userId,
            [FromQuery] IEnumerable<string> relatedItems)
        {
            Maybe<ReviewDto> user = await _reviewManager.GetById(userId, relatedItems);

            if (user.HasNoValue)
            {
                return new ReturnObject<ReviewDtoWithPossibleActions>(null, "failed", "failed");
            }

            ReviewDtoWithPossibleActions userDto = new ReviewDtoWithPossibleActions(user.Value);

            return new ReturnObject<ReviewDtoWithPossibleActions>(userDto, "", "");
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
        public async Task<ActionResult<ReturnObject<ReviewDtoWithPossibleActions>>> Update([FromRoute] int userId,
            [FromRoute] int tenantId,
            [FromQuery] IDictionary<string, object> updatedValues)
        {
            Result<ReviewDto> updateResult =
                await _reviewManager.UpdateById(userId,
                    new Dictionary<string, object>(JsonElementConverter<ReviewDto>.ConvertUpdatedValues(updatedValues)));

            if (updateResult.Success)
            {
                Uri currentRequestUri = new Uri(Request.GetDisplayUrl());

                return new ReturnObject<ReviewDtoWithPossibleActions>(new ReviewDtoWithPossibleActions(updateResult.Value), "", "");
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
            Result deleteResult = await _reviewManager
                .DeleteById(userId);

            if (deleteResult.Success)
            {
                return NoContent();
            }

            return CreateErrorActionResult(new Document());
        }
    }
}
