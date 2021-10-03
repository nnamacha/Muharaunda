using Muharaunda.Core.Contracts;
using Munharaunda.Core.Dtos;
using System.Threading.Tasks;

namespace Munharaunda.Domain.Contracts
{
    public interface IProfileRepository : IProfile
    {
        public Task<bool> CheckPersonIsUnique(CreateProfileRequest request);
    }
}
