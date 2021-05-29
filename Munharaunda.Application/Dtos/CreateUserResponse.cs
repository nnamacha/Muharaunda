using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Application.Dtos
{
    public class CreateUserResponse
    {
        public bool UserCreated { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }

    }
}
