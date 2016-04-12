using System.Web.Http;
using System.Web.Http.Description;
using Ises.BackOffice.Api.Utils;
using Ises.Core.Api.Common;

namespace Ises.BackOffice.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [RoutePrefix(RouteConstants.BaseRoute + "/system")]
    public class SystemController : BaseSystemController
    {
    }
}
