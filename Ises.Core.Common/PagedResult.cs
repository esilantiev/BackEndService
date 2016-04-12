using System;
using System.Collections.Generic;

namespace Ises.Core.Common
{
    public class PagedResult<T>
    {
        public PageInfo PageInfo { get; set; }
        public List<T> Data { get; set; }
    }

    public class PageInfo
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public int TotalPages
        {
            get
            {
                if (PageSize == 0) throw new Exception("Page size could not equal to zero.");
                var totalPages = TotalItems / PageSize;
                return TotalItems % PageSize == 0 ? totalPages : totalPages + 1;
            }
        }
    }
}
