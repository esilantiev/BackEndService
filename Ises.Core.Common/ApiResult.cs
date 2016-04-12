using System.Collections.Generic;

namespace Ises.Core.Common
{
    public class ApiResult
    {
        public ApiResult(MessageType messageType, object pagedResult = null)
        {
            MessageType = messageType;
            PagedResult = pagedResult;
            AdditionalDetails = new Dictionary<object, object>();
        }

        public dynamic PagedResult { get; set; }
        public MessageType MessageType { get;  set; }
        public ApiError ApiError { get; set; }
        public Dictionary<object, object> AdditionalDetails { get; set; }

    }

    public class ApiError
    {
        public string Message { get; set; }
        public Dictionary<string, string> ErrorDetails { get; set; }
    }

    public enum MessageType
    {
        Success,
        Danger,
        Warning
    }

}
