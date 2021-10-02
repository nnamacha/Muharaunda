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
using static Muharaunda.Core.Constants.SystemWideConstants;
using Profile = Muharaunda.Core.Models.Profile;

namespace Munharaunda.Resources.Implementation
{
    public class MongoDBRepository : IProfileRespository
    {

        private readonly IMongoDatabase mongoDb;
        private readonly IMapper _mapper;
        private readonly FilterDefinitionBuilder<Profile> filterBuilder;

          

        public MongoDBRepository(IMongoClient client, IMapper mapper)
        {

            mongoDb = client.GetDatabase("KnowledgeBank");

            _mapper = mapper;

            filterBuilder = Builders<Profile>.Filter;
        }

        public ResponseModel<bool> AuthoriseProfile(int ProfileId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            try
            {
                var updateBuilder = Builders<Profile>.Update;

                updateBuilder.Set(u => u.ProfileStatus, ProfileStatuses.Active);

                response.ResponseData.Add(true);


            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
                response.ResponseData.Add(false);
            }

            return response;
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
                var filter = filterBuilder.Eq("ProfileId", profileId);

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
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            try
            {
                var filter = filterBuilder.Eq("ProfileStatus", 0);

                var profile = _mapper.Map<ProfileBase>(await mongoDb.GetCollection<Profile>("Profile").FindAsync(filter).Result.ToListAsync());


                response.ResponseData.Add(profile);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetListOfDependentsByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            try
            {
                

                var filter = filterBuilder.Where(u => u.ProfileType == ProfileTypes.Dependent && u.NextOfKin == profileId);

                var profile = _mapper.Map<ProfileBase>(await mongoDb.GetCollection<Profile>("Profile").FindAsync(filter).Result.ToListAsync());


                response.ResponseData.Add(profile);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<ProfileBase>> GetNextOfKindByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<ProfileBase>();
            try
            {


                var filter = filterBuilder.Eq("NextOfKind", profileId);

                var profile = _mapper.Map<ProfileBase>(await mongoDb.GetCollection<Profile>("Profile").FindAsync(filter).Result.ToListAsync());


                response.ResponseData.Add(profile);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
            }

            return response;
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
            var filter = Builders<Profile>.Filter.Eq("IdentificationNumber", IdNumber);

            var profilesCount = await mongoDb.GetCollection<Profile>("Profile").Find(filter).CountDocumentsAsync();


            response.ResponseData.Add(profilesCount == 0);

            return response;
        }


    }
}
