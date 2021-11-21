using Microsoft.Azure.Cosmos;
using Muharaunda.Core.Constants;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Models;
using System;
using System.Collections.Generic;
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

        public Task<bool> CheckPersonIsUnique(ProfileBase request)
        {
            throw new NotImplementedException();
        }

        public async Task CreateBulkProfilesAsync(List<ProfileBase> profiles)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            var tasks = new List<Task>();
            foreach (var profile in profiles)
            {
                var task = _container.CreateItemAsync<ProfileBase>(profile, new PartitionKey(profile.Pk));
                tasks.Add(task
                    .ContinueWith(t =>
                    {
                        if (t.Status != TaskStatus.RanToCompletion)
                        {
                            Console.WriteLine($"Error creating document : {t.Exception.Message} ");
                        }
                    }));
            }

            await Task.WhenAll(tasks);

            
        }



        public async Task<ResponseModel<ProfileBase>> CreateProfileAsync(ProfileBase request, bool checkUnique = false)
        {


            request.id = Guid.NewGuid();

            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            try
            {

                request.Pk = request.ProfileId.ToString();
                var result = await _container.CreateItemAsync<ProfileBase>(request, new PartitionKey(request.Pk));


                response.ResponseCode = ResponseConstants.R00;
                response.ResponseMessage = ResponseConstants.R00Message;
                response.ResponseData.Add(result.Resource);



            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;
                response.ResponseMessage = ex.Message;
            }

            return response;

        }

        public Task<ResponseModel<bool>> DeleteProfileAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<ProfileBase>> GetListOfActiveProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            try
            {
                var sql = "select * from c";

                var iterator =  _container.GetItemQueryIterator<CosmosProfile>(sql);

                var profiles = await iterator.ReadNextAsync();

                foreach (var profile in profiles)
                {
                    response.ResponseData.Add(profile);
                }
                
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public Task<ResponseModel<ProfileBase>> GetListOfDependentsByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<ProfileBase>> GetNextOfKindByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<ProfileBase>> GetProfileDetailsAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<ProfileBase>> GetUnauthorisedProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateProfileAsync(Profile profile)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateProfileStatusAsync(int profileId, SystemWideConstants.Statuses newStatus)
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
