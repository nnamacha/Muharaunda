using AutoMapper;
using Muharaunda.Core.Constants;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Application.Dtos;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Profile = Muharaunda.Core.Models.Profile;

namespace Munharaunda.Application.Orchestration.Implementation
{
    public class ProfilesImplementation : IProfilesImplementation
    {
        private readonly IProfileRespository _repository;
        private readonly IMapper _mapper;
        private readonly ProfileValidator _validator;

        public ProfilesImplementation(IProfileRespository repository, IMapper mapper ,ProfileValidator validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<ResponseModel<Profile>> AuthoriseProfile(int profileId)
        {
            ResponseModel<Profile> response = GenerateResponseModel<Profile>();

            var profileDetails = await _repository.GetProfileDetails(profileId);

            if (profileDetails.ResponseCode == ResponseConstants.R00)
            {

                if (profileDetails.ResponseData[0].ProfileStatus == SystemWideConstants.ProfileStatuses.Unauthorised)
                {
                    response = await _repository.AuthoriseProfile(profileId);
                }
                else
                {
                    response.ResponseMessage = ResponseConstants.INVALID_PROFILE_STATUS;
                    
                }
                
            }
            else
            {

                response = profileDetails;

            }

            return response;
        }

        public async Task<ResponseModel<Profile>> CreateProfile(CreateProfileRequest request)
        {
            var response = GenerateResponseModel<Profile>();

            try
            {
                var requestValidation = _validator.Validate(request);


                if (requestValidation.IsValid)
                {
                    response = await _repository.CreateProfile(request);
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



        public async Task<ResponseModel<bool>> DeleteProfile(int profileId)
        {
           var response = GenerateResponseModel<bool>();

            try
            {
                var profileDetails = await _repository.GetProfileDetails(profileId);

                if (profileDetails.ResponseCode == ResponseConstants.R00)
                {
                    response = await _repository.DeleteProfile(profileId);
                }
                else
                {

                    response = _mapper.Map<ResponseModel<bool>>(profileDetails);

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

        

        public async Task<ResponseModel<Profile>> GetListOfActiveProfiles()
        {
            var response = GenerateResponseModel<Profile>();

            

            try
            {

                response = await _repository.GetListOfActiveProfiles();

                var InactiveProfileFound = (response.ResponseData.FindAll(x => x.ProfileStatus != SystemWideConstants.ProfileStatuses.Active).Count > 0);

                if (InactiveProfileFound)
                {
                    response.ResponseCode = ResponseConstants.R01;
                    response.ResponseMessage = ResponseConstants.INACTIVE_PROFILE_FOUND;
                }
                else if(response.ResponseData.Count == 0 )
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

        public async Task<ResponseModel<Profile>> GetUnauthorisedProfiles()
        {
            var response = GenerateResponseModel<Profile>();



            try
            {

                response = await _repository.GetUnauthorisedProfiles();

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


        public Task<ResponseModel<Profile>> GetListOfDependentsByProfile(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetNextOfKindByProfile(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetOverAgeDependents()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetProfileDetails(int ProfileId)
        {
            throw new NotImplementedException();
        }



        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            throw new NotImplementedException();
        }


        #region Private Methods

        private ResponseModel<T> GenerateResponseModel<T>()
        {
            return new ResponseModel<T>
            {
                ResponseCode = ResponseConstants.R01
            };

        }

        #endregion
    }
}
