using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ResQueDal.Common.Reading;
using ResQueDal.Common.Reading.Filtering;
using ResQueDal.Common.Reading.Sorting;
using ResQueDal.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResQueDal.Common.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity<int>
    {
        protected readonly Reader<TEntity> Reader;

        public readonly ResQueDbContext DbContext;

        /// <summary> 
        /// Initializes a new instance of the <see cref="Repository{TEntity}"/> class. 
        /// </summary> 
        /// <param name="dbContext">The database context.</param>
        public Repository(ResQueDbContext dbContext,
                Reader<TEntity> reader)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
        }

        /// <summary> 
        /// Gets all Entities. 
        /// </summary> 
        /// <returns>All Entities.</returns> 
        public IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().AsNoTracking();
        }

        /// <summary> 
        /// Gets the database set. 
        /// </summary> 
        /// <returns>Database set.</returns> 
        public DbSet<TEntity> GetDbSet()
        {
            return DbContext.Set<TEntity>();
        }

        /// <summary> 
        /// Gets all Entities with related items. For more information see: https://docs.microsoft.com/en-us/dotnet/api/microsoft.entityframeworkcore.entityframeworkqueryableextensions.include?view=efcore-2.1#Microsoft_EntityFrameworkCore_EntityFrameworkQueryableExtensions_Include__1_System_Linq_IQueryable___0__System_String_ 
        /// </summary> 
        /// <param name="relatedItems">The related items.</param> 
        /// <returns>All Entities with related items.</returns> 
        public virtual IQueryable<TEntity> GetAllWithRelatedItems(IEnumerable<string> relatedItems)
        {
            IQueryable<TEntity> items = GetAll();

            if (relatedItems == null)
            {
                return items;
            }

            foreach (string relatedItem in relatedItems.ToList())
            {
                items = items.Include(relatedItem);
            }

            return items;
        }

        /// <summary> 
        /// Gets the Entity by identifier. 
        /// </summary> 
        /// <param name="id">The identifier.</param> 
        /// <returns>Entity.</returns> 
        public async Task<Maybe<TEntity>> GetById(int id)
        {
            return await DbContext.Set<TEntity>()
                .AsNoTracking()
                .SingleOrDefaultAsync(entity => entity.Id == id);
        }

        /// <summary> 
        /// Gets the Entity by identifier with its related items. 
        /// </summary> 
        /// <param name="id">The identifier.</param> 
        /// <param name="relatedItems">The related items.</param> 
        /// <returns>Entity with related items.</returns> 
        public virtual async Task<Maybe<TEntity>> GetByIdWithRelatedItems(int id,
            IEnumerable<string> relatedItems)
        {
            return await GetAllWithRelatedItems(relatedItems)
                .SingleOrDefaultAsync(entity => entity.Id == id);
        }

        /// <summary> 
        /// Creates the specified Entity. 
        /// </summary> 
        /// <param name="entity">The Entity.</param> 
        /// <returns>Result of creation.</returns> 
        public async Task<Result<TEntity>> Create(TEntity entity)
        {
            EntityEntry<TEntity> entityEntry = await DbContext.Set<TEntity>().AddAsync(entity);

            await DbContext.SaveChangesAsync();

            return Result.Ok(entityEntry.Entity);
        }

        /// <summary> 
        /// Updates the specified Entity. 
        /// </summary> 
        /// <param name="id">The identifier.</param> 
        /// <param name="updatedValues">The updated values.</param> 
        /// <returns>Result of update.</returns> 
        public async Task<Result<TEntity>> UpdateById(int id, Dictionary<string, object> updatedValues)
        {
            TEntity entity = await DbContext.Set<TEntity>().FindAsync(id);

            return await Update(entity, updatedValues);
        }

        /// <summary> 
        /// Deletes the specified Entity. 
        /// </summary> 
        /// <param name="id">The identifier.</param> 
        /// <returns>Result of deletion.</returns> 
        public async Task<Result> DeleteById(int id)
        {
            TEntity entity = await DbContext.Set<TEntity>().FindAsync(id);

            return await Delete(entity);
        }

        public Task<List<TEntity>> GetByIds(HashSet<int> ids)
        {
            return GetAll().Where(x => ids.Contains(x.Id)).ToListAsync();
        }

        public Task<Dictionary<int, TEntity>> GetByIdsAsDictionary(HashSet<int> ids)
        {
            return GetAll().Where(x => ids.Contains(x.Id)).ToDictionaryAsync(x => x.Id);
        }

        public Task<PaginatedList<TEntity>> GetPaginatedItems(
            PaginationOptions paginationOptions,
            IEnumerable<FilteringOptions> multipleFilteringOptions,
            SortingOptions sortingOptions,
            IEnumerable<string> relatedItems)
        {
            return Reader.ApplyOptionsToEntities(GetAllWithRelatedItems(relatedItems).IgnoreQueryFilters(),
                multipleFilteringOptions,
                sortingOptions,
                paginationOptions);
        }

        public async Task<List<TEntity>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions,
            SortingOptions sortingOptions,
            IEnumerable<string> relatedItems)
        {
            IQueryable<TEntity> filteredAndSortedEntitys = await Reader.ApplyFilteringAndSortingToEntities(
                GetAllWithRelatedItems(relatedItems),
                multipleFilteringOptions,
                sortingOptions);

            return filteredAndSortedEntitys.ToList();
        }

        public void AddSortingHandlerForProperty(ISortingHandler<TEntity> sortingHandler)
            => Reader.AddSortingHandlerForProperty(sortingHandler);

        public void AddClientFilteringHandlerForProperty(IFilteringHandler<TEntity> filteringHandler)
            => Reader.AddClientFilteringHandlerForProperty(filteringHandler);

        public void AddServerFilteringHandlerForProperty(IFilteringHandler<TEntity> filteringHandler)
            => Reader.AddServerFilteringHandlerForProperty(filteringHandler);

        public Task<IQueryable<TEntity>> ApplyFilteringAndSortingToEntities(IQueryable<TEntity> entities,
        IEnumerable<FilteringOptions> multipleFilteringOptions,
        SortingOptions sortingOptions)
        => Reader.ApplyFilteringAndSortingToEntities(entities, multipleFilteringOptions, sortingOptions);

        public Task<PaginatedList<TEntity>> ApplyOptionsToEntities(IQueryable<TEntity> entities,
            IEnumerable<FilteringOptions> multipleFilteringOptions,
            SortingOptions sortingOptions,
            PaginationOptions paginationOptions)
            => Reader.ApplyOptionsToEntities(entities, multipleFilteringOptions, sortingOptions, paginationOptions);

        protected async Task<Result<TEntity>> Update(TEntity entity,
            Dictionary<string, object> updatedValues)
        {
            if (entity is null)
            {
                return Result.Fail<TEntity>(RepositoryErrors.CanNotUpdateBecauseEntityIsNotFoundErrorMessage,
                    RepositoryErrors.CanNotUpdateBecauseEntityIsNotFoundErrorKey);
            }

            DbContext.Entry(entity).CurrentValues.SetValues(updatedValues);
            DbContext.Entry(entity).State = EntityState.Modified;

            await DbContext.SaveChangesAsync();

            return Result.Ok(DbContext.Entry(entity).Entity);
        }

        protected async Task<Result> Delete(TEntity entity)
        {
            if (entity is null)
            {
                return Result.Fail<TEntity>(RepositoryErrors.CanNotDeleteBecauseEntityIsNotFoundErrorMessage,
                    RepositoryErrors.CanNotDeleteBecauseEntityIsNotFoundErrorKey);
            }

            DbContext.Set<TEntity>().Remove(entity);

            await DbContext.SaveChangesAsync();

            return Result.Ok();
        }
    }
}
