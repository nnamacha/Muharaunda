using Muharaunda.Core.Models;
using Munharaunda.Core.Dtos;

namespace Munharaunda.Application
{
    public class ApplicationProfile : AutoMapper.Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreateProfileRequest, Muharaunda.Core.Models.Profile>().ReverseMap();
            CreateMap<ProfileBase, Muharaunda.Core.Models.Profile>();

        }
    }
}
