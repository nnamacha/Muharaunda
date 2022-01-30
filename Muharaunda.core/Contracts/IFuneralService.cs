using Muharaunda.Core.Models;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Domain.Contracts
{
    public interface IFuneralService
    {
        public Task<ResponseModel<Funeral>> CreateFuneralAsync(CreateFuneralRequest request);
        public Task<ResponseModel<bool>> DeleteFuneralAsync(int funeralId);
        public Task<ResponseModel<Funeral>> GetFuneralDetailsByProfileIdAsync(int profileId);
        public Task<ResponseModel<Funeral>> GetFuneralDetailsByFuneralIdAsync(string funeralId);
        public Task<ResponseModel<bool>> AuthoriseFuneralAsync(int funeralId);
        public Task<ResponseModel<Funeral>> GetListOfActiveFuneralsAsync();
        public Task<ResponseModel<bool>> UpdateFuneralAsync(Funeral funeral);
        public Task<ResponseModel<bool>> UpdateProfiles(string funeralId);
    }
}
    