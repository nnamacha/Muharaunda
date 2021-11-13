﻿using Microsoft.Azure.Cosmos;
using Muharaunda.Core.Constants;
using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using System;
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

        public Task<bool> CheckPersonIsUnique(IProfileBase request)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<IProfileBase>> CreateProfileAsync(IProfileBase request, bool checkUnique = false)
        {


            request.id = Guid.NewGuid();

            var response = CommonUtilites.GenerateResponseModel<IProfileBase>();

            try
            {


                var result = await _container.CreateItemAsync<IProfileBase>(request);


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

        public Task<ResponseModel<IProfileBase>> GetListOfActiveProfilesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IProfileBase>> GetListOfDependentsByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IProfileBase>> GetNextOfKindByProfileAsync(int profileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IProfileBase>> GetProfileDetailsAsync(int ProfileId)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseModel<IProfileBase>> GetUnauthorisedProfilesAsync()
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
