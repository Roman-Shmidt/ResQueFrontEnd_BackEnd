using System.Runtime.Serialization;

namespace Infrastructure.QueryParamsParser
{
    /// <summary>
    /// Contains information for pagination.
    /// </summary>
    [DataContract]
    public sealed class PaginationOptions
    {
        /// <summary>
        /// Gets or sets the size of the page.
        /// </summary>
        [DataMember]
        public int PageSize { get; set; }

        /// <summary>
        /// Gets or sets the page number.
        /// </summary>
        [DataMember]
        public int PageNumber { get; set; }

        /// <summary>
        /// Gets the default pagination options.
        /// </summary>
        public static PaginationOptions DefaultPaginationOptions => new PaginationOptions
        {
            PageNumber = 1,
            PageSize = 20
        };

        /// <summary>
        /// Gets the no pagination.
        /// </summary>
        public static PaginationOptions NoPagination => new PaginationOptions
        {
            PageNumber = 0,
            PageSize = 0
        };
    }
}