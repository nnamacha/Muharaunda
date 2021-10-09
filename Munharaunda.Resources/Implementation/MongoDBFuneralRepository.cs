using MongoDB.Driver;
using Muharaunda.Core.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Dtos;
using System;
using System.Threading.Tasks;

namespace Munharaunda.Infrastructure.Implementation
{
    public class MongoDBFuneralRepository : IFuneralRepository
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase mongoDb;

        public MongoDBFuneralRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
            mongoDb = _mongoClient.GetDatabase("KnowledgeBank");
        }

        public Task<ResponseModel<bool>> AuthoriseFuneralAsync(int FuneralId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Funeral>> CreateFuneralAsync(Funeral request)
        {
            var response = CommonUtilites.GenerateResponseModel<Funeral>();

            try
            {
                await mongoDb.GetCollection<Funeral>("Funeral").InsertOneAsync(request);
                
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        private Task<bool> CheckFuneralIsUnique(CreateFuneralRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> DeleteFuneralAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            try
            {
                var filter = Builders<Funeral>.Filter.Eq("Profile.ProfileId", profileId);

                var result = await mongoDb.GetCollection<Funeral>("Funeral").DeleteOneAsync(filter);

                if (result.DeletedCount == 1)
                {
                    response.ResponseData.Add(true);
                }
                else
                {
                    throw new Exception($"Database failed to delete funeral for Profile id {profileId}");
                }

                
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public async Task<ResponseModel<Funeral>> GetFuneralDetailsByProfileIdAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<Funeral>();
            try
            {
                var filter = Builders<Funeral>.Filter.Eq("Profile.ProfileId", profileId);

                var funeral = await mongoDb.GetCollection<Funeral>("Funeral").FindAsync(filter).Result.FirstOrDefaultAsync();

                if (funeral != null)
                {
                    response.ResponseData.Add(funeral);
                }
                else
                {
                    response.ResponseCode = ResponseConstants.R404;
                }
               
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }
        
        public async Task<ResponseModel<Funeral>> GetFuneralDetailsByFuneralIdAsync(int funeralId)
        {
            var response = CommonUtilites.GenerateResponseModel<Funeral>();
            try
            {
                var filter = Builders<Funeral>.Filter.Eq("FuneralId", funeralId);

                var funeral = await mongoDb.GetCollection<Funeral>("Funeral").FindAsync(filter).Result.FirstOrDefaultAsync();

                if (funeral != null)
                {
                    response.ResponseData.Add(funeral);
                }
                
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public Task<ResponseModel<Funeral>> GetListOfActiveFuneralsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<bool>> UpdateFuneralAsync(Funeral funeral)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();
            try
            {
                var filter = Builders<Funeral>.Filter.Eq(u => u.FuneralId, funeral.FuneralId);                

                var result = await mongoDb.GetCollection<Funeral>("Funeral").ReplaceOneAsync(filter, funeral);

                if (result.IsAcknowledged && result.ModifiedCount == 1)
                {
                    response.ResponseData.Add(true);
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
