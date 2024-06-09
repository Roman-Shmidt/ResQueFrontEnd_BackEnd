using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using LimeTec.Corus.ApiGateway.AdministrationService.Users;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using ResQue.Controllers.Clients;
using ResQue.Controllers.Common;
using ResQue.Controllers.Dishes;
using ResQue.Filters.AuthorizationFilter;
using ResQue.Infrastructure;
using ResQueBackEnd.Managers.ClientDomain;
using ResQueBackEnd.Managers.MenuDomain;
using ResQueBackEnd.Managers.UserDomain;
using System.Reflection.Metadata;

namespace ResQue.Controllers.Client
{
    [Route("clients")]
    public class ClientController : CustomControllerBase
    {
        private readonly IClientManager _clientManager;
        //private readonly UserData _userData;

        public ClientController(IClientManager clientManager)//,
                                                             //IUserDataService userDataService)
        {
            _clientManager = clientManager ?? throw new ArgumentNullException(nameof(clientManager));

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
        /// <param name="clientDto">The User.</param>
        /// <param name="tenantId">The tenant identifier of User that is requested.</param>
        /// <returns>Created User.</returns>
        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<ReturnObject<ClientDtoWithPossibleActions>>> Create([FromBody] ClientDto clientDto)
        {
            Result<ClientDto> createResult = await _clientManager.Create(clientDto);

            if (createResult.Success)
            {
                Uri createdUserLocationUri = new Uri(Request.GetDisplayUrl());

                ClientDtoWithPossibleActions user = new ClientDtoWithPossibleActions(createResult.Value);

                return Created(createdUserLocationUri, new ReturnObject<ClientDtoWithPossibleActions>(user, "", ""));
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
        public async Task<ActionResult<ReturnObject<List<ClientDtoWithPossibleActions>>>> Get([FromQuery] PaginationOptions paginationOptions,
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
                IEnumerable<ClientDto> users = await _clientManager.GetItems(filters,
                    null,
                    sortingOptions);

                List<ClientDtoWithPossibleActions> usersWithPossibleActions = users
                    .Select(x => new ClientDtoWithPossibleActions(x))
                    .ToList();

                return new ReturnObject<List<ClientDtoWithPossibleActions>>(usersWithPossibleActions, "", "");
            }
            else
            {
                PaginatedList<ClientDto> paginatedUsers =
                    await _clientManager.GetPaginatedItems(paginationOptions,
                        filters,
                        null,
                        sortingOptions);

                PaginatedList<ClientDtoWithPossibleActions> paginatedUsersWithPossibleActions =
                    new PaginatedList<ClientDtoWithPossibleActions>(
                        paginatedUsers.Items.Select(x => new ClientDtoWithPossibleActions(x)).ToList(),
                        paginatedUsers.PageNumber,
                        paginatedUsers.PageSize,
                        paginatedUsers.TotalItems,
                        paginatedUsers.TotalPageCount);

                return new ReturnObject<List<ClientDtoWithPossibleActions>>(paginatedUsersWithPossibleActions.Items, "", "");
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
        public async Task<ActionResult<ReturnObject<ClientDtoWithPossibleActions>>> Get([FromRoute] int userId,
            [FromQuery] IEnumerable<string> relatedItems)
        {
            Maybe<ClientDto> user = await _clientManager.GetById(userId, relatedItems);

            if (user.HasNoValue)
            {
                return new ReturnObject<ClientDtoWithPossibleActions>(null, "failed", "failed");
            }

            ClientDtoWithPossibleActions userDto = new ClientDtoWithPossibleActions(user.Value);

            return new ReturnObject<ClientDtoWithPossibleActions>(userDto, "", "");
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
        public async Task<ActionResult<ReturnObject<ClientDtoWithPossibleActions>>> Update([FromRoute] int userId,
            [FromRoute] int tenantId,
            [FromQuery] IDictionary<string, object> updatedValues)
        {
            Result<ClientDto> updateResult =
                await _clientManager.UpdateById(userId,
                    new Dictionary<string, object>(JsonElementConverter<ClientDto>.ConvertUpdatedValues(updatedValues)));

            if (updateResult.Success)
            {
                Uri currentRequestUri = new Uri(Request.GetDisplayUrl());

                return new ReturnObject<ClientDtoWithPossibleActions>(new ClientDtoWithPossibleActions(updateResult.Value), "", "");
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
            Result deleteResult = await _clientManager
                .DeleteById(userId);

            if (deleteResult.Success)
            {
                return NoContent();
            }

            return CreateErrorActionResult(new Document());
        }
    }
}
