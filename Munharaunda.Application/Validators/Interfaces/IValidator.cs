using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Application.Validators.Interfaces
{
    public interface IValidator<T>
    {
        bool Validate(T t);
    }
}
