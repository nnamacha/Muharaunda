using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Muharaunda.Core.Contracts;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Application.Orchestration.Implementation;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Resources.Implementation;
using System;

namespace Munharaunda.Application
{
    public static class DependencyInjectionExtension
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettings appSettings = new AppSettings(configuration);

            SqlProfileRespository profileRespository = new SqlProfileRespository();

            services.AddTransient<IAppSettings>(a => appSettings);
            services.AddScoped<IProfileRespository>(r => profileRespository);
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ProfileValidator>(s => new ProfileValidator(appSettings, profileRespository));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }

        public static IServiceCollection AddResources(this IServiceCollection services, IConfiguration configuration)
        {
            var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDB"));
            services.AddScoped<IMongoClient>(s => new MongoClient(settings));
            services.AddScoped<IProfileRespository,MongoDBRepository>();
            return services;
        }
    }
}
