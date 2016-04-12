using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Owin;

namespace Ises.Core.Common.Middleware
{
    public class RequestLogger : OwinMiddleware
    {
        public RequestLogger(OwinMiddleware next)
            : base(next)
        {
        }

        public override async Task Invoke(IOwinContext context)
        {
            var requestId = string.Format("{0}_{1}", DateTime.Now.ToFileTimeUtc().ToString(CultureInfo.InvariantCulture), Thread.CurrentThread.ManagedThreadId.ToString(CultureInfo.InvariantCulture));

            var request = context.Request;

            var stopWatch = new Stopwatch();

            var host = string.Format("Scheme:{0}, Local:{1}:{2}, Remote {3}:{4}", request.Scheme, request.LocalIpAddress, request.LocalPort, request.RemoteIpAddress, request.RemotePort);

            string requestBody;
            var stream = request.Body;
            using (var sr = new StreamReader(stream)) requestBody = sr.ReadToEnd();
            request.Body = new MemoryStream(Encoding.UTF8.GetBytes(requestBody));

            ApplicationContext.Logger.DebugFormat("{0} - Request: {1} {2} {3}. Body: {4}", requestId, request.Method, host, request.Path, requestBody.IsNullOrEmpty() ? "Empty" : requestBody);
           
            stopWatch.Start();
            await Next.Invoke(context);
            stopWatch.Stop();

            var response = context.Response;
            string responseBody;
            stream = request.Body;

            using (var sr = new StreamReader(stream)) responseBody = sr.ReadToEnd();

            request.Body = new MemoryStream(Encoding.UTF8.GetBytes(responseBody));
            var executionTime = stopWatch.ElapsedMilliseconds.ToString(CultureInfo.InvariantCulture);

            ApplicationContext.Logger.DebugFormat("{0} - Response: {1}. Took {2} milliseconds", requestId, response.StatusCode.ToString(CultureInfo.InvariantCulture), executionTime);
        }
    }
}
