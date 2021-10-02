using AutoMapper;
using Microsoft.Extensions.Configuration;
using Moq;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Application.Orchestration.Implementation;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using static Muharaunda.Core.Constants.SystemWideConstants;
using Profile = Muharaunda.Core.Models.Profile;

namespace Munharaunda.Test
{


    public class UserTest
    {
        private CreateProfileRequest ProfileRecord;
        private Mock<IProfileRespository> _profileRepository;
        private Mock<IAppSettings> _appSettings;
        private Mock<IMapper> _mapper;
        private readonly Mock<IConfiguration> _configuration;
        private readonly ProfileService profilesImplementation;
        private List<ProfileBase> profiles = new List<ProfileBase>();
        private List<ProfileBase> activeProfiles = new List<ProfileBase>();
        private List<ProfileBase> unauthorisedProfiles;
        private List<ProfileBase> dependentProfiles;
        private ProfileValidator validator;
        private readonly DependentValidator dependentValidator;
        private ResponseModel<ProfileBase> resultGetProfileDetails;


        public UserTest()
        {
            ProfileRecord = new CreateProfileRequest()
            {
                ProfileId = 1,
                FirstName = "Nicholas",
                Surname = "Namacha",
                DateOfBirth = "24-Aug-1982",
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "nnamacha@gmail.com",
                ProfileType = ProfileTypes.Admin,
                NextOfKin = 2,
                ProfileStatus = ProfileStatuses.Active,
                Address = "15-10 Test Road",
                CreatedBy = 1


            };



            profiles.Add(new CreateProfileRequest()
            {
                ProfileId = 2,
                FirstName = "Marvelous",
                Surname = "Namacha",
                DateOfBirth = "15-May-1985",
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "mnamacha@test.com",
                //IsNextOfKin = true,
                ProfileType = ProfileTypes.Member,
                NextOfKin = 2,
                ProfileStatus = ProfileStatuses.Active,
                Address = "15-10 Test Road",
                CreatedBy = 1


            });
            profiles.Add(new CreateProfileRequest()
            {
                ProfileId = 3,
                FirstName = "Patick",
                Surname = "Namacha",
                DateOfBirth = "15-May-1985",
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "mnamacha@test.com",

                ProfileType = ProfileTypes.Member,
                NextOfKin = 2,
                ProfileStatus = ProfileStatuses.Unauthorised,
                Address = "15-10 Test Road",
                CreatedBy = 1


            });
            profiles.Add(new CreateProfileRequest()
            {
                ProfileId = 4,
                FirstName = "Leon",
                Surname = "Mapemba",
                DateOfBirth = "15-May-1985",
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "mnamacha@test.com",

                ProfileType = ProfileTypes.Member,
                NextOfKin = 2,
                ProfileStatus = ProfileStatuses.Terminated,
                Address = "15-10 Test Road",
                CreatedBy = 1


            });

            profiles.Add(new CreateProfileRequest()
            {
                ProfileId = 5,
                FirstName = "Nick",
                Surname = "Namacha",
                DateOfBirth = "15-May-1985",
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "mnamacha@test.com",

                ProfileType = ProfileTypes.Member,
                NextOfKin = 2,
                ProfileStatus = ProfileStatuses.Flagged,
                Address = "15-10 Test Road",
                CreatedBy = 1


            });

            profiles.Add(new CreateProfileRequest()
            {
                ProfileId = 6,
                FirstName = "Nick",
                Surname = "Namacha",
                DateOfBirth = "15-May-1985",
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "mnamacha@test.com",

                ProfileType = ProfileTypes.Dependent,
                NextOfKin = 2,
                ProfileStatus = ProfileStatuses.Flagged,
                Address = "15-10 Test Road",
                CreatedBy = 1


            });


            activeProfiles = profiles.FindAll(x => x.ProfileStatus == ProfileStatuses.Active);

            unauthorisedProfiles = profiles.FindAll(x => x.ProfileStatus == ProfileStatuses.Unauthorised);
            dependentProfiles = profiles.FindAll(x => x.ProfileType == ProfileTypes.Dependent);

            var resultValidNumber = new ResponseModel<bool>
            {

                ResponseData = new List<bool>() { true }

            };

            resultGetProfileDetails = new ResponseModel<ProfileBase>
            {
                ResponseCode = ResponseConstants.R00,
                ResponseData = new List<ProfileBase>()

            };

            var userCreationRepoResponse = new ResponseModel<ProfileBase>
            {
                ResponseCode = ResponseConstants.R00,
            };




            _profileRepository = new Mock<IProfileRespository>();

            _appSettings = new Mock<IAppSettings>();

            _mapper = new Mock<IMapper>();

            _configuration = new Mock<IConfiguration>();

            _profileRepository.Setup(x => x.ValidateIdNumber(It.IsAny<string>())).ReturnsAsync(resultValidNumber);

            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(resultGetProfileDetails);

            _profileRepository.Setup(x => x.CreateProfileAsync(It.IsAny<CreateProfileRequest>())).ReturnsAsync(userCreationRepoResponse);

            _appSettings.SetupGet(x => x.MinAgeInMonths).Returns(3);

            _appSettings.SetupGet(x => x.LengthForMobileNumber).Returns(10);

            _appSettings.SetupGet(x => x.MaximumDependentAge).Returns(20);

            validator = new ProfileValidator(_appSettings.Object, _profileRepository.Object);

            dependentValidator = new DependentValidator(_appSettings.Object);

            profilesImplementation = new ProfileService(_profileRepository.Object, _mapper.Object, validator, _appSettings.Object);


        }
        [Theory]
        [InlineData(true, ResponseConstants.R00)]
        [InlineData(false, ResponseConstants.R01)]

        public void TestNextOfKinValidation(bool valid, string expected)
        {
            var result = new ResponseModel<ProfileBase>
            {
                ResponseCode = expected,

            };

            var validator = new NextOfKinValidator(_profileRepository.Object);

            var profile = new ProfileNextOfKin();

            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(result);

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
            _appSettings.SetupGet(x => x.ProfileCreationAutoAuthorisation).Returns(true);



            var validation = validator.Validate(ProfileRecord);

            var isValid = validation.IsValid;

            Assert.Equal(valid, isValid);

        }

        [Theory]
        [InlineData(3, true)]
        [InlineData(100000, false)]
        public void TestProfileAgeValidation(int months, bool Validation)
        {
            _appSettings.SetupGet(x => x.ProfileCreationAutoAuthorisation).Returns(true);
            _appSettings.SetupGet(x => x.MinAgeInMonths).Returns(months);
            var validation = validator.Validate(ProfileRecord);

            var isValid = validation.IsValid;

            Assert.Equal(Validation, isValid);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(3, false)]
        public void TestMobileNumberValidation(int noOfCharacters, bool Validation)
        {
            _appSettings.SetupGet(x => x.ProfileCreationAutoAuthorisation).Returns(true);
            _appSettings.SetupGet(x => x.LengthForMobileNumber).Returns(noOfCharacters);

            var validation = validator.Validate(ProfileRecord);

            var isValid = validation.IsValid;

            Assert.Equal(Validation, isValid);
        }

        [Theory]
        [InlineData(10, true)]
        [InlineData(30, false)]
        public void TestDependentAgeValidation(int age, bool Validation)
        {
            var dob = DateTime.Now.AddYears(age * -1);

            var dependent = new CreateProfileRequest()
            {
                ProfileId = 2,
                FirstName = "Marvelous",
                Surname = "Namacha",
                DateOfBirth = dob.ToString(),
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "mnamacha@test.com",
                ProfileType = ProfileTypes.Dependent,
                NextOfKin = 2,
                ProfileStatus = ProfileStatuses.Active,
                Address = "15-10 Test Road",
                CreatedBy = 1
            };

            var validation = dependentValidator.Validate(dependent);

            var isValid = validation.IsValid;

            Assert.Equal(Validation, isValid);
        }

        [Theory]
        [InlineData(10, ProfileTypes.Dependent, true)]
        [InlineData(10, ProfileTypes.Member, false)]
        public void TestDependentProfileTypeValidation(int age, ProfileTypes profileType, bool Validation)
        {
            var dob = DateTime.Now.AddYears(age * -1);

            var dependent = new CreateProfileRequest()
            {
                ProfileId = 2,
                FirstName = "Marvelous",
                Surname = "Namacha",
                DateOfBirth = dob.ToString(),
                IdentificationType = IdentificationTypes.Passport,
                IdentificationNumber = "123458690",
                MobileNumber = "+27846994000",
                Email = "mnamacha@test.com",

                ProfileType = profileType,
                NextOfKin = 2,
                ProfileStatus = ProfileStatuses.Active,
                Address = "15-10 Test Road",
                CreatedBy = 1
            };

            var validation = dependentValidator.Validate(dependent);

            var isValid = validation.IsValid;

            Assert.Equal(Validation, isValid);
        }

        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async Task TestProfileCreation(string repoResponse)
        {
            var userCreationRepoResponse = new ResponseModel<ProfileBase>
            {
                ResponseCode = repoResponse,
            };

            _appSettings.SetupGet(x => x.ProfileCreationAutoAuthorisation).Returns(true);
            _profileRepository.Setup(x => x.CreateProfileAsync(It.IsAny<CreateProfileRequest>())).ReturnsAsync(userCreationRepoResponse);

            var result = await profilesImplementation.CreateProfileAsync(ProfileRecord);

            Assert.Equal(result.ResponseCode, repoResponse);

        }
        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async Task TestProfileDelete(string repoResponse)
        {
            var GetProfileDetailsRepoResponse = new ResponseModel<ProfileBase>
            {
                ResponseCode = repoResponse,
            };

            var DeleteProfileAsyncRepoResponse = new ResponseModel<bool>
            {
                ResponseCode = repoResponse,
            };

            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(GetProfileDetailsRepoResponse);

            _profileRepository.Setup(x => x.DeleteProfileAsync(It.IsAny<int>())).ReturnsAsync(DeleteProfileAsyncRepoResponse);

            var result = await profilesImplementation.DeleteProfileAsync(1);

            Assert.Equal(result.ResponseCode, repoResponse);
        }
        [Theory]
        [InlineData(ResponseConstants.R00,true, ResponseConstants.R00)]
        [InlineData(ResponseConstants.R00,false, ResponseConstants.R01)]
        [InlineData(ResponseConstants.R01,true, ResponseConstants.R01)]
        [InlineData(ResponseConstants.R01,false, ResponseConstants.R01)]
        public async Task TestAuthoriseProfile(string repoResponse,bool authorised, string expected)
        {
            var userCreationRepoResponse = new ResponseModel<bool>
            {
                ResponseCode = repoResponse,
                ResponseData = new List<bool>()
                {
                    authorised
                }
            };

            ProfileRecord.ProfileStatus = ProfileStatuses.Unauthorised;

            resultGetProfileDetails.ResponseData.Add(ProfileRecord);

            _profileRepository.Setup(x => x.AuthoriseProfileAsync(It.IsAny<int>())).ReturnsAsync(userCreationRepoResponse);

            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(resultGetProfileDetails);

            var result = await profilesImplementation.AuthoriseProfileAsync(1);

            Assert.Equal(result.ResponseCode, expected);
        }

        [Theory]
        [InlineData(ResponseConstants.R00, ProfileStatuses.Unauthorised, 1)]
        [InlineData(ResponseConstants.R01, ProfileStatuses.Terminated, 0)]
        [InlineData(ResponseConstants.R01, ProfileStatuses.Active, 0)]
        [InlineData(ResponseConstants.R01, ProfileStatuses.Flagged, 0)]
        public async Task TestAuthoriseProfileSuccessfulWhenStatusUnauthorisedOnly(string repoResponse, ProfileStatuses profileStatus, int count)
        {

            var userCreationRepoResponse = new ResponseModel<bool>
            {
                ResponseCode = ResponseConstants.R00,
                ResponseData = new List<bool>
                {
                    true
                }
            };

            ProfileRecord.ProfileStatus = profileStatus;

            resultGetProfileDetails.ResponseData.Add(ProfileRecord);

            _profileRepository.Setup(x => x.AuthoriseProfileAsync(It.IsAny<int>())).ReturnsAsync(userCreationRepoResponse);

            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(resultGetProfileDetails);

            var result = await profilesImplementation.AuthoriseProfileAsync(1);

            Assert.Equal(result.ResponseCode, repoResponse);
            _profileRepository.Verify(m => m.AuthoriseProfileAsync(It.IsAny<int>()), Times.Exactly(count));
        }

        [Theory]
        [InlineData(ResponseConstants.R00, ProfileStatuses.Unauthorised)]
        [InlineData(ResponseConstants.R01, ProfileStatuses.Active)]

        public async Task TestCreateProfileAutoAuthorizationFlag(string repoResponse, ProfileStatuses profileStatus)
        {

            var getProfileRepoResponse = new ResponseModel<ProfileBase>
            {
                ResponseCode = ResponseConstants.R00,


            };
            var userCreationRepoResponse = new ResponseModel<ProfileBase>
            {
                ResponseCode = repoResponse,

            };
            
            var userAuthorisationRepoResponse = new ResponseModel<bool>
            {
                ResponseCode = ResponseConstants.R00,
                ResponseData = new List<bool>()
                {
                    true
                }


            };

            ProfileRecord.ProfileStatus = profileStatus;
            userCreationRepoResponse.ResponseData.Add(ProfileRecord);
            getProfileRepoResponse.ResponseData.Add(ProfileRecord);
            _appSettings.SetupGet(x => x.ProfileCreationAutoAuthorisation).Returns(true);

            _profileRepository.Setup(x => x.CreateProfileAsync(It.IsAny<CreateProfileRequest>())).ReturnsAsync(userCreationRepoResponse);

            _profileRepository.Setup(x => x.GetProfileDetailsAsync(It.IsAny<int>())).ReturnsAsync(getProfileRepoResponse);

            _profileRepository.Setup(x => x.AuthoriseProfileAsync(It.IsAny<int>())).ReturnsAsync(userAuthorisationRepoResponse);

            var result = await profilesImplementation.AuthoriseProfileAsync(1);

            Assert.Equal(result.ResponseCode, repoResponse);

        }

        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async Task TestGetListOfActiveProfiles(string repoResponse)
        {
            var listOfProfiles = new ResponseModel<ProfileBase>()
            {
                ResponseData = new List<ProfileBase>()
            };

            if (repoResponse == ResponseConstants.R00)
            {
                listOfProfiles.ResponseData = activeProfiles.Cast<ProfileBase>().ToList();
            }
            else
            {
                listOfProfiles.ResponseData = profiles.Cast<ProfileBase>().ToList();
            }

            _profileRepository.Setup(x => x.GetListOfActiveProfilesAsync()).ReturnsAsync(listOfProfiles);

            var result = await profilesImplementation.GetListOfActiveProfilesAsync();

            Assert.Equal(repoResponse, result.ResponseCode);

        }

        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async Task TestGetListOfUnauthorisedProfiles(string repoResponse)
        {
            var listOfProfiles = new ResponseModel<ProfileBase>()
            {
                ResponseData = new List<ProfileBase>()
            };

            if (repoResponse == ResponseConstants.R00)
            {
                listOfProfiles.ResponseData = unauthorisedProfiles;
            }
            else
            {
                listOfProfiles.ResponseData = profiles;
            }

            _profileRepository.Setup(x => x.GetUnauthorisedProfilesAsync()).ReturnsAsync(listOfProfiles);

            var result = await profilesImplementation.GetUnauthorisedProfilesAsync();

            Assert.Equal(repoResponse, result.ResponseCode);

        }

        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async Task TestGetListOfDependentsByProfile(string repoResponse)
        {
            var listOfProfiles = new ResponseModel<ProfileBase>()
            {
                ResponseData = new List<ProfileBase>()
            };

            if (repoResponse == ResponseConstants.R00)
            {
                listOfProfiles.ResponseData = dependentProfiles.Cast<ProfileBase>().ToList(); ;
            }
            else
            {
                listOfProfiles.ResponseData = profiles.Cast<ProfileBase>().ToList(); ;
            }

            _profileRepository.Setup(x => x.GetListOfDependentsByProfileAsync(It.IsAny<int>())).ReturnsAsync(listOfProfiles);

            var result = await profilesImplementation.GetListOfDependentsByProfileAsync(1);


        }



    }
}
