using AutoMapper;
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
using System.Threading.Tasks;
using Profile = Muharaunda.Core.Models.Profile;

namespace Munharaunda.Resources.Implementation
{
    public class MongoDBRepository : IProfileRespository
    {

        private readonly IMongoDatabase mongoDb;
        private readonly IMapper _mapper;

        public MongoDBRepository(IMongoClient client, IMapper mapper)
        {

            mongoDb = client.GetDatabase("KnowledgeBank");
            _mapper = mapper;
        }

        public Task<ResponseModel<ProfileBase>> AuthoriseProfileAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<ProfileBase>> CreateProfileAsync(CreateProfileRequest request)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();

            try
            {
                var currentProfilesCount = mongoDb.GetCollection<CreateProfileRequest>("Profile").AsQueryable().Count();

                request.ProfileId = currentProfilesCount++;





                await mongoDb.GetCollection<CreateProfileRequest>("Profile").InsertOneAsync(request);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            try
            {
                var filter = Builders<Muharaunda.Core.Models.Profile>.Filter.Eq("ProfileId", profileId);

                await mongoDb.GetCollection<Profile>("Profile").DeleteOneAsync(filter);

                response.ResponseData.Add(true);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }



        public async Task<ResponseModel<ProfileBase>> GetListOfActiveProfilesAsync()
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

        public async Task<ResponseModel<ProfileBase>> GetProfileDetailsAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            try
            {
                var filter = Builders<Profile>.Filter.Eq("ProfileId", profileId);

                var profile = _mapper.Map<ProfileBase>(await mongoDb.GetCollection<Profile>("Profile").FindAsync(filter).Result.FirstOrDefaultAsync());


                response.ResponseData.Add(profile);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetUnauthorisedProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            try
            {
                var filter = Builders<Profile>.Filter.Eq("ProfileStatus", 3);

                var profiles = await mongoDb.GetCollection<Profile>("Profile").Find(filter).ToListAsync();


                response.ResponseData = _mapper.Map<List<ProfileBase>>(profiles);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            response.ResponseData.Add(true);

            return response;
        }


    }
}
