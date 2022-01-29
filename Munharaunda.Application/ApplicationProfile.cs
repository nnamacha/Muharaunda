using Muharaunda.Core.Models;
using Muharaunda.Domain.Models;
using Munharaunda.Core.Dtos;
using Munharaunda.Domain.Models;

namespace Munharaunda.Application
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreateProfileRequest, Muharaunda.Core.Models.Profile>().ReverseMap();
            CreateMap<ProfileBase, Muharaunda.Core.Models.Profile>().ReverseMap();
            CreateMap<object, ProfileBase>().ReverseMap();
            CreateMap<CreateProfileRequest, CosmosProfile>();

        }
    }
}
