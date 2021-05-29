using Munharaunda.Core.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Core.Models
{
    public class ResponseModel<T>
    {
        public ResponseModel()
        {
           
            this.ResponseCode = ResponseConstants.R00;
            this.ResponseMessage = ResponseConstants.R00Message;
        }

        public List<T> ResponseData { get; set; }

        public string ResponseCode { get; set; }

        public List<string> Errors { get; set; }

        public string ResponseMessage { get; set; }
    }
}
