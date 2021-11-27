using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Muharaunda.Core.Constants;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Models;
using Munharaunda.Infrastructure.Implementation.CosmoDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Profile = Muharaunda.Core.Models.Profile;

namespace Munharaunda.Infrastructure.Implementation
{
    public class CosmoDBProfileRepository : IProfileRepository
    {
        
        private readonly ICosmosUtilities _cosmosUtilities;
        private Container _container;
        private string containerId = "Profile";

        public CosmoDBProfileRepository(ICosmosUtilities cosmosUtilities)
        {            

            _cosmosUtilities = cosmosUtilities ?? throw new ArgumentNullException(nameof(cosmosUtilities));                                  

        }
        public async Task<ResponseModel<bool>> AuthoriseProfileAsync(int ProfileId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            _container =  await _cosmosUtilities.GetContainer(containerId);

            return response;
        }

        public async Task<bool> CheckPersonIsUnique(ProfileBase request)
        {
            var response = false;

            _container =  await _cosmosUtilities.GetContainer(containerId);

            return response;
        }

        public async Task CreateBulkProfilesAsync(List<ProfileBase> profiles)
        {

            

            _container =  await _cosmosUtilities.GetContainer(containerId);
            
            var tasks = new List<Task>();

            foreach (var profile in profiles)
            {
                profile.Pk = profile.ProfileId.ToString();
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
            _container =  await _cosmosUtilities.GetContainer(containerId);
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

        public async Task<ResponseModel<bool>> DeleteProfileAsync(int ProfileId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            _container =  await _cosmosUtilities.GetContainer(containerId);
             return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetListOfActiveProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            _container =  await _cosmosUtilities.GetContainer(containerId);
            try
            {

                _container = await _cosmosUtilities.GetContainer(containerId);

                QueryDefinition query = new QueryDefinition(
                   "select * from Profile WHERE Profile.ProfileStatus = @ProfileStatus")
                   .WithParameter("@ProfileStatus", SystemWideConstants.Statuses.Active);


                var iterator = _container.GetItemQueryIterator<ProfileBase>(query);


                var profiles = await iterator.ReadNextAsync();

                if (profiles.Count == 0)
                {
                    response.ResponseCode = ResponseConstants.R404;

                    response.ResponseMessage = ResponseConstants.RECORD_NOT_FOUND;

                    return response;
                }

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

        public async Task<ResponseModel<ProfileBase>> GetListOfDependentsByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            _container =  await _cosmosUtilities.GetContainer(containerId);

            return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetNextOfKindByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            _container =  await _cosmosUtilities.GetContainer(containerId);

            return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetProfileDetailsAsync(int ProfileId)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            _container =  await _cosmosUtilities.GetContainer(containerId);

            QueryDefinition query = new QueryDefinition(
               "select * from Profile WHERE Profile.ProfileId = @ProfileId")
               .WithParameter("@ProfileId", ProfileId);           
            

            var iterator = _container.GetItemQueryIterator<ProfileBase>(query, requestOptions: new QueryRequestOptions()
            {
                PartitionKey = new PartitionKey(ProfileId.ToString()),
                MaxItemCount = 1
            });

            
            var profiles = await iterator.ReadNextAsync();

            if (profiles.Count > 1)
            {
                throw new Exception($"Duplicate Profiles for ProfileID {ProfileId}");
            }

            foreach (var profile in profiles)
            {
                response.ResponseData.Add(profile);
            }

            return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetUnauthorisedProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            _container =  await _cosmosUtilities.GetContainer(containerId);
            return response;
        }

        public async Task UpdateBulkProfilesAsync(List<ProfileBase> profiles)
        {
            List<Task> tasks = new();

            foreach (var profile in profiles)
            {

                

                Task task = _container.ReplaceItemAsync<ProfileBase>(profile, profile.id.ToString(), new PartitionKey(profile.Pk));

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

        public async Task<ResponseModel<bool>> UpdateProfileAsync(Profile profile)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            _container =  await _cosmosUtilities.GetContainer(containerId);
            return response;
        }

        public async Task<ResponseModel<bool>> UpdateProfileStatusAsync(int profileId, SystemWideConstants.Statuses newStatus)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            _container =  await _cosmosUtilities.GetContainer(containerId);

            var dbResponse =  await GetProfileDetailsAsync(profileId);

            var profile = dbResponse.ResponseData[0];

            profile.ProfileStatus = newStatus;

            ItemResponse<ProfileBase> replaceResponse = await _container.ReplaceItemAsync<ProfileBase>(profile, profile.id.ToString(), new PartitionKey(profile.Pk));

            if (replaceResponse.StatusCode != System.Net.HttpStatusCode.OK)
            {
                response.ResponseMessage = ResponseConstants.FAILED_PROFILE_STATUS_UPDATE;

                response.ResponseCode = ResponseConstants.R01;
            }

            return response;
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
