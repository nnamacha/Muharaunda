﻿using Moq;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Application.Contracts;
using Munharaunda.Application.Dtos;
using Munharaunda.Application.Orchestration.Implementation;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using System.Collections.Generic;
using Xunit;
using static Muharaunda.Core.Constants.SystemWideConstants;

namespace Munharaunda.Test
{


    public class UserTest
    {
        private CreateProfileRequest _createProfileRequest;
        private Mock<IProfileRespository> _profileRepository;
        private Mock<IAppSettings> _appSettings;
        private readonly Profiles profilesImplementation;

        private ProfileValidator validator;



        public UserTest()
        {
            _createProfileRequest = new CreateProfileRequest()
            {
                FirstName = "Nicholas",
                Surname = "Namacha",
                DateOfBirth = "24-Aug-1982",
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "nnamacha@gmail.com",
                IsNextOfKin = true,
                ProfileType = ProfileTypes.Admin,
                NextOfKin = 2,
                ProfileStatus = ProfileStatuses.Active,
                Address = "15-10 Test Road",
                CreatedBy = 1


            };

            var resultValidNumber = new ResponseModel<bool>
            {

                ResponseData = new List<bool>() { true }

            };

            var resultGetProfileDetails = new ResponseModel<Muharaunda.Core.Models.Profile>
            {
                ResponseCode = ResponseConstants.R00,

            };






            _profileRepository = new Mock<IProfileRespository>();
            _appSettings = new Mock<IAppSettings>();
            _profileRepository.Setup(x => x.ValidateIdNumber(It.IsAny<string>())).ReturnsAsync(resultValidNumber);
            _profileRepository.Setup(x => x.GetProfileDetails(It.IsAny<int>())).ReturnsAsync(resultGetProfileDetails);
            _appSettings.SetupGet(x => x.MinAgeInMonths).Returns(3);
            _appSettings.SetupGet(x => x.LengthForMobileNumber).Returns(10);
            profilesImplementation = new Profiles(_profileRepository.Object);
            validator = new ProfileValidator(_appSettings.Object, _profileRepository.Object);

        }
        [Theory]
        [InlineData(true, ResponseConstants.R00)]
        [InlineData(false, ResponseConstants.R01)]

        public void TestNextOfKinValidation(bool valid, string expected)
        {
            var result = new ResponseModel<Profile>
            {
                ResponseCode = expected,

            };

            var validator = new NextOfKinValidator(_profileRepository.Object);

            var profile = new ProfileNextOfKin();

            _profileRepository.Setup(x => x.GetProfileDetails(It.IsAny<int>())).ReturnsAsync(result);

            var validation = validator.Validate(profile);

            var isValid = validation.IsValid;

            Assert.Equal(valid, isValid);

        }

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestIdNumberValidation(bool valid)
        {
            var resultValidNumber = new ResponseModel<bool>
            {

                ResponseData = new List<bool>() { valid }

            };

            _profileRepository.Setup(x => x.ValidateIdNumber(It.IsAny<string>())).ReturnsAsync(resultValidNumber);



            var validation = validator.Validate(_createProfileRequest);

            var isValid = validation.IsValid;

            Assert.Equal(valid, isValid);

        }

        [Theory]
        [InlineData(3, true)]
        [InlineData(100000, false)]
        public void TestProfileAgeValidation(int months, bool Validation)
        {

            _appSettings.SetupGet(x => x.MinAgeInMonths).Returns(months);
            var validation = validator.Validate(_createProfileRequest);

            var isValid = validation.IsValid;

            Assert.Equal(Validation, isValid);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(3, false)]
        public void TestMobileNumberValidation(int noOfCharacters, bool Validation)
        {

            _appSettings.SetupGet(x => x.LengthForMobileNumber).Returns(noOfCharacters);
            var validation = validator.Validate(_createProfileRequest);

            var isValid = validation.IsValid;

            Assert.Equal(Validation, isValid);
        }




    }
}
