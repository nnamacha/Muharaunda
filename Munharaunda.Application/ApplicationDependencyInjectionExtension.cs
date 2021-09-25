using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muharaunda.Core.Contracts;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Application.Orchestration.Implementation;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Resources.Implementation;
using System;

namespace Munharaunda.Application
{
    public static class ApplicationDependencyInjectionExtension
    {
        public static IServiceCollection AppApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettings appSettings = new AppSettings(configuration);

            ProfileRespository profileRespository = new ProfileRespository();

            services.AddTransient<IAppSettings>(a => appSettings);
            services.AddScoped<IProfileRespository>(r => profileRespository);
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ProfileValidator>(s => new ProfileValidator(appSettings, profileRespository));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
