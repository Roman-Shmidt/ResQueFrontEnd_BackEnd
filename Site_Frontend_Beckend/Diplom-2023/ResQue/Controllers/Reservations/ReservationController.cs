using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ResQue.Controllers.Common;
using ResQue.Controllers.Reservations;
using ResQue.Infrastructure;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.ReservationDomain;
using ResQueDal.ReservationDomain;
using System.Reflection.Metadata;

namespace ResQue.Controllers.Reservation
{
    [Route("reservations")]
    public class ReservationController : CustomControllerBase
    {
        private readonly IReservationManager _reservationManager;
        //private readonly UserData _userData;

        public ReservationController(IReservationManager reservationManager)//,
                                                                            //IUserDataService userDataService)
        {
            _reservationManager = reservationManager ?? throw new ArgumentNullException(nameof(reservationManager));

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
        /// <param name="reservationDto">The User.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        /// <returns>Created User.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ReturnObject<ReservationDtoWithPossibleActions>>> Create([FromBody] ReservationDto reservationDto)
        {
            Result<ReservationDto> createResult = await _reservationManager.Create(reservationDto);

            if (createResult.Success)
            {
                Uri createdUserLocationUri = new Uri(Request.GetDisplayUrl());

                ReservationDtoWithPossibleActions user = new ReservationDtoWithPossibleActions(createResult.Value);

                return Created(createdUserLocationUri, new ReturnObject<ReservationDtoWithPossibleActions>(user, "", ""));
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
        public async Task<ActionResult<ReturnObject<List<ReservationDtoWithPossibleActions>>>> Get([FromQuery] PaginationOptions paginationOptions,
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
                IEnumerable<ReservationDto> users = await _reservationManager.GetItems(filters,
                    null,
                    sortingOptions);

                List<ReservationDtoWithPossibleActions> usersWithPossibleActions = users
                    .Select(x => new ReservationDtoWithPossibleActions(x))
                    .ToList();

                return new ReturnObject<List<ReservationDtoWithPossibleActions>>(usersWithPossibleActions, "", "");
            }
            else
            {
                PaginatedList<ReservationDto> paginatedUsers =
                    await _reservationManager.GetPaginatedItems(paginationOptions,
                        filters,
                        null,
                        sortingOptions);

                PaginatedList<ReservationDtoWithPossibleActions> paginatedUsersWithPossibleActions =
                    new PaginatedList<ReservationDtoWithPossibleActions>(
                        paginatedUsers.Items.Select(x => new ReservationDtoWithPossibleActions(x)).ToList(),
                        paginatedUsers.PageNumber,
                        paginatedUsers.PageSize,
                        paginatedUsers.TotalItems,
                        paginatedUsers.TotalPageCount);

                return new ReturnObject<List<ReservationDtoWithPossibleActions>>(paginatedUsersWithPossibleActions.Items, "", "");
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
        public async Task<ActionResult<ReturnObject<ReservationDtoWithPossibleActions>>> Get([FromRoute] int userId,
            [FromQuery] IEnumerable<string> relatedItems)
        {
            Maybe<ReservationDto> user = await _reservationManager.GetById(userId, relatedItems);

            if (user.HasNoValue)
            {
                return new ReturnObject<ReservationDtoWithPossibleActions>(null, "failed", "failed");
            }

            ReservationDtoWithPossibleActions userDto = new ReservationDtoWithPossibleActions(user.Value);

            return new ReturnObject<ReservationDtoWithPossibleActions>(userDto, "", "");
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
        public async Task<ActionResult<ReturnObject<ReservationDtoWithPossibleActions>>> Update([FromRoute] int userId,
            [FromRoute] int tenantId,
            [FromBody] IDictionary<string, object> updatedValues)
        {
            Result<ReservationDto> updateResult =
                await _reservationManager.UpdateById(userId,
                    new Dictionary<string, object>(JsonElementConverter<ReservationDto>.ConvertUpdatedValues(updatedValues)));

            if (updateResult.Success)
            {
                Uri currentRequestUri = new Uri(Request.GetDisplayUrl());

                return new ReturnObject<ReservationDtoWithPossibleActions>(new ReservationDtoWithPossibleActions(updateResult.Value), "", "");
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
            Result deleteResult = await _reservationManager
                .DeleteById(userId);

            if (deleteResult.Success)
            {
                return NoContent();
            }

            return CreateErrorActionResult(new Document());
        }
    }
}
