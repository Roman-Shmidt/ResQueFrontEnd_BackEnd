using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Serialization; 
using System.Threading.Tasks; 
 
namespace Infrastructure.PaginatedList
{
    /// <summary> 
    /// Provides list of items and pagination information. 
    /// </summary> 
    [DataContract]
    public sealed class PaginatedList<T>
        where T : class
    {
        public static PaginatedList<T> Empty = new PaginatedList<T>(Enumerable.Empty<T>(), 0, 0, 0, 0);

        /// <summary> 
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class. 
        /// </summary> 
        /// <param name="items">The items.</param> 
        /// <param name="pageNumber">The page number.</param> 
        /// <param name="pageSize">Size of the page.</param> 
        public PaginatedList(IQueryable<T> items, int pageNumber, int pageSize)
        {
            ValidateParameters(items, pageNumber, pageSize);

            PageSize = pageSize;
            PageNumber = pageNumber;

            if (items.Any())
            {
                TotalItems = items.Count();
                TotalPageCount = pageSize > 0
                    ? (int)Math.Ceiling(TotalItems / (double)pageSize)
                    : 1; // if we want to receive all items (pageSize = 0 && pageNumber = 0), then there will be only 1 page. 

                if (pageNumber == 0 && pageSize == 0)
                {
                    // when pageNumber and pageSize are both equal to 0 than we need to return all items. 
                    Items.AddRange(items);
                }
                else
                {
                    IQueryable<T> itemsForPage = items
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize);

                    Items.AddRange(itemsForPage);
                }

                PagingMetaData = new PageMetaData(pageNumber, pageSize, TotalItems, TotalPageCount);
            }
        }

        public PaginatedList(IEnumerable<T> items, int pageNumber, int pageSize)
            : this(items.AsQueryable(), pageNumber, pageSize)
        {
        }

        /// <summary> 
        /// Initializes a new instance of the <see cref="PaginatedList{T}"/> class. 
        /// </summary> 
        /// <param name="itemsForPage">The items for page.</param> 
        /// <param name="pageNumber">The page number.</param> 
        /// <param name="pageSize">Size of the page.</param> 
        /// <param name="totalItems">The total items.</param> 
        /// <param name="totalPageCount">The total page count.</param> 
        public PaginatedList(IEnumerable<T> itemsForPage, int pageNumber, int pageSize, int totalItems, int totalPageCount)
        {
            ValidateParameters(itemsForPage, pageNumber, pageSize);

            if (totalPageCount < 0)
            {
                throw new BadRequestException($"{nameof(totalPageCount)} should be greater or equal to zero");
            }

            if (totalItems < 0)
            {
                throw new BadRequestException($"{nameof(totalItems)} should be greater or equal to zero");
            }

            Items.AddRange(itemsForPage);
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalPageCount = totalPageCount;
            TotalItems = totalItems;
            PagingMetaData = new PageMetaData(pageNumber, pageSize, totalItems, totalPageCount);
        }

        private PaginatedList()
        {
        }

        /// <summary> 
        /// Gets the items. 
        /// </summary> 
        [DataMember]
        public List<T> Items { get; private set; } = new List<T>();

        /// <summary> 
        /// Gets the paging meta data. 
        /// </summary> 
        [DataMember]
        public PageMetaData PagingMetaData { get; private set; }

        /// <summary> 
        /// Gets the page number. 
        /// </summary> 
        [DataMember]
        public int PageNumber { get; private set; }

        /// <summary> 
        /// Gets the page size. 
        /// </summary> 
        [DataMember]
        public int PageSize { get; private set; }

        /// <summary> 
        /// Gets the total page count. 
        /// </summary> 
        [DataMember]
        public int TotalPageCount { get; private set; }

        /// <summary> 
        /// Gets the total items. 
        /// </summary> 
        [DataMember]
        public int TotalItems { get; private set; }

        /// <summary> 
        /// Gets a value indicating whether this instance has previous page. 
        /// </summary> 
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary> 
        /// Gets a value indicating whether this instance has next page. 
        /// </summary> 
        public bool HasNextPage => PageNumber < TotalPageCount;

        /// <summary> 
        /// Creates the paginated list. 
        /// NOTE: This method can be used only for real IQueryable Providers (EF, etc). 
        /// </summary> 
        /// <param name="items">The items.</param> 
        /// <param name="pageNumber">The page number.</param> 
        /// <param name="pageSize">Size of the page.</param> 
        /// <returns>Paginated list.</returns> 
        public static async Task<PaginatedList<T>> CreatePaginatedList(IQueryable<T> items, int pageNumber, int pageSize)
        {
            ValidateParameters(items, pageNumber, pageSize);

            bool isAsyncEnumerable = items is IAsyncEnumerable<T>;

            var paginatedList = new PaginatedList<T> { PageSize = pageSize, PageNumber = pageNumber };

            paginatedList.TotalItems = isAsyncEnumerable ? await items.CountAsync() : items.Count();

            paginatedList.TotalPageCount = pageSize > 0
                ? (int)Math.Ceiling(paginatedList.TotalItems / (double)pageSize)
                : 1;

            if (pageNumber == 0 && pageSize == 0)
            {
                // when pageNumber and pageSize are both equal to 0 than we need to return all items. 
                paginatedList.Items.AddRange(isAsyncEnumerable ? await items.ToListAsync() : items.ToList());
            }
            else
            {
                IQueryable<T> itemsForPage = items
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize);

                paginatedList.Items.AddRange(isAsyncEnumerable
                    ? await itemsForPage.ToListAsync()
                    : itemsForPage.ToList());
            }

            paginatedList.PagingMetaData = new PageMetaData(pageNumber,
                pageSize,
                paginatedList.TotalItems,
                paginatedList.TotalPageCount);

            return paginatedList;
        }

        private static void ValidateParameters(IEnumerable<T> items, int pageNumber, int pageSize)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (pageNumber < 0)
            {
                throw new BadRequestException($"{nameof(pageNumber)} should be greater or equal to zero");
            }

            if (pageSize < 0)
            {
                throw new BadRequestException($"{nameof(pageSize)} should be greater or equal zero");
            }

            if ((pageSize == 0 && pageNumber != 0) || (pageSize != 0 && pageNumber == 0))
            {
                throw new BadRequestException($"Both {nameof(pageNumber)} and {nameof(pageSize)} must be equal to 0 or be greater than 0");
            }
        }
    }
}