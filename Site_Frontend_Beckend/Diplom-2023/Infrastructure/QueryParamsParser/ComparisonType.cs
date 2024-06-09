namespace Infrastructure.QueryParamsParser;

/// <summary>
/// Contains type of comparison for filtering.
/// </summary>
public enum ComparisonType
{
    Equal,
    NotEqual,
    GreaterThanOrEqual,
    GreaterThan,
    LessThanOrEqual,
    LessThan,
    Like
}