using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;
using System.Web.Http;
using System.Web.Http.Description;
using Ises.BackOffice.Api.Utils;
using Ises.Core.Api.Help.Common;
using Ises.Core.Api.Help.Postman;
using Ises.Core.Api.Help.SampleGeneration;

namespace Ises.BackOffice.Api.Controllers
{
    [RoutePrefix(RouteConstants.BaseRoute + "/postman")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class PostmanController : ApiController
    {
        readonly Regex pathVariableRegEx = new Regex("\\{([A-Za-z0-9-_]+)\\}", RegexOptions.ECMAScript | RegexOptions.Compiled);
        HelpPageSampleGenerator helpPageSampleGenerator;
        string baseUri;
        JsonMediaTypeFormatter mediaTypeFormatter;

        [HttpGet]
        [Route(Name = "GetPostmanCollection")]
        [ResponseType(typeof(PostmanCollection))]
        public IHttpActionResult GetPostmanCollection()
        {
            helpPageSampleGenerator = Configuration.GetHelpPageSampleGenerator();
            baseUri = Core.Common.Configuration.Host.BackOfficeApiBaseAddress;
            mediaTypeFormatter = Configuration.Formatters.OfType<JsonMediaTypeFormatter>().Single();

            return Ok(PostmanCollectionForController());
        }

        PostmanCollection PostmanCollectionForController()
        {
            var postManCollection = new PostmanCollection
            {
                Id = Guid.NewGuid(),
                Name = string.Format("Ises Backoffice Api (v{0})", Core.Common.Utils.GetHostVersion()),
                Timestamp = DateTime.Now.Ticks,
                Order = new Guid[0],
                Folders = new Collection<PostmanFolder>(),
                Requests = new Collection<PostmanRequest>(),
                Synced = false,
                Description = "Ises Backoffice Api"
            };

            var apiExplorer = Configuration.Services.GetApiExplorer();
            var apiDescriptionsByController = apiExplorer.ApiDescriptions.GroupBy(description => description.ActionDescriptor.ActionBinding.ActionDescriptor.ControllerDescriptor.ControllerType);

            foreach (var apiDescriptionsByControllerGroup in apiDescriptionsByController)
            {
                var postManFolder = GetApiControllerDescription(apiDescriptionsByControllerGroup, postManCollection);

                postManCollection.Folders.Add(postManFolder);
            }

            return postManCollection;
        }


        #region Api definitions
        PostmanFolder GetApiControllerDescription(IGrouping<Type, ApiDescription> apiDescriptionsByControllerGroup, PostmanCollection postManCollection)
        {
            var controllerName = apiDescriptionsByControllerGroup.Key.Name.Replace("Controller", string.Empty);

            var postManFolder = new PostmanFolder
            {
                Id = Guid.NewGuid(),
                CollectionId = postManCollection.Id,
                Name = controllerName,
                Description = string.Format("Api Methods for {0}", controllerName),
                CollectionName = "api",
                Order = new Collection<Guid>()
            };

            var apiDescriptions = apiDescriptionsByControllerGroup
                .OrderBy(description => description.HttpMethod, new HttpMethodComparator())
                .ThenBy(description => description.RelativePath)
                .ThenBy(description => description.Documentation == null ? string.Empty : description.Documentation.ToString(CultureInfo.InvariantCulture));

            foreach (var apiDescription in apiDescriptions)
            {
                var request = GetApiActionDescription(apiDescription, postManCollection);
                request.Time = postManCollection.Timestamp;
                request.CollectionId = postManCollection.Id;

                postManFolder.Order.Add(request.Id); // add to the folder
                postManCollection.Requests.Add(request);
            }
            return postManFolder;
        }

        PostmanRequest GetApiActionDescription(ApiDescription apiDescription, PostmanCollection postManCollection)
        {
            TextSample sampleData = null;
            var sampleDictionary = helpPageSampleGenerator.GetSample(apiDescription, SampleDirection.Request);
            var mediaTypeHeader = GetMediaTypeHeaderValue();
            if (sampleDictionary.ContainsKey(mediaTypeHeader))
            {
                sampleData = sampleDictionary[mediaTypeHeader] as TextSample;
            }

            var nv = new NameValueCollection();
            foreach (var parameter in apiDescription.ParameterDescriptions)
            {
                var sampleObject = helpPageSampleGenerator.GetSampleObject(parameter.ParameterDescriptor.ParameterType);
                nv.Add(parameter.Name, sampleObject == null ? string.Empty : sampleObject.ToString());
            }

            var questionMark = apiDescription.RelativePath.IndexOf("?", StringComparison.Ordinal);

            var cleanedUrlParameterUrl = apiDescription.RelativePath;
            if (questionMark >= 0)
            {
                var queryString = nv.ConvertToQueryString();

                cleanedUrlParameterUrl = apiDescription.RelativePath.Remove(questionMark);
                cleanedUrlParameterUrl += "?" + queryString;
            }
            // get path variables from url
            var pathVariables = pathVariableRegEx.Matches(cleanedUrlParameterUrl)
                                                 .Cast<Match>()
                                                 .Select(m => m.Value)
                                                 .Select(s => s.Substring(1, s.Length - 2))
                                                 .ToDictionary(s => s, s => string.Format("{0}-value", s));

            // change format of parameters within string to be colon prefixed rather than curly brace wrapped
            var postmanReadyUrl = pathVariableRegEx.Replace(cleanedUrlParameterUrl, ":$1");

            // prefix url with base uri
            var url = string.Format("{0}/{1}", baseUri.TrimEnd('/'), postmanReadyUrl);
            var headers = new Dictionary<string, string>
            {
                { "Content-Type", "application/json" }
            };

            var sample = sampleData != null ? sampleData.Text : null;
            var relativePath = string.Format("/{0}", apiDescription.RelativePath);
            var request = CreatePostmanRequest(postManCollection, apiDescription.HttpMethod.Method, relativePath, url, headers, sample, "raw", apiDescription.Documentation);
            request.PathVariables = pathVariables;

            return request;
        }

        #endregion

        static string GetHeadersString(Dictionary<string, string> headers)
        {
            return headers.Select(header => string.Format("{0}: {1}", header.Key, header.Value)).Aggregate(string.Empty, (current, header) => current + ("\n" + header));
        }

        static MediaTypeHeaderValue GetMediaTypeHeaderValue()
        {
            MediaTypeHeaderValue mediaTypeHeader;
            MediaTypeHeaderValue.TryParse("application/json", out mediaTypeHeader);

            return mediaTypeHeader;
        }

        static PostmanRequest CreatePostmanRequest(PostmanCollection postManCollection, string method, string name, string url, Dictionary<string, string> headers, object sampleData, string dataMode, string description)
        {
            return new PostmanRequest
            {
                Name = name,
                Description = description,
                Url = url,
                Method = method,
                Headers = GetHeadersString(headers),
                Data = sampleData,
                DataMode = dataMode,
                Time = postManCollection.Timestamp,
                CollectionId = postManCollection.Id
            };
        }
    }
}
