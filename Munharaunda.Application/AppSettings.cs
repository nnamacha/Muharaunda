using Microsoft.Extensions.Configuration;
using Muharaunda.Core.Contracts;

namespace Munharaunda.Application
{
    public class AppSettings : IAppSettings
    {
        private readonly int minAgeInMonths;
        private readonly int lengthForMobileNumber;
        private readonly int numberOfDaysToActivateProfile;
        private readonly int maximumDependentAge;
        private readonly bool profileCreationAutoAuthorisation;

        public AppSettings(IConfiguration configuration)
        {
            minAgeInMonths = int.Parse(configuration["General:MinAgeInMonth"]);
            lengthForMobileNumber = int.Parse(configuration["General:MobileNumberLength"]);
            numberOfDaysToActivateProfile = int.Parse(configuration["General:NumberOfDaysToActivateProfile"]);
            maximumDependentAge = int.Parse(configuration["General:MaximumDependentAge"]);
            profileCreationAutoAuthorisation = configuration.GetValue<bool>("General:ProfileCreationAutoAuthorisation");
        }
        public int MinAgeInMonths { get => minAgeInMonths; }

        public int LengthForMobileNumber { get => lengthForMobileNumber; }

        public int NumberOfDaysToActivateProfile { get => numberOfDaysToActivateProfile; }

        public int MaximumDependentAge { get => maximumDependentAge; }

        public bool ProfileCreationAutoAuthorisation => profileCreationAutoAuthorisation;
    }

}
