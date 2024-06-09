using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using ResQueBackEnd.Common.Dto;
using ResQueDal.Common.Repositories;
using ResQueDal.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResQueBackEnd.Common.Mappers;

namespace ResQueBackEnd.Common.Managers
{
    /// <summary> 
    /// Manager which contains business logic. 
    /// </summary> 
    /// <typeparam name="TDto">The type of the dto.</typeparam> 
    /// <typeparam name="TEntity">The type of the entity.</typeparam> 
    public abstract class Manager<TDto, TEntity> : IManager<TDto>
        where TDto : BaseDto
        where TEntity : class, IEntity<int>
    {
        /// <summary> 
        /// The property must contain value for key and not be empty error key. 
        /// </summary> 
        private const string PropertyMustContainValueForKeyAndNotBeEmptyErrorKey =
            "PROPERTY_MUST_CONTAIN_VALUE_FOR_KEY_AND_NOT_BE_EMPTY";

        /// <summary> 
        /// The repository. 
        /// </summary> 
        protected readonly IRepository<TEntity> Repository;

        /// <summary> 
        /// The mapper. 
        /// </summary> 
        protected readonly IDtoMapper<TDto, TEntity> Mapper;

        /// <summary> 
        /// Initializes a new instance of the <see cref="Manager{TDto, TEntity}"/> class. 
        /// </summary> 
        /// <param name="repository">The repository.</param> 
        /// <param name="mapper">The mapper.</param> 
        protected Manager(IRepository<TEntity> repository,
            IDtoMapper<TDto, TEntity> mapper)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary> 
        /// Creates the specified item. 
        /// </summary> 
        /// <param name="dto">The DTO.</param> 
        /// <returns>Result of creation.</returns> 
        public virtual async Task<Result<TDto>> Create(TDto dto)
        {
            Result<TEntity> entityMappingResult = await Mapper.MapToEntity(dto);

            if (entityMappingResult.Failure)
            {
                return Result.Fail<TDto>(entityMappingResult.ErrorMessage, entityMappingResult.ErrorKey);
            }

            Result<TEntity> createResult = await Repository.Create(entityMappingResult.Value);

            if (createResult.Failure)
            {
                return Result.Fail<TDto>(createResult.ErrorMessage, createResult.ErrorKey);
            }

            TEntity entity = createResult.Value;

            Result<TDto> createdDtoMappingResult = await Mapper.MapToDto(entity);

            if (createdDtoMappingResult.Failure)
            {
                return createdDtoMappingResult;
            }

            return Result.Ok(createdDtoMappingResult.Value);
        }

        public virtual async Task<PaginatedList<TDto>> GetPaginatedItems(PaginationOptions paginationOptions,
            IEnumerable<FilteringOptions> multipleFilteringOptions,
            IEnumerable<string> relatedItems,
            SortingOptions sortingOptions)
        {
            PaginatedList<TEntity> entityPaginatedList = await Repository.GetPaginatedItems(paginationOptions,
                multipleFilteringOptions,
                sortingOptions,
                relatedItems);

            List<TDto> dtos = await Mapper.MapToDtos(entityPaginatedList.Items);

            return new PaginatedList<TDto>(dtos,
                entityPaginatedList.PageNumber,
                entityPaginatedList.PageSize,
                entityPaginatedList.TotalItems,
                entityPaginatedList.TotalPageCount);
        }

        public virtual async Task<IEnumerable<TDto>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions,
            IEnumerable<string> relatedItems,
            SortingOptions sortingOptions)
        {
            IReadOnlyList<TEntity> entities = await Repository.GetItems(multipleFilteringOptions,
                sortingOptions,
                relatedItems);

            return await Mapper.MapToDtos(entities.ToList());
        }
    }
}