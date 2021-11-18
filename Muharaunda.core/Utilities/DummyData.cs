using Bogus;
using Muharaunda.Core.Contracts;
using Muharaunda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Domain.Utilities
{
    public class DummyData
    {
        private readonly IAppSettings _appSettings;

        public DummyData(IAppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public List<ProfileBase> GenerateDummyProfiles(int numberOfProfiles)
        {
            var profiles = new List<ProfileBase>();

            var test = new Faker<ProfileBase>()
                .StrictMode(false)
                .RuleFor(o => o.id, f => Guid.NewGuid())
                .RuleFor(o => o.ActivationDate, f => f.Date.Soon(_appSettings.NumberOfDaysToActivateProfile))
                .RuleFor(o => o.Address, f => f.Address.StreetAddress(true))
                .RuleFor(o => o.DateOfBirth, f => f.Date.Between(DateTime.Today.AddYears(-100), DateTime.Now.AddMonths(-1)))
                .RuleFor(o => o.ProfileType, f => f.PickRandom<ProfileTypes>());



            return profiles;
        }

    }
}
