using FluentValidation;
using Muharaunda.Core.Contracts;
using Muharaunda.Core.Models;
using Munharaunda.Core.Constants;
using Munharaunda.Core.Models;
using Munharaunda.Core.Utilities;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Dtos;
using Munharaunda.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Munharaunda.Application.Orchestration.Services
{
    public class FuneralService : IFuneralService
    {
        private readonly IFuneralRepository _funeralRepository;
        private readonly IValidator<Funeral> _validator;
        private readonly IProfile _profileRepository;

        public FuneralService(IFuneralRepository funeralRepository, IValidator<Funeral> validator, IProfile profileRepository)
        {
            _funeralRepository = funeralRepository;
            _validator = validator;
            _profileRepository = profileRepository;
        }

        public Task<ResponseModel<bool>> AuthoriseFuneralAsync(int FuneralId)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseModel<Funeral>> CreateFuneralAsync(CreateFuneralRequest request)
        {
            var response = CommonUtilites.GenerateResponseModel<Funeral>();

            try
            {

                var getProfileResponse = await _profileRepository.GetProfileDetailsAsync(request.ProfileId);

                if (getProfileResponse.ResponseCode == ResponseConstants.R00 && getProfileResponse.ResponseData.Count == 0)
                {
                    response.ResponseCode = ResponseConstants.R404;
                    response.ResponseMessage = ResponseConstants.PROFILE_NOT_FOUND;
                    return response;

                }
                else if (getProfileResponse.ResponseCode != ResponseConstants.R00)
                {
                    response.ResponseCode= getProfileResponse.ResponseCode;
                    response.ResponseMessage = getProfileResponse.ResponseMessage;
                    return response;
                }



                var FuneralId = Guid.NewGuid();

                var funeral = new Funeral()
                {
                    id = FuneralId,
                    FuneralId = FuneralId,
                    Profile = getProfileResponse.ResponseData[0],
                    Address = request.Address,
                    DateOfDeath = request.DateOfDeath,
                    Audit = new Audit()
                    {
                        Created = DateTime.Now
                    },
                    Pk = request.ProfileId.ToString()
                };
                var requestValidation = _validator.Validate(funeral);

                if (!requestValidation.IsValid)
                {
                    response.ResponseCode = ResponseConstants.R01;

                    response.ResponseMessage = ResponseConstants.CREATE_FUNERAL_REQUEST_INVALID;

                    foreach (var error in requestValidation.Errors)
                    {
                        response.Errors.Add(error.ErrorMessage);
                    }
                    return response;
                }


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
                return response;








            }
            catch (Exception ex)
            {

                response.ResponseCode = ResponseConstants.R500;
                response.ResponseMessage = ex.Message;
                return response;


            }


        }

        public async Task<ResponseModel<bool>> DeleteFuneralAsync(int profileId)
        {
            return await _funeralRepository.DeleteFuneralAsync(profileId);
        }



        public async Task<ResponseModel<Funeral>> GetFuneralDetailsByFuneralIdAsync(string funeralId)
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

        public async Task<ResponseModel<bool>> UpdateFuneralAsync(Funeral funeral)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();

            funeral.Audit.Updated = DateTime.Now;

            response = await _funeralRepository.UpdateFuneralAsync(funeral);

            if (response.ResponseCode == ResponseConstants.R00 && response.ResponseData[0] == false)
            {
                response.ResponseCode = ResponseConstants.R400;
            }

            return response;

        }

        public async Task<ResponseModel<bool>> UpdateProfiles(string funeralId)
        {
            var response = CommonUtilites.GenerateResponseModel<bool>();


            return await _funeralRepository.UpdateProfiles(funeralId);


        }
    }
}
