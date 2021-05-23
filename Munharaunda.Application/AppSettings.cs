using Microsoft.Extensions.Configuration;
using Muharaunda.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Application
{
    public class AppSettings : IAppSettings
    {
        private readonly int minAgeInMonths;
        private readonly int lengthForMobileNumber;

        public AppSettings(IConfiguration configuration)
        {
            minAgeInMonths = configuration.GetValue<int>("General:MinAgeInMonth");
            lengthForMobileNumber = configuration.GetValue<int>("General:lengthForMobileNumber");
        }
        public int MinAgeInMonths { get => minAgeInMonths; }

        public int LengthForMobileNumber { get => lengthForMobileNumber; }
    }
}
