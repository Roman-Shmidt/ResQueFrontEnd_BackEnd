using Infrastructure.QueryParamsParser;
using System.Runtime.Serialization; 
 
namespace Infrastructure.QueryParamsParser;

[DataContract]
public sealed class SortingOptions : IContainsAttribute
{
    [DataMember]
    public string AttributeName { get; set; }

    [DataMember]
    public bool IsDescending { get; set; }

    [DataMember]
    public bool NoSorting { get; set; }

    public static SortingOptions EmptySortingOptions => new SortingOptions
    {
        NoSorting = true
    };
}