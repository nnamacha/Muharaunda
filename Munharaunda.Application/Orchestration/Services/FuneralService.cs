using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using System;
using System.Threading.Tasks;

namespace Munharaunda.Application.Orchestration.Services
{
    public class FuneralService : IFuneralService
    {
        private readonly IFuneralRepository _funeralRepository;
        private readonly FuneralValidator _validator;
        private readonly IAppSettings _appSettings;
        private readonly IProfile _profileRepository;

        public FuneralService(IFuneralRepository funeralRepository, FuneralValidator validator, IAppSettings appSettings, IProfile profileRepository)
        {
            _funeralRepository = funeralRepository;
            _validator = validator;
            _appSettings = appSettings;
            _profileRepository = profileRepository;
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

                var getProfileResponse = await _profileRepository.GetProfileDetailsAsync(request.ProfileId);

                if (getProfileResponse.ResponseCode == ResponseConstants.R00 && getProfileResponse.ResponseData.Count == 1)
                {
                    var funeral = new Funeral()
                    {
                        //Profile = getProfileResponse.ResponseData[0],
                        DateOfDeath = request.DateOfDeath,
                        Address = request.Address,
                        Created = DateTime.Now
                    };

                    var requestValidation = _validator.Validate(funeral);

                    if (requestValidation.IsValid)
                    {

                        response = await _funeralRepository.CreateFuneralAsync(funeral);

                        if (response.ResponseCode == ResponseConstants.R00)
                        {
                            await _profileRepository.UpdateProfileStatusAsync(request.ProfileId, Muharaunda.Core.Constants.SystemWideConstants.Statuses.Deceased);

                            var createdFuneral = await _funeralRepository.GetFuneralDetailsByProfileIdAsync(request.ProfileId);

                            if (createdFuneral.ResponseCode == ResponseConstants.R00)
                            {
                                response.ResponseData = createdFuneral.ResponseData;
                            }
                        }


                    }
                    else
                    {
                        response.ResponseMessage = ResponseConstants.CREATE_FUNERAL_REQUEST_INVALID;

                        foreach (var error in requestValidation.Errors)
                        {
                            response.Errors.Add(error.ErrorMessage);
                        }


                    }

                }
                else
                {
                    response.ResponseCode = ResponseConstants.R404;
                    response.ResponseMessage = ResponseConstants.PROFILE_NOT_FOUND;

                }



            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;
                response.ResponseMessage = ex.Message;


            }

            return response;
        }

        public async Task<ResponseModel<bool>> DeleteFuneralAsync(int profileId)
        {
            return await _funeralRepository.DeleteFuneralAsync(profileId);
        }



        public async Task<ResponseModel<Funeral>> GetFuneralDetailsByFuneralIdAsync(int funeralId)
        {
            return await _funeralRepository.GetFuneralDetailsByFuneralIdAsync(funeralId);
            
        }

        public async Task<ResponseModel<Funeral>> GetFuneralDetailsByProfileIdAsync(int profileId)
        {
            return await _funeralRepository.GetFuneralDetailsByProfileIdAsync(profileId);
        }

        public async Task<ResponseModel<Funeral>> GetListOfActiveFuneralsAsync()
        {
            var response = CommonUtilites.GenerateResponseModel<Funeral>();

            try
            {

                response = await _funeralRepository.GetListOfActiveFuneralsAsync();

                if (response.ResponseData.Count == 0)
                {
                    response.ResponseCode = ResponseConstants.R404;
                }


                return response;
            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;
                response.ResponseMessage = ex.Message;
                return response;

            }
        }

        public async  Task<ResponseModel<bool>> UpdateFuneralAsync(Funeral funeral)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            funeral.Updated = DateTime.Now;

            response = await _funeralRepository.UpdateFuneralAsync(funeral);

            if (response.ResponseCode == ResponseConstants.R00 && response.ResponseData[0] == false)
            {
                response.ResponseCode = ResponseConstants.R400;
            }

            return response;

        }
    }
}
