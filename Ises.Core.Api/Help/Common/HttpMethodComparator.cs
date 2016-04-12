using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Ises.Core.Api.Help.Common
{
    /// <summary>
    ///     Quick comparer for ordering http methods for display
    /// </summary>
    public class HttpMethodComparator : IComparer<HttpMethod>
    {
        private readonly string[] order =
		{
			"GET",
			"POST",
			"PUT",
			"DELETE"
		};

        public int Compare(HttpMethod x, HttpMethod y)
        {
            return Array.IndexOf(order, x.ToString()).CompareTo(Array.IndexOf(order, y.ToString()));
        }
    }
}