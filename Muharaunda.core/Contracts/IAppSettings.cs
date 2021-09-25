using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Muharaunda.Core.Contracts
{
    public interface IAppSettings
    {
        public int MinAgeInMonths { get; }
        public int LengthForMobileNumber { get; }
        public int NumberOfDaysToActivateProfile { get; }
        public int MaximumDependentAge { get;  }
    }
}
