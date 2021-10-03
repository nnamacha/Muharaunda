using Munharaunda.Core.Constants;
using System.Collections.Generic;

namespace Munharaunda.Core.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {

            this.ResponseCode = ResponseConstants.R00;
            this.ResponseMessage = ResponseConstants.R00Message;
        }

        public List<T> ResponseData { get; set; } = new List<T>();

        public string ResponseCode { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public string ResponseMessage { get; set; }
    }
}
