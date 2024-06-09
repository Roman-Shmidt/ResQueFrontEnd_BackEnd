using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Infrastructure.QueryParamsParser;

/// <summary>
/// Contains information for filtering.
/// </summary>
[DataContract]
public sealed class FilteringOptions : IContainsAttribute
{
    [DataMember]
    private Dictionary<string, ComparisonType> _filters =
        new Dictionary<string, ComparisonType>();

    /// <summary>
    /// Initializes a new instance of the <see cref="FilteringOptions"/> class.
    /// </summary>
    /// <param name="attributeName">Name of the attribute.</param>
    public FilteringOptions(string attributeName)
    {
        AttributeName = attributeName;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FilteringOptions"/> class.
    /// </summary>
    /// <param name="attributeName">Name of the attribute.</param>
    /// <param name="value">The value.</param>
    /// <param name="comparisonType">Type of the comparison.</param>
    public FilteringOptions(string attributeName,
        string value,
        ComparisonType comparisonType)
    {
        AttributeName = attributeName;
        AddFilter(value, comparisonType);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="FilteringOptions"/> class.
    /// </summary>
    /// <param name="attributeName">Name of the attribute.</param>
    /// <param name="filters">The filters.</param>
    public FilteringOptions(string attributeName,
        IReadOnlyDictionary<string, ComparisonType> filters)
    {
        AttributeName = attributeName;

        if (filters is null || !filters.Any())
        {
            IsEmpty = true;
        }
        else
        {
            _filters = new Dictionary<string, ComparisonType>(filters);
            IsEmpty = false;
        }
    }

    /// <summary>
    /// Gets a value indicating whether this instance is empty.
    /// </summary>
    [DataMember]
    public bool IsEmpty { get; private set; } = true;

    /// <summary>
    /// The name of the attribute for filtering.
    /// </summary>
    /// TODO: Rename to "PropertyName" and convert it from attribute name to
    /// TODO: property name in this place.
    [DataMember]
    public string AttributeName { get; private set; }

    /// <summary>
    /// The filters (in order to do filtering by same property using OR logical operator)
    /// </summary>
    public IReadOnlyDictionary<string, ComparisonType> Filters => _filters;

    /// <summary>
    /// The value for filtering (default one).
    /// </summary>
    public string Value => _filters.Keys.FirstOrDefault() ?? string.Empty;

    /// <summary>
    /// he type of the comparison (default one).
    /// </summary>
    public ComparisonType ComparisonType => _filters.Values.FirstOrDefault();

    /// <summary>
    /// Adds the filter.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="comparisonType">Type of the comparison.</param>
    public void AddFilter(string value, ComparisonType comparisonType)
    {
        _filters.Add(value, comparisonType);
        IsEmpty = false;
    }
}