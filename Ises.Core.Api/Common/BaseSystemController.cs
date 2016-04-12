using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Ises.Core.Common;

namespace Ises.Core.Api.Common
{
    public class BaseSystemController : ApiController
    {
        [Route("ping"), HttpGet]
        public IHttpActionResult Ping()
        {
            return Ok(true);
        }

        [Route("hostversion"), HttpGet]
        public IHttpActionResult HostVersion()
        {
            return Ok(new { Version = Utils.GetHostVersion() });
        }

        [Route("log"), HttpGet]
        public async Task<IHttpActionResult> Log()
        {
            var content = await Utils.GetLoggerContent();

            var response = new HttpResponseMessage { Content = new StringContent(content) };
            return ResponseMessage(response);
        }
    }
}
