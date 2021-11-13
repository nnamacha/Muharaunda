using MongoDB.Driver;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Muharaunda.Core.Constants.SystemWideConstants;
using Profile = Muharaunda.Core.Models.Profile;

namespace Munharaunda.Resources.Implementation
{
    public class MongoDBProfileRepository : IProfileRepository
    {

        private readonly IMongoDatabase mongoDb;
        private readonly FilterDefinitionBuilder<IProfileBase> filterBuilder;
        private readonly UpdateDefinitionBuilder<IProfileBase> updateBuilder;

        public MongoDBProfileRepository(IMongoClient client)
        {

            mongoDb = client.GetDatabase("KnowledgeBank");



            filterBuilder = Builders<IProfileBase>.Filter;

            updateBuilder = Builders<IProfileBase>.Update;
        }

        public async Task<ResponseModel<bool>> AuthoriseProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            try
            {
                var filter = filterBuilder.Eq("ProfileId", profileId);

                var update = updateBuilder.Set(u => u.ProfileStatus, Statuses.Active);

                var result = await mongoDb.GetCollection<IProfileBase>("Profile").UpdateOneAsync(filter, update);

                response.ResponseData.Add(result.MatchedCount == 1);


            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;
                response.ResponseMessage = ex.Message;
                response.ResponseData.Add(false);
            }

            return response;
        }

        public async Task<ResponseModel<IProfileBase>> CreateProfileAsync(IProfileBase request, bool checkUnique = false)
        {
            var response = CommonUtilites.GenerateResponseModel<IProfileBase>();

            try
            {
                if (checkUnique && !await CheckPersonIsUnique(request))
                {
                    response.ResponseCode = ResponseConstants.R400;

                    response.ResponseMessage = ResponseConstants.PROFILE_NOT_UNIQUE;

                    return response;
                }
                var currentProfilesCount = mongoDb.GetCollection<CreateProfileRequest>("Profile").AsQueryable().Count();

                request.ProfileId = currentProfilesCount++;

                await mongoDb.GetCollection<IProfileBase>("Profile").InsertOneAsync(request);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<bool> CheckPersonIsUnique(IProfileBase request)
        {
            var filter = filterBuilder.Where(u => u.FirstName == request.FirstName && u.Surname == request.Surname && u.DateOfBirth == request.DateOfBirth);

            var profiles = await mongoDb.GetCollection<IProfileBase>("Profile").FindAsync(filter).Result.ToListAsync();

            return profiles.Count == 0;
        }

        public async Task<ResponseModel<bool>> DeleteProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            try
            {
                var filter = filterBuilder.Eq("ProfileId", profileId);

                await mongoDb.GetCollection<IProfileBase>("Profile").DeleteOneAsync(filter);

                response.ResponseData.Add(true);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }



        public async Task<ResponseModel<IProfileBase>> GetListOfActiveProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<IProfileBase>();
            try
            {
                var filter = filterBuilder.Eq("ProfileStatus", 0);

                var profile = await mongoDb.GetCollection<IProfileBase>("Profile").FindAsync(filter).Result.ToListAsync();


                response.ResponseData = profile;
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<IProfileBase>> GetListOfDependentsByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<IProfileBase>();
            try
            {


                var filter = filterBuilder.Where(u => u.ProfileType == ProfileTypes.Dependent && u.NextOfKin == profileId);

                var profiles = await mongoDb.GetCollection<IProfileBase>("Profile").FindAsync(filter).Result.ToListAsync();


                response.ResponseData = profiles;
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<IProfileBase>> GetNextOfKindByProfileAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<IProfileBase>();
            try
            {


                var filter = filterBuilder.Eq("NextOfKind", profileId);

                var profiles = await mongoDb.GetCollection<IProfileBase>("Profile").FindAsync(filter).Result.ToListAsync();


                response.ResponseData = profiles;
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<IProfileBase>> GetProfileDetailsAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<IProfileBase>();
            try
            {
                var filter = Builders<IProfileBase>.Filter.Eq("ProfileId", profileId);

                var profile = await mongoDb.GetCollection<IProfileBase>("Profile").FindAsync(filter).Result.FirstOrDefaultAsync();


                response.ResponseData.Add(profile);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<IProfileBase>> GetUnauthorisedProfilesAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<IProfileBase>();
            try
            {
                var filter = Builders<IProfileBase>.Filter.Eq("ProfileStatus", 3);

                var profiles = await mongoDb.GetCollection<IProfileBase>("Profile").Find(filter).ToListAsync();


                response.ResponseData = profiles;
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<bool>> UpdateProfileAsync(Profile profile)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            try
            {
                var filter = Builders<IProfileBase>.Filter.Eq(u => u.ProfileId, profile.ProfileId);

                var update = updateBuilder.Set(u => u.ProfileStatus, Statuses.Active);

                var result = await mongoDb.GetCollection<IProfileBase>("Profile").ReplaceOneAsync(filter, profile);

                if (result.IsAcknowledged && result.ModifiedCount == 1)
                {
                    response.ResponseData.Add(true);
                }
                else
                {
                    response.ResponseCode = ResponseConstants.R400;

                    response.ResponseMessage = ResponseConstants.PROFILE_UPDATE_FAILED;

                    response.ResponseData.Add(false);
                }

            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;


        }

        public async Task<ResponseModel<bool>> ValidateIdNumber(string IdNumber)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            var filter = Builders<IProfileBase>.Filter.Eq("IdentificationNumber", IdNumber);

            var profilesCount = await mongoDb.GetCollection<IProfileBase>("Profile").Find(filter).CountDocumentsAsync();


            response.ResponseData.Add(profilesCount == 0);

            return response;
        }

        public async Task<ResponseModel<bool>> UpdateProfileStatusAsync(int profileId, Statuses newStatus)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            try
            {
                var filter = Builders<IProfileBase>.Filter.Eq(u => u.ProfileId, profileId);

                var update = updateBuilder.Set(u => u.ProfileStatus, newStatus);

                var result = await mongoDb.GetCollection<IProfileBase>("Profile").UpdateOneAsync(filter, update);

                if (result.IsAcknowledged && result.ModifiedCount == 1)
                {
                    response.ResponseData.Add(true);
                }
                else
                {
                    response.ResponseCode = ResponseConstants.R400;

                    response.ResponseMessage = ResponseConstants.PROFILE_UPDATE_FAILED;

                    response.ResponseData.Add(false);
                }

            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

       
    }
}
