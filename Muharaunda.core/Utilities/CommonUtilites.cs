using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;

namespace Munharaunda.Core.Utilities
{
    public static class CommonUtilites
    {
        public static ResponseModel<T> GenerateResponseModel<T>()
        {
            return new ResponseModel<T>();


        }

        public static IActionResult GenerateResponse<T>(ResponseModel<T> response)
        {
            if (response.ResponseCode == ResponseConstants.R00 )
            {
                return new OkObjectResult(response.ResponseData);
            }
            else if (response.ResponseCode == ResponseConstants.R404)
            {
                return new NotFoundObjectResult("No Record Found.");
            }
            else if (response.ResponseCode == ResponseConstants.R500)
            {
                return new StatusCodeResult(500);
            }
            else
            {
                return new BadRequestObjectResult(response.ResponseMessage);
            }
        }
    }
}
