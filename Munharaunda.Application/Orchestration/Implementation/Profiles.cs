using Muharaunda.Core.Models;
using Munharaunda.Application.Contracts;
using Munharaunda.Application.Dtos;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Application.Orchestration.Implementation
{
    public class Profiles : IProfiles
    {
        private readonly IProfileRespository _repository;

        public Profiles(IProfileRespository repository)
        {
            _repository = repository;
        }
        public Task<ResponseModel<Profile>> AuthoriseProfile(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> CreateProfile(CreateProfileRequest request)
        {
            throw new NotImplementedException();
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
