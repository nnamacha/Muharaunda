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

                response.ResponseCode = ResponseConstants.R99;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        private Task<bool> CheckFuneralIsUnique(CreateFuneralRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> DeleteFuneralAsync(int FuneralId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Funeral>> GetFuneralDetailsByProfileIdAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<Funeral>();
            try
            {
                var filter = Builders<Funeral>.Filter.Eq("Profile.ProfileId", profileId);

                var funeral = await mongoDb.GetCollection<Funeral>("Funeral").FindAsync(filter).Result.FirstOrDefaultAsync();


                response.ResponseData.Add(funeral);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;

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


                response.ResponseData.Add(funeral);
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R99;

                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public Task<ResponseModel<Funeral>> GetListOfActiveFuneralsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<bool>> UpdateFuneralAsync(Funeral Funeral)
        {
            throw new NotImplementedException();
        }
    }
}
