using FluentValidation;
using FluentValidation.Results;
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
using Munharaunda.Domain.Dtos;
using Munharaunda.Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Test
{
    public class FuneralTest
    {
        private Profile profileRecord;
        private Funeral funeralRecord;
        private CreateFuneralRequest request;
        private FuneralService funeralService;
        private readonly Mock<IProfileRepository> _profileRepository;
        private readonly Mock<IFuneralRepository> _funeralRepository;
        private readonly Mock<IAppSettings> _appSettings;
        private readonly FuneralValidator validator;
        private readonly Mock<IValidator<Funeral>> _validator;

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
                Profile = profileRecord,
                Address = "30-33 crescent",
                DateOfDeath = DateTime.Parse("2021-10-03T00:00:36.597Z")
            };

            request = new CreateFuneralRequest()
            {
                DateOfDeath = DateTime.Now,
                Address = "21 Jump Street",
                ProfileId = 1
            };
            _profileRepository = new Mock<IProfileRepository>();

            _funeralRepository = new Mock<IFuneralRepository>();

            _appSettings = new Mock<IAppSettings>();

            validator = new FuneralValidator();

            _validator = new Mock<IValidator<Funeral>>(MockBehavior.Loose);

            funeralService = new FuneralService(_funeralRepository.Object, _validator.Object, _profileRepository.Object);

        }

        [Theory]
        [InlineData(ResponseConstants.R404, 0)]
        [InlineData(ResponseConstants.R00, 1)]
        public async Task TestFuneralCreationWhenNoProfileFound(string responseCode, int runtimes)
        {


            var getProfileResponse = new ResponseModel<ProfileBase>()
            {
                ResponseCode = responseCode,

            };

            var validationResult = new ValidationResult();

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
            _validator.Setup(validator => validator.Validate(It.IsAny<Funeral>())).Returns(validationResult);

            var result = await funeralService.CreateFuneralAsync(request);

            Assert.Equal(result.ResponseCode, responseCode);

            _funeralRepository.Verify(m => m.CreateFuneralAsync(It.IsAny<Funeral>()), Times.Exactly(runtimes));

        }

        [Fact]
        public async Task CreateFuneralAsync_Success()
        {

            var getProfileResponse = CommonUtilites.GenerateResponseModel<ProfileBase>();
            var getFuneralDetailsByProfileId = CommonUtilites.GenerateResponseModel<Funeral>();
            var createFuneralResponse = new ResponseModel<Funeral>();
            var validationResult = new ValidationResult();

            getProfileResponse.ResponseData.Add(new()
            {
                ProfileId = 1
            });

            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(getProfileResponse);
            _funeralRepository.Setup(x => x.CreateFuneralAsync(It.IsAny<Funeral>())).ReturnsAsync(createFuneralResponse);
            _funeralRepository.Setup(x => x.GetFuneralDetailsByProfileIdAsync(It.IsAny<int>())).ReturnsAsync(getFuneralDetailsByProfileId);
            _validator.Setup(validator => validator.Validate(It.IsAny<Funeral>())).Returns(validationResult);

            var result = await funeralService.CreateFuneralAsync(request);
            Assert.Equal(result.ResponseCode, ResponseConstants.R00);


        }

        [Fact]
        public async Task CreateFuneral_ProfileNotFound()
        {
            var getProfileResponse = CommonUtilites.GenerateResponseModel<ProfileBase>();
            var getFuneralDetailsByProfileId = CommonUtilites.GenerateResponseModel<Funeral>();
            var createFuneralResponse = new ResponseModel<Funeral>();



            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(getProfileResponse);
            _funeralRepository.Setup(x => x.CreateFuneralAsync(It.IsAny<Funeral>())).ReturnsAsync(createFuneralResponse);
            _funeralRepository.Setup(x => x.GetFuneralDetailsByProfileIdAsync(It.IsAny<int>())).ReturnsAsync(getFuneralDetailsByProfileId);

            var result = await funeralService.CreateFuneralAsync(request);
            Assert.Equal(result.ResponseCode, ResponseConstants.R404);
            Assert.Equal(result.ResponseMessage, ResponseConstants.PROFILE_NOT_FOUND);
        }

        [Fact]
        public async Task FuneralValidation_Failed()
        {
            var result = new ValidationResult();
            result.Errors.Add(new ValidationFailure("SomeProperty", "SomeError"));
            var funeral = new Funeral();
            var getProfileResponse = CommonUtilites.GenerateResponseModel<ProfileBase>();
            var getFuneralDetailsByProfileId = CommonUtilites.GenerateResponseModel<Funeral>();
            var createFuneralResponse = new ResponseModel<Funeral>();

            getProfileResponse.ResponseData.Add(new()
            {
                ProfileId = 1
            });


            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(getProfileResponse);
            _funeralRepository.Setup(x => x.CreateFuneralAsync(It.IsAny<Funeral>())).ReturnsAsync(createFuneralResponse);
            _funeralRepository.Setup(x => x.GetFuneralDetailsByProfileIdAsync(It.IsAny<int>())).ReturnsAsync(getFuneralDetailsByProfileId);
            _validator.Setup(validator => validator.Validate(It.IsAny<Funeral>())).Returns(result);

            var response = await funeralService.CreateFuneralAsync(request);
            Assert.Equal(response.ResponseCode, ResponseConstants.R01);
        }


    }
}
