using AutoMapper;
using Moq;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Application.Orchestration.Implementation;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using System.Collections.Generic;
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
        private readonly ProfilesImplementation profilesImplementation;
        private List<Profile> profiles = new List<Profile>();
        private List<Profile> activeProfiles = new List<Profile>();
        private List<Profile> unauthorisedProfiles;
        private List<Profile> dependentProfiles;
        private ProfileValidator validator;
        private ResponseModel<Profile> resultGetProfileDetails;

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
                IsNextOfKin = true,
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
                IsNextOfKin = true,
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
                IsNextOfKin = true,
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
                IsNextOfKin = true,
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
                IsNextOfKin = true,
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
                IsNextOfKin = true,
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

            resultGetProfileDetails = new ResponseModel<Muharaunda.Core.Models.Profile>
            {
                ResponseCode = ResponseConstants.R00,
                ResponseData = new List<Profile>()

            };

            var userCreationRepoResponse = new ResponseModel<Profile>
            {
                ResponseCode = ResponseConstants.R00,
            };




            _profileRepository = new Mock<IProfileRespository>();
            _appSettings = new Mock<IAppSettings>();
            _mapper = new Mock<IMapper>();
            _profileRepository.Setup(x => x.ValidateIdNumber(It.IsAny<string>())).ReturnsAsync(resultValidNumber);
            _profileRepository.Setup(x => x.GetProfileDetails(It.IsAny<int>())).ReturnsAsync(resultGetProfileDetails);
            _profileRepository.Setup(x => x.CreateProfile(It.IsAny<CreateProfileRequest>())).ReturnsAsync(userCreationRepoResponse);
            _appSettings.SetupGet(x => x.MinAgeInMonths).Returns(3);
            _appSettings.SetupGet(x => x.LengthForMobileNumber).Returns(10);
            validator = new ProfileValidator(_appSettings.Object, _profileRepository.Object);
            profilesImplementation = new ProfilesImplementation(_profileRepository.Object, _mapper.Object,validator);


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



            var validation = validator.Validate(ProfileRecord);

            var isValid = validation.IsValid;

            Assert.Equal(valid, isValid);

        }

        [Theory]
        [InlineData(3, true)]
        [InlineData(100000, false)]
        public void TestProfileAgeValidation(int months, bool Validation)
        {

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

            _appSettings.SetupGet(x => x.LengthForMobileNumber).Returns(noOfCharacters);

            var validation = validator.Validate(ProfileRecord);

            var isValid = validation.IsValid;

            Assert.Equal(Validation, isValid);
        }

        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async Task TestProfileCreation(string repoResponse)
        {
            var userCreationRepoResponse = new ResponseModel<Profile>
            {
                ResponseCode = repoResponse,
            };

            _profileRepository.Setup(x => x.CreateProfile(It.IsAny<CreateProfileRequest>())).ReturnsAsync(userCreationRepoResponse);

            var result = await profilesImplementation.CreateProfile(ProfileRecord);

            Assert.Equal(result.ResponseCode, repoResponse);

        }
        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async Task TestProfileDelete(string repoResponse)
        {
            var userCreationRepoResponse = new ResponseModel<bool>
            {
                ResponseCode = repoResponse,
            };


            _profileRepository.Setup(x => x.DeleteProfile(It.IsAny<int>())).ReturnsAsync(userCreationRepoResponse);

            var result = await profilesImplementation.DeleteProfile(1);

            Assert.Equal(result.ResponseCode, repoResponse);
        }
        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async Task TestAuthoriseProfile(string repoResponse)
        {
            var userCreationRepoResponse = new ResponseModel<Profile>
            {
                ResponseCode = repoResponse,
            };

            ProfileRecord.ProfileStatus = ProfileStatuses.Unauthorised;

            resultGetProfileDetails.ResponseData.Add(ProfileRecord);

            _profileRepository.Setup(x => x.AuthoriseProfile(It.IsAny<int>())).ReturnsAsync(userCreationRepoResponse);

            _profileRepository.Setup(x => x.GetProfileDetails(It.IsAny<int>())).ReturnsAsync(resultGetProfileDetails);

            var result = await profilesImplementation.AuthoriseProfile(1);

            Assert.Equal(result.ResponseCode, repoResponse);
        }

        [Theory]
        [InlineData(ResponseConstants.R00,ProfileStatuses.Unauthorised,1)]
        [InlineData(ResponseConstants.R01,ProfileStatuses.Terminated,0)]
        [InlineData(ResponseConstants.R01,ProfileStatuses.Active,0)]
        [InlineData(ResponseConstants.R01,ProfileStatuses.Flagged,0)]
        public async Task TestAuthoriseProfileSuccessfulWhenStatusUnauthorisedOnly(string repoResponse,ProfileStatuses profileStatus, int count)
        {

            var userCreationRepoResponse = new ResponseModel<Profile>
            {
                ResponseCode = ResponseConstants.R00,
            };

            ProfileRecord.ProfileStatus = profileStatus;

            resultGetProfileDetails.ResponseData.Add(ProfileRecord);

           _profileRepository.Setup(x => x.AuthoriseProfile(It.IsAny<int>())).ReturnsAsync(userCreationRepoResponse);

            _profileRepository.Setup(x => x.GetProfileDetails(It.IsAny<int>())).ReturnsAsync(resultGetProfileDetails);

            var result = await profilesImplementation.AuthoriseProfile(1);

            Assert.Equal(result.ResponseCode, repoResponse);
            _profileRepository.Verify(m => m.AuthoriseProfile(It.IsAny<int>()), Times.Exactly(count));
        }

        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async  Task TestGetListOfActiveProfiles(string repoResponse)
        {
            var listOfProfiles = new ResponseModel<Profile>()
            {
                ResponseData = new List<Profile>()
            };

            if (repoResponse == ResponseConstants.R00)
            {
                listOfProfiles.ResponseData = activeProfiles;
            }
            else
            {
                listOfProfiles.ResponseData = profiles;
            }

            _profileRepository.Setup(x => x.GetListOfActiveProfiles()).ReturnsAsync(listOfProfiles);

            var result = await profilesImplementation.GetListOfActiveProfiles();

            Assert.Equal(repoResponse, result.ResponseCode);

        } 
        
        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async  Task TestGetListOfUnauthorisedProfiles(string repoResponse)
        {
            var listOfProfiles = new ResponseModel<Profile>()
            {
                ResponseData = new List<Profile>()
            };

            if (repoResponse == ResponseConstants.R00)
            {
                listOfProfiles.ResponseData = unauthorisedProfiles;
            }
            else
            {
                listOfProfiles.ResponseData = profiles;
            }

            _profileRepository.Setup(x => x.GetUnauthorisedProfiles()).ReturnsAsync(listOfProfiles);

            var result = await profilesImplementation.GetUnauthorisedProfiles();

            Assert.Equal(repoResponse, result.ResponseCode);

        }

        [Theory]
        [InlineData(ResponseConstants.R00)]
        [InlineData(ResponseConstants.R01)]
        public async Task TestGetListOfDependentsByProfile(string repoResponse)
        {
            var listOfProfiles = new ResponseModel<Profile>()
            {
                ResponseData = new List<Profile>()
            };

            if (repoResponse == ResponseConstants.R00)
            {
                listOfProfiles.ResponseData = dependentProfiles;
            }
            else
            {
                listOfProfiles.ResponseData = profiles;
            }

            _profileRepository.Setup(x => x.GetListOfDependentsByProfile(It.IsAny<int>())).ReturnsAsync(listOfProfiles);

            var result = await profilesImplementation.GetListOfDependentsByProfile()

            
        }



    }
}
