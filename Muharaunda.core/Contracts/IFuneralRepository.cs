using Muharaunda.Core.Models;
using Munharaunda.Core.Models;
using Munharaunda.Domain.Dtos;
using System.Threading.Tasks;

namespace Munharaunda.Domain.Contracts
{
    public interface IFuneralRepository
    {
        public Task<ResponseModel<Funeral>> CreateFuneralAsync(Funeral request);
        public Task<ResponseModel<bool>> DeleteFuneralAsync(int FuneralId);
        public Task<ResponseModel<Funeral>> GetFuneralDetailsByProfileIdAsync(int profileId);
        public Task<ResponseModel<Funeral>> GetFuneralDetailsByFuneralIdAsync(int funeralId);
        public Task<ResponseModel<bool>> AuthoriseFuneralAsync(int FuneralId);
        public Task<ResponseModel<Funeral>> GetListOfActiveFuneralsAsync();
        public Task<ResponseModel<bool>> UpdateFuneralAsync(Funeral Funeral);
    }
}
