using Infrastructure.FunctionalStyleResult;
using Infrastructure.PaginatedList;
using Infrastructure.QueryParamsParser;
using Microsoft.EntityFrameworkCore;

namespace ResQueDal.Common.Repositories;

/// <summary> 
/// Base CRUD repository. 
/// </summary> 
/// <typeparam name="TEntity">The type of the entity.</typeparam> 
public interface IRepository<TEntity>
    where TEntity : class, IEntity<int>
{
    /// <summary> 
    /// Gets all Entities. 
    /// </summary> 
    /// <returns>All Entities.</returns> 
    IQueryable<TEntity> GetAll();

    /// <summary> 
    /// Gets the database set. 
    /// </summary> 
    /// <returns>Database set.</returns> 
    DbSet<TEntity> GetDbSet();

    /// <summary> 
    /// Gets all Entities with related items. 
    /// </summary> 
    /// <param name="relatedItems">The related items.</param> 
    /// <returns>All Entities with related items.</returns> 
    IQueryable<TEntity> GetAllWithRelatedItems(IEnumerable<string> relatedItems);

    /// <summary> 
    /// Gets the Entity by identifier. 
    /// </summary> 
    /// <param name="id">The identifier.</param> 
    /// <returns>Entity.</returns> 
    Task<Maybe<TEntity>> GetById(int id);

    /// <summary> 
    /// Gets the Entity by identifier with its related items. 
    /// </summary> 
    /// <param name="id">The identifier.</param> 
    /// <param name="relatedItems">The related items.</param> 
    /// <returns>Entity with related items.</returns> 
    Task<Maybe<TEntity>> GetByIdWithRelatedItems(int id, IEnumerable<string> relatedItems);

    Task<List<TEntity>> GetByIds(HashSet<int> ids);

    Task<Dictionary<int, TEntity>> GetByIdsAsDictionary(HashSet<int> ids);

    /// <summary> 
    /// Creates the specified Entity. 
    /// </summary> 
    /// <param name="entity">The Entity.</param> 
    /// <returns>Result of creation.</returns> 
    Task<Result<TEntity>> Create(TEntity entity);

    /// <summary> 
    /// Updates the specified Entity. 
    /// </summary> 
    /// <param name="id">The identifier.</param> 
    /// <param name="updatedValues">The updated values.</param> 
    /// <returns>Result of update.</returns> 
    Task<Result<TEntity>> UpdateById(int id, Dictionary<string, object> updatedValues);

    /// <summary> 
    /// Deletes the specified Entity. 
    /// </summary> 
    /// <param name="id">The identifier.</param> 
    /// <returns>Result of deletion.</returns> 
    Task<Result> DeleteById(int id);

    Task<PaginatedList<TEntity>> GetPaginatedItems(PaginationOptions paginationOptions,
        IEnumerable<FilteringOptions> multipleFilteringOptions,
        SortingOptions sortingOptions,
        IEnumerable<string> relatedItems);

    Task<List<TEntity>> GetItems(IEnumerable<FilteringOptions> multipleFilteringOptions,
        SortingOptions sortingOptions,
        IEnumerable<string> relatedItems);
}