using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Resources.Implementation
{
    public class MongoDBRepository: IProfileRespository
    {
        private readonly IConfiguration _configuration;
        private IMongoDatabase mongoDb;

        public MongoDBRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            var settings = MongoClientSettings.FromConnectionString(_configuration.GetConnectionString("MongoDB"));
            var client = new MongoClient(settings);
            mongoDb = client.GetDatabase("KnowledgeBank");
        }

        public Task<ResponseModel<Profile>> AuthoriseProfileAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Profile>> CreateProfileAsync(CreateProfileRequest request)
        {
            var response = CommonUtilites.GenerateResponseModel<Profile>();

            try
            {
                await mongoDb.GetCollection<Profile>("Profile").InsertOneAsync(request);
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

        private IMongoDatabase GetClient()
        {
            var settings = MongoClientSettings.FromConnectionString(_configuration.GetConnectionString("MongoDB"));
            var client = new MongoClient(settings);
            IMongoDatabase database = client.GetDatabase("KnowledgeBank");
            return database;
        }

        public Task<ResponseModel<Profile>> GetListOfActiveProfilesAsync()
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

        public async Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            response.ResponseData.Add(true);

            return response;
        }
    }
}
