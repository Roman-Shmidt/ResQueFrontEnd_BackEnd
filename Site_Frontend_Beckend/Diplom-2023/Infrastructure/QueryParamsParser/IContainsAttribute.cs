namespace Infrastructure.QueryParamsParser;

/// <summary>Contains attribute for filtering/sorting.</summary>
public interface IContainsAttribute
{
    /// <summary>Attribute for filtering/sorting.</summary>
    string AttributeName { get; }
}
