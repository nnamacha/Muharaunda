using Muharaunda.Core.Contracts;
using System;

namespace Munharaunda.Application.Orchestration.Contracts
{
    public interface IProfileService : IProfile
    {

        DateTime CalculateProfileActivationDate();


    }
}
