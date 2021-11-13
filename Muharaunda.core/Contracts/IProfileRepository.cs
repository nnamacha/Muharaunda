using Muharaunda.Core.Contracts;
using Muharaunda.Domain.Models;
using Munharaunda.Core.Dtos;
using System.Threading.Tasks;

namespace Munharaunda.Domain.Contracts
{
    public interface IProfileRepository : IProfile
    {
        public Task<bool> CheckPersonIsUnique(IProfileBase request);
    }
}
