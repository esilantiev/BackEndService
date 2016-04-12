using System.Collections.Specialized;
using System.Text;
using System.Web;

namespace Ises.Core.Api.Help.Common
{
    public static class Extensions
    {
        public static string ConvertToQueryString(this NameValueCollection nv)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < nv.Count; i++)
            {
                sb.AppendFormat("{0}={1}", HttpUtility.HtmlEncode(nv.GetKey(i)), HttpUtility.HtmlEncode(nv.Get(i)));
            }

            return sb.ToString();
        }
    }
}