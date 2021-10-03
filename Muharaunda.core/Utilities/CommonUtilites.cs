using Munharaunda.Core.Models;

namespace Munharaunda.Core.Utilities
{
    public static class CommonUtilites
    {
        public static ResponseModel<T> GenerateResponseModel<T>()
        {
            return new ResponseModel<T>();


        }
    }
}
