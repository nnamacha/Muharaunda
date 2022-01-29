using Microsoft.Azure.Cosmos;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Dtos;
using Munharaunda.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Infrastructure.Implementation.CosmoDB
{
    public class CosmoDBFuneralRepository : IFuneralRepository
    {
        
        private readonly ICosmosUtilities _cosmosUtilities;
        private readonly IProfileRepository _profileRepository;
        private readonly string containerId = "Funeral";
        

        public CosmoDBFuneralRepository( ICosmosUtilities cosmosUtilities, IProfileRepository profileRepository)
        {
            _cosmosUtilities = cosmosUtilities ?? throw new ArgumentNullException(nameof(cosmosUtilities));

            _profileRepository = profileRepository ?? throw new ArgumentNullException(nameof(profileRepository));            
        }
        public Task<ResponseModel<bool>> AuthoriseFuneralAsync(int FuneralId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Funeral>> CreateFuneralAsync(Funeral funeral)
        {
            

           var  _container = await _cosmosUtilities.GetContainer(containerId);

            

            var response = CommonUtilites.GenerateResponseModel<Funeral>();

            try
            {
                funeral.Pk = funeral.Profile.ProfileId.ToString();
                
                response.ResponseCode = ResponseConstants.R00;

                response.ResponseMessage = ResponseConstants.R00Message;

                response.ResponseData.Add(funeral);



            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;
                response.ResponseMessage = ex.Message;
            }

            return response;
        }

        public Task<ResponseModel<bool>> DeleteFuneralAsync(int FuneralId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Funeral>> GetFuneralDetailsByFuneralIdAsync(string funeralId)
        {

            var id = Guid.Parse(funeralId);
            var response = CommonUtilites.GenerateResponseModel<Funeral>();

            var _container = await _cosmosUtilities.GetContainer(containerId);

            QueryDefinition query = new QueryDefinition(
               "select * from c WHERE c.id = @id")
               .WithParameter("@id", id);


            var iterator = _container.GetItemQueryIterator<Funeral>(query);


            var funerals = await iterator.ReadNextAsync();

            if (funerals.Count > 1)
            {
                throw new NotSupportedException($"Duplicate funeral with funeralId {funeralId}");
            }

            foreach (var funeral in funerals)
            {
                response.ResponseData.Add(funeral);
            }

            return response;
        }

        public async Task<ResponseModel<Funeral>> GetFuneralDetailsByProfileIdAsync(int profileId)
        {
            var response = CommonUtilites.GenerateResponseModel<Funeral>();

            var _container = await _cosmosUtilities.GetContainer(containerId);

            QueryDefinition query = new QueryDefinition(
               "select * from c WHERE c.Profile.ProfileId = @ProfileId")
               .WithParameter("@ProfileId", profileId);


            var iterator = _container.GetItemQueryIterator<Funeral>(query, requestOptions: new QueryRequestOptions()
            {
                PartitionKey = new PartitionKey(profileId.ToString()),
                
            });


            var funerals = await iterator.ReadNextAsync();

            if (funerals.Count > 1)
            {
                throw new Exception($"Duplicate funeral with ProfileID {profileId}");
            }

            foreach (var funeral in funerals)
            {
                response.ResponseData.Add(funeral);
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

        public async Task<ResponseModel<bool>> UpdateProfiles(string funeralId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            try
            {

                List<ProfileBase> profiles = new();

                var _container = await _cosmosUtilities.GetContainer(containerId);

                var dbResponse = await _profileRepository.GetListOfActiveProfilesAsync();

                var funeralDetails = await GetFuneralDetailsByFuneralIdAsync(funeralId);

                if (funeralDetails.ResponseCode != ResponseConstants.R00)
                {
                    response.ResponseMessage = funeralDetails.ResponseMessage;

                    response.ResponseCode = funeralDetails.ResponseCode;

                    response.ResponseData.Add(false);

                    return response;

                }

                var funeralPayment = new FuneralPayment()
                {
                    Paid = false,
                    Amount = 0,
                    Funeral = funeralDetails.ResponseData[0]
                };

                foreach (var profile in dbResponse.ResponseData)
                {
                    profile.FuneralPayments.Add(funeralPayment);

                    profiles.Add(profile);
                }

                await _profileRepository.UpdateBulkProfilesAsync(profiles);
            }
            catch (Exception ex)
            {

                response.ResponseMessage = ex.Message;

                response.ResponseCode = ResponseConstants.R500;               

               
            }

            return response;

        }
    }
}
