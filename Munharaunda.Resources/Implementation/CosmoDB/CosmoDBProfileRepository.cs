using Microsoft.Azure.Cosmos;
using Muharaunda.Core.Constants;
using Muharaunda.Core.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using System;
using System.Threading.Tasks;
using Profile = Muharaunda.Core.Models.Profile;

namespace Munharaunda.Infrastructure.Implementation
{
    public class CosmoDBProfileRepository : IProfileRepository
    {
        private readonly Database _dataBase;
        private readonly string containerId;
        private readonly Container _container;

        public CosmoDBProfileRepository(Database dataBase)
        {

            _dataBase = dataBase ?? throw new ArgumentNullException(nameof(dataBase));


            containerId = "Profile";

            _container = _dataBase.GetContainer(containerId);

        }
        public Task<ResponseModel<bool>> AuthoriseProfileAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckPersonIsUnique(CreateProfileRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<ProfileBase>> CreateProfileAsync(CreateProfileRequest request, bool checkUnique = false)
        {


            request.id = Guid.NewGuid();

            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            try
            {


                var result = await _container.CreateItemAsync<CreateProfileRequest>(request);


                response.ResponseCode = ResponseConstants.R00;
                response.ResponseMessage = ResponseConstants.R00Message;
                response.ResponseData.Add(result.Resource);



            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
            }

            return response;

        }

        public Task<ResponseModel<bool>> DeleteProfileAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Muharaunda.Core.Models.Profile>> GetListOfActiveProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetListOfDependentsByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetNextOfKindByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetProfileDetailsAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<Profile>> GetUnauthorisedProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateProfileAsync(Profile profile)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateProfileStatusAsync(int profileId, SystemWideConstants.ProfileStatuses newStatus)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            response.ResponseData.Add(true);

            await Task.CompletedTask;

            return response;
        }
    }
}
