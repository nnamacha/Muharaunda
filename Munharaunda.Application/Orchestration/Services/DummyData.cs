using Bogus;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Application;
using Munharaunda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Application.Orchestration.Services
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

            for (int j = 0; j <= numberOfProfiles; j++)
            {
                var funeralPayments = new List<FuneralPayment>();

                for (int i = 0; i < 4; i++)
                {
                    var funeral = new Faker<Funeral>()
                        .RuleFor(o => o.FuneralId, f => Guid.NewGuid().ToString())
                        .RuleFor(o => o.Address, f => f.Address.StreetAddress(true))
                        .RuleFor(o => o.Created, f => DateTime.Now)
                        .RuleFor(o => o.ProfileId, f => f.Random.Number())
                        .RuleFor(o => o.DateOfDeath, f => f.Date.Between(DateTime.Now.AddYears(-20), DateTime.Today));

                    var funeralPayment = new Faker<FuneralPayment>()
                     .RuleFor(o => o.Amount, 100)
                     .RuleFor(o => o.Paid, f => f.PickRandom<bool>())
                     .RuleFor(o => o.Funeral, f => funeral);

                    funeralPayments.Add(funeralPayment);


                }



                var profile = new Faker<ProfileBase>()
                    .StrictMode(false)
                    .RuleFor(o => o.id, f => Guid.NewGuid())
                    .RuleFor(o => o.ActivationDate, f => f.Date.Soon(_appSettings.NumberOfDaysToActivateProfile))
                    .RuleFor(o => o.Address, f => f.Address.StreetAddress(true))
                    .RuleFor(o => o.DateOfBirth, f => f.Date.Between(DateTime.Today.AddYears(-100), DateTime.Now.AddMonths(-1)))
                    .RuleFor(o => o.ProfileType, f => f.PickRandom<ProfileTypes>())
                    .RuleFor(o => o.Email, f => f.Internet.Email(f.Name.FirstName(), f.Name.LastName()))
                    .RuleFor(o => o.FirstName, f => f.Name.FirstName())
                    .RuleFor(o => o.FuneralPayments, funeralPayments);


                profiles.Add(profile);
            }

           

            return profiles;
        }

    }
}
