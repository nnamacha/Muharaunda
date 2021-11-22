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
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Profile = Muharaunda.Core.Models.Profile;

namespace Munharaunda.Infrastructure.Implementation
{
    public class CosmoDBProfileRepository : IProfileRepository
    {
        private readonly Database _dataBase;
        private readonly IConfiguration _configuration;
        private Container _container;

        public CosmoDBProfileRepository(Database dataBase, IConfiguration configuration)
        {

            _dataBase = dataBase ?? throw new ArgumentNullException(nameof(dataBase));
            _configuration = configuration;            

        }
        public async Task<ResponseModel<bool>> AuthoriseProfileAsync(int ProfileId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            await PrepareContainer();

            return response;
        }

        public async Task<bool> CheckPersonIsUnique(ProfileBase request)
        {
            var response = false;

            await PrepareContainer();

            return response;
        }

        public async Task CreateBulkProfilesAsync(List<ProfileBase> profiles)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            await PrepareContainer();
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
            await PrepareContainer();
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
            await PrepareContainer();
             return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetListOfActiveProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            await PrepareContainer();
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

        public async Task<ResponseModel<ProfileBase>> GetListOfDependentsByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            await PrepareContainer();

            return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetNextOfKindByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            await PrepareContainer();

            return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetProfileDetailsAsync(int ProfileId)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            await PrepareContainer();
            return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetUnauthorisedProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            await PrepareContainer();
            return response;
        }

        public async Task<ResponseModel<bool>> UpdateProfileAsync(Profile profile)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            await PrepareContainer();
            return response;
        }

        public async Task<ResponseModel<bool>> UpdateProfileStatusAsync(int profileId, SystemWideConstants.Statuses newStatus)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            await PrepareContainer();
            return response;
        }

        public async Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            response.ResponseData.Add(true);

            await Task.CompletedTask;

            return response;
        }

        private async Task PrepareContainer()
        {
            
            var containerProperties = new ContainerProperties()
            {
                Id = _configuration["CosmosDB:ContainerId"],
                PartitionKeyPath = _configuration["CosmosDB:PartitionKeyPath"],
                
            };
            _container = await _dataBase.CreateContainerIfNotExistsAsync(containerProperties);
        }
    }
}
