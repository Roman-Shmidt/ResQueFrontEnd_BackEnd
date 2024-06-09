namespace ResQueDal.Common;

/// <summary> 
/// Base Entity. 
/// </summary> 
/// <typeparam name="TKey">The type of the key.</typeparam> 
public interface IEntity<TKey>
{
    /// <summary> 
    /// The identifier. 
    /// </summary> 
    TKey Id { get; }
}