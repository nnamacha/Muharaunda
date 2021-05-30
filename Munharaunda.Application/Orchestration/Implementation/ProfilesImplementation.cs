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

namespace Munharaunda.Application.Orchestration.Implementation
{
    public class ProfilesImplementation : IProfilesImplementation
    {
        private readonly IProfileRespository _repository;
        private readonly ProfileValidator _validator;

        public ProfilesImplementation(IProfileRespository repository, ProfileValidator validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public Task<ResponseModel<Profile>> AuthoriseProfile(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Profile>> CreateProfile(CreateProfileRequest request)
        {
            //Default Failure
            ResponseModel<Profile> response = new ResponseModel<Profile>
            {
                ResponseCode = ResponseConstants.R01
            };

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



        public Task<ResponseModel<Profile>> DeleteProfile(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetListOfActiveProfiles()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetListofAuthorisedProfiles()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetListOfDependentsByProfile(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetNextOfKindByProfile(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetOverAgeDependents()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetProfileDetails(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<List<Profile>>> GetUnauthorisedProfiles()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            throw new NotImplementedException();
        }
    }
}
