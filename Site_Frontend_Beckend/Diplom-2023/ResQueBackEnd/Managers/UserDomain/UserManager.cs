using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ResQueBackEnd.Common.Dto;
using ResQueBackEnd.Common.Managers;
using ResQueBackEnd.Common.Mappers;
using ResQueBackEnd.Managers.ClientDomain;
using ResQueBackEnd.Managers.RestaurantDomain;
using ResQueBackEnd.Managers.UserDomain.Mappers;
using ResQueBackEnd.Managers.UserDomain.PasswordValidator;
using ResQueDal.Common;
using ResQueDal.Common.Repositories;
using ResQueDal.RoleDomain;
using ResQueDal.UserDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueBackEnd.Managers.UserDomain
{
    public class UserManager : ManagerForModelWithoutNumber<UserDto,User>, IUserManager
    {
        private readonly IPasswordPolicyValidator _passwordPolicyValidator;
        private readonly IUserRepository _userRepository;
        private readonly IRestaurantManager _restaurantManager;
        private readonly IClientManager _clientManager;
        private readonly PasswordHasher<User> _passwordHasher = new();

        public UserManager(IUserRepository repository,
            IUserDtoMapper mapper,
            IPasswordPolicyValidator passwordPolicyValidator,
            IClientManager clientManager,
            IRestaurantManager restaurantManager)
            : base(repository, mapper)
        {
            _userRepository = repository
                ?? throw new ArgumentNullException(nameof(repository));

            _passwordPolicyValidator = passwordPolicyValidator
                ?? throw new ArgumentNullException(nameof(passwordPolicyValidator));

            _clientManager = clientManager
                ?? throw new ArgumentNullException(nameof(clientManager));

            _restaurantManager = restaurantManager
                ?? throw new ArgumentNullException(nameof(restaurantManager));
        }

        public override async Task<Result<UserDto>> Create(UserDto dto)
        {
            string userNumber = await _userRepository.GetNextUserNumber();
            dto.Number = userNumber;
            dto.Id = int.Parse(userNumber);
            Result<User> userCreationResult = await CreateUser(dto);
            if (userCreationResult.Failure)
            {
                return Result.Fail<UserDto>("Creation failed", "Creation failed");
            }

            // After successfully creating user, also create client or restaurant based on user type
            if (dto.UserType == UserType.Client)
            {
                await _clientManager.Create(new ClientDto(default, userCreationResult.Value.Id, null, 1, 5, String.Empty));
            }
            else if (dto.UserType == UserType.Restaurant)
            {
                await _restaurantManager.Create(new RestaurantDto(default, userCreationResult.Value.Id, null, false, false, String.Empty, String.Empty,
                    String.Empty, String.Empty, 5, default, default, default, default, String.Empty));
            }

            Result<UserDto> createdDtoMappingResult = await Mapper.MapToDto(userCreationResult.Value);

            return createdDtoMappingResult.Failure ? createdDtoMappingResult : Result.Ok(createdDtoMappingResult.Value);
        }

        private async Task<Result<User>> CreateUser(UserDto dto)
        {
            Result<User> entityMappingResult = await Mapper.MapToEntity(dto);
            if (entityMappingResult.Failure)
            {
                return Result.Fail<User>("Failed mapping", "Failed mapping");
            }

            User mappedUser = entityMappingResult.Value;

            mappedUser.UserName = mappedUser.Email; //username is not sent by Portal for Portal users but required for login 
            mappedUser.NormalizedUserName = mappedUser.UserName.ToUpperInvariant();

            Result passwordPolicyValidationResult = _passwordPolicyValidator.Validate(dto.Password);

            if (passwordPolicyValidationResult.Failure)
            {
                return Result.Fail<User>("Bad password", "Bad password");
            }

            entityMappingResult.Value.PasswordHash = _passwordHasher.HashPassword(mappedUser, dto.Password);
           

            Result<User> createResult = await Repository.Create(entityMappingResult.Value);
            if (createResult.Failure)
            {
                return Result.Fail<User>(createResult.ErrorMessage, createResult.ErrorKey);
            }

            User entity = createResult.Value;

            return Result.Ok(entity);
        }

        public override Task<Result> DeleteById(int id)
        {
            return base.DeleteById(id);
        }

        public override Task<Maybe<UserDto>> GetById(int id, IEnumerable<string> relatedItems)
        {
            return base.GetById(id, relatedItems);
        }

        public override Task<IEnumerable<UserDto>> GetByIds(HashSet<int> ids, IEnumerable<string> relatedItems)
        {
            return base.GetByIds(ids, relatedItems);
        }

        public override Task<IEnumerable<UserDto>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetItems(multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<PaginatedList<UserDto>> GetPaginatedItems(PaginationOptions paginationOptions, IEnumerable<FilteringOptions> multipleFilteringOptions, IEnumerable<string> relatedItems, SortingOptions sortingOptions)
        {
            return base.GetPaginatedItems(paginationOptions, multipleFilteringOptions, relatedItems, sortingOptions);
        }

        public override Task<Result<UserDto>> UpdateById(int id, Dictionary<string, object> updatedValues)
        {
            return base.UpdateById(id, updatedValues);
        }

        public async Task<Maybe<UserDto>> GetUserByEmail(string email, string password)
        {
            var user = await Repository.GetAll().SingleOrDefaultAsync(x => x.Email == email);

            if (user == null) 
            {
                return Maybe<UserDto>.Empty;
            }

            if (user != null && 
                _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) != PasswordVerificationResult.Success)
            {
                return Maybe<UserDto>.Empty;
            }

            Result<UserDto> dtoMappingResult = await Mapper.MapToDto(user);

            if (dtoMappingResult.Failure)
            {
                return Maybe<UserDto>.Empty;
            }

            return dtoMappingResult.Value;
        }
    }
}
