// <copyright file="PageMetaData.cs" company="Lime-Tec AG"> 
// Copyright (C) Lime-Tec AG, Switzerland - All Rights Reserved. 
// Unauthorized copying of this file, via any medium is strictly prohibited. 
// Proprietary and confidential. 
// </copyright> 

using System.Runtime.Serialization;

namespace Infrastructure.PaginatedList
{
    /// <summary> 
    /// A representation of paging meta data for JSON API 
    /// </summary> 
    [DataContract]
    public sealed class PageMetaData
    {

        /// <summary> 
        /// Initializes a new instance of the <see cref="PageMetaData"/> class. 
        /// </summary> 
        /// <param name="number">The page number.</param> 
        /// <param name="perPage">The items per page value.</param> 
        /// <param name="totalItems">The total items count.</param> 
        /// <param name="totalPages">The total pages count.</param> 
        public PageMetaData(int number, int perPage, int totalItems, int totalPages)
        {
            Number = number;
            PerPage = perPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

        /// <summary> 
        /// Gets the page number. 
        /// </summary> 
        [DataMember]
        public int Number { get; private set; }

        /// <summary> 
        /// Gets the items per page value. 
        /// </summary> 
        [DataMember]
        public int PerPage { get; private set; }

        /// <summary> 
        /// Gets the total items count. 
        /// </summary> 
        [DataMember]
        public int TotalItems { get; private set; }

        /// <summary> 
        /// Gets the total pages count. 
        /// </summary> 
        [DataMember]
        public int TotalPages { get; private set; }
    }
}