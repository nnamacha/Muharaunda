using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Core.Utilities
{
    public static class CommonUtilites
    {
        public static ResponseModel<T> GenerateResponseModel<T>()
        {
            return new ResponseModel<T>
            {
                ResponseCode = ResponseConstants.R01
            };

        }
    }
}
