using AutoMapper;
using Munharaunda.Application.Dtos;
using Munharaunda.Core.Dtos;
using Munharaunda.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Application
{
    class ApplicationProfile:Profile
    {
        public ApplicationProfile()
        {
            CreateMap<CreateProfileRequest, Muharaunda.Core.Models.Profile>().ReverseMap();
            CreateMap<ResponseModel<bool>, ResponseModel<Muharaunda.Core.Models.Profile>>().ReverseMap();

        }
    }
}
