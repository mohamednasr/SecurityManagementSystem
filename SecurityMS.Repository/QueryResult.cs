namespace SecurityMS.Repository
{
    public class QueryResult<T> where T : class
    {
        /// <summary>
        /// Result of the Query
        /// </summary>
        public IEnumerable<T> Result { get; private set; }

        /// <summary>
        /// total number of items in the query
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// page nubmer returned by query
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// page size of current query
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// create new QueryResult instant
        /// </summary>
        /// <param name="Result">The items.</param>
        /// <param name="totalCount">The total count (if paging is used, otherwise <c>0</c>).</param>
        /// <param name="pageNumber">The page number (if paging is used, otherwise <c>0</c>).</param>
        /// <param name="pageSize">The page size (if paging is used, otherwise <c>0</c>).</param>
        public QueryResult(IEnumerable<T> Result, int totalCount, int pageNumber, int pageSize)
        {
            this.Result = Result;
            this.TotalCount = totalCount;
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
        }
    }
}
