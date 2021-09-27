using AutoMapper;
using Muharaunda.Core.Constants;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using System;
using System.Threading.Tasks;
using Profile = Muharaunda.Core.Models.Profile;

namespace Munharaunda.Application.Orchestration.Implementation
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRespository _repository;
        private readonly IMapper _mapper;
        private readonly ProfileValidator _validator;
        private readonly IAppSettings _appSettings;

        public ProfileService(IProfileRespository repository, IMapper mapper, ProfileValidator validator, IAppSettings appSettings)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            _appSettings = appSettings;
        }
        public async Task<ResponseModel<ProfileBase>> AuthoriseProfileAsync(int profileId)
        {
            ResponseModel<ProfileBase> response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            var profileDetails = await _repository.GetProfileDetailsAsync(profileId);

            if (profileDetails.ResponseCode == ResponseConstants.R00)
            {

                if (profileDetails.ResponseData[0].ProfileStatus == SystemWideConstants.ProfileStatuses.Unauthorised)
                {
                    response = await _repository.AuthoriseProfileAsync(profileId);
                }
                else
                {
                    response.ResponseCode = ResponseConstants.R01;
                    response.ResponseMessage = ResponseConstants.INVALID_PROFILE_STATUS;

                }

            }
            else
            {

                response = profileDetails;

            }

            return response;
        }

        public async Task<ResponseModel<ProfileBase>> CreateProfileAsync(CreateProfileRequest request)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            try
            {
                if (_appSettings.ProfileCreationAutoAuthorisation)
                {
                    request.ProfileStatus = SystemWideConstants.ProfileStatuses.Active;
                }
                else
                {
                    request.ProfileStatus = SystemWideConstants.ProfileStatuses.Unauthorised;
                }

                var requestValidation = _validator.Validate(request);


                if (requestValidation.IsValid)
                {
                    if (request.ProfileType == SystemWideConstants.ProfileTypes.Member)
                    {
                        request.ActivationDate = CalculateProfileActivationDate();
                    }



                    request.Created = DateTime.Now.ToLocalTime();

                    response = await _repository.CreateProfileAsync(request);
                }
                else
                {
                    response.ResponseMessage = ResponseConstants.CREATE_PROFILE_REQUEST_INVALID;

                    foreach (var error in requestValidation.Errors)
                    {
                        response.Errors.Add(error.ErrorMessage);
                    }


                }

                return response;
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
                return response;

            }



        }



        public async Task<ResponseModel<bool>> DeleteProfileAsync(int profileId)
        {
            var GetProfileResponse = CommonUtilites.GenerateResponseModel<ProfileBase>();
            var response = CommonUtilites.GenerateResponseModel<bool>();


            try
            {
                GetProfileResponse = await _repository.GetProfileDetailsAsync(profileId);

                if (GetProfileResponse.ResponseCode == ResponseConstants.R00)
                {
                    response = await _repository.DeleteProfileAsync(profileId);
                }
                else
                {

                    response.ResponseCode = GetProfileResponse.ResponseCode;
                    response.ResponseMessage = GetProfileResponse.ResponseMessage;
                    response.ResponseData.Add(false);

                }

                return response;
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
                return response;

            }
        }



        public async Task<ResponseModel<ProfileBase>> GetListOfActiveProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();



            try
            {

                response = await _repository.GetListOfActiveProfilesAsync();

                var InactiveProfileFound = (response.ResponseData.FindAll(x => x.ProfileStatus != SystemWideConstants.ProfileStatuses.Active).Count > 0);

                if (InactiveProfileFound)
                {
                    response.ResponseCode = ResponseConstants.R01;
                    response.ResponseMessage = ResponseConstants.INACTIVE_PROFILE_FOUND;
                }
                else if (response.ResponseData.Count == 0)
                {
                    response.ResponseMessage = ResponseConstants.RECORD_NOT_FOUND;
                }


                return response;
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
                return response;

            }

        }

        public async Task<ResponseModel<ProfileBase>> GetUnauthorisedProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();



            try
            {

                response = await _repository.GetUnauthorisedProfilesAsync();

                var InactiveProfileFound = (response.ResponseData.FindAll(x => x.ProfileStatus != SystemWideConstants.ProfileStatuses.Unauthorised).Count > 0);

                if (InactiveProfileFound)
                {
                    response.ResponseCode = ResponseConstants.R01;
                    response.ResponseMessage = ResponseConstants.AUTHORISED_PROFILE_FOUND;
                }
                else if (response.ResponseData.Count == 0)
                {
                    response.ResponseMessage = ResponseConstants.RECORD_NOT_FOUND;
                }


                return response;
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
                return response;

            }
        }


        public async Task<ResponseModel<Profile>> GetListOfDependentsByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<Profile>();

            try
            {
                response = await _repository.GetListOfDependentsByProfileAsync(profileId);

                return response;

            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
                return response;
            }


        }

        public async Task<ResponseModel<Profile>> GetNextOfKindByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<Profile>();

            try
            {
                response = await _repository.GetNextOfKindByProfileAsync(profileId);

                return response;

            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
                return response;
            }
        }



        public Task<ResponseModel<ProfileBase>> GetProfileDetailsAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }



        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            throw new NotImplementedException();
        }

        public DateTime CalculateProfileActivationDate()
        {
            return DateTime.Now.AddDays(_appSettings.NumberOfDaysToActivateProfile);
        }

        Task<ResponseModel<ProfileBase>> IProfileRespository.GetListOfActiveProfilesAsync()
        {
            throw new NotImplementedException();
        }



        #region Private Methods



        #endregion
    }
}
