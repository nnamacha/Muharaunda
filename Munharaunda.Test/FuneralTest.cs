using Moq;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Application.Orchestration.Services;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Test
{
    public class FuneralTest
    {
        private Profile profileRecord;
        private Funeral funeralRecord;
        private FuneralService funeralService;
        private readonly Mock<IProfileRepository> _profileRepository;
        private readonly Mock<IFuneralRepository> _funeralRepository;
        private readonly Mock<IAppSettings> _appSettings;
        private readonly FuneralValidator validator;

        public FuneralTest()
        {
            profileRecord = new Profile()
            {
                ProfileId = 1,
                FirstName = "Nicholas",
                Surname = "Namacha",
                DateOfBirth = DateTime.Parse("24-Aug-1982"),
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "nnamacha@gmail.com",
                ProfileType = ProfileTypes.Admin,
                NextOfKin = 2,
                ProfileStatus = Statuses.Active,
                Address = "15-10 Test Road",
                Audit = new Audit()
                {
                    CreatedBy = 1

                }


            };

            funeralRecord = new Funeral()
            {
                ProfileId = 1,
                Address = "30-33 crescent",
                DateOfDeath = DateTime.Parse("2021-10-03T00:00:36.597Z")
            };


            _profileRepository = new Mock<IProfileRepository>();

            _funeralRepository = new Mock<IFuneralRepository>();

            _appSettings = new Mock<IAppSettings>();

            validator = new FuneralValidator(_appSettings.Object, _funeralRepository.Object);

            funeralService = new FuneralService(_funeralRepository.Object, validator, _appSettings.Object, _profileRepository.Object);

        }

        [Theory]
        [InlineData(ResponseConstants.R400, 0)]
        [InlineData(ResponseConstants.R00, 1)]
        public async Task TestFuneralCreationWhenNoProfileFound(string responseCode, int runtimes)
        {
            

            var getProfileResponse = new ResponseModel<ProfileBase>()
            {
                ResponseCode = responseCode,
                 
            };

            var createFuneralResponse = new ResponseModel<Funeral>();

            var updateProfileStatusResponse = new ResponseModel<bool>()
            {
                ResponseCode = responseCode,

            };

             var getFuneralDetailsByProfileId = CommonUtilites.GenerateResponseModel<Funeral>();

            getProfileResponse.ResponseData.Add(profileRecord);
            _funeralRepository.Setup(x => x.CreateFuneralAsync(It.IsAny<Funeral>())).ReturnsAsync(createFuneralResponse);
            _profileRepository.Setup(x => x.UpdateProfileStatusAsync(It.IsAny<int>(), It.IsAny<Statuses>())).ReturnsAsync(updateProfileStatusResponse);
            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(getProfileResponse);
            _funeralRepository.Setup(x => x.GetFuneralDetailsByProfileIdAsync(It.IsAny<int>())).ReturnsAsync(getFuneralDetailsByProfileId);

            var result = await funeralService.CreateFuneralAsync(funeralRecord);

            Assert.Equal(result.ResponseCode, responseCode);

            _funeralRepository.Verify(m => m.CreateFuneralAsync(It.IsAny<Funeral>()), Times.Exactly(runtimes));

        }


    }
}
