using System;
using System.Collections.Generic;
using Ises.Core.Common;
using Newtonsoft.Json;

namespace Ises.Contracts.ClientFilters
{
    public class Filter
    {
        public long Id { get; set; }
        public int Page { get; set; }
        public string OrderBy { get; set; }
        public bool ApplyPaging { get; set; }
        [JsonIgnore]
        public int Skip { get { return ApplyPaging ? PageSize : 0; }   }
        [JsonIgnore]
        public int Take
        {
            get
            {
                if (!ApplyPaging) return Int32.MaxValue;
                var maxPageSize = Configuration.Report.DefaultItemsPerPage;
                return PageSize > maxPageSize ? maxPageSize : PageSize;
            }
        }

        public IEnumerable<string> PropertiesToInclude { get; set; }

        public int PageSize { get; set; }

        public Filter()
        {
            PageSize = 10;
            Page = 1;
            OrderBy = "Id";
            PropertiesToInclude = new String[0];
            ApplyPaging = false;
        }
    }
}
