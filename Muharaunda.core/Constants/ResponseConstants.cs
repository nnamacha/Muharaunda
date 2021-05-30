using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Core.Constants
{
    public class ResponseConstants
    {
        public const string R00 = "R00";
        public const string R01 = "R01";
        public const string R02 = "R02";
        public const string R03 = "R03";
        

        //Unhandled exceptions
        public const string R99 = "R99";

        //Response messages
        public const string R00Message = "Success";
        public const string R01Message = "Failure";
        public const string R02Message = "Validation error";
        public const string CREATE_PROFILE_REQUEST_INVALID = "The Create profile request is invalid";
       
    }
}
