using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Muharaunda.Core.Contracts;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Application.Orchestration.Implementation;
using Munharaunda.Application.Orchestration.Services;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Domain.Contracts;
using Munharaunda.Infrastructure.Implementation;
using Munharaunda.Infrastructure.Implementation.CosmoDB;
using Munharaunda.Resources.Implementation;
using System;

namespace Munharaunda.Application
{
    public static class DependencyInjectionExtension
    {

        public static IServiceCollection AddResources(this IServiceCollection services, IConfiguration configuration)
        {
            AppSettings appSettings = new AppSettings(configuration);

            services.AddTransient<IAppSettings>(a => appSettings);

            #region MongoDB Definition
            var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDB"));

            var mongoClient = new MongoClient(settings);

            services.AddScoped<IMongoClient>(s => mongoClient);

            MongoDBProfileRepository profileRepository = new(mongoClient);

            MongoDBFuneralRepository funeralRespository = new(mongoClient);

            #endregion

            #region CosmoDB Definition

            //var _endpointUri = configuration["CosmoDbSettings:Endpoint"];

            //var _primaryKey = configuration["CosmoDbSettings:Key"];

            //var connectionString = configuration.GetConnectionString("CosmoDB");

            //var _dbId = configuration["CosmoDbSettings:DbId"];

            //var cosmoDBClient = new CosmosClient(connectionString);

            //var cosmoDB = cosmoDBClient.GetDatabase(_dbId);

            //services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //services.AddScoped<Database>(s => cosmoDB);

            //CosmoDBProfileRepository profileRepository = new CosmoDBProfileRepository(cosmoDB);

            //CosmoDBFuneralRepository funeralRespository = new CosmoDBFuneralRepository(cosmoDB);

            #endregion

            services.AddScoped<IProfile>(r => profileRepository);

            services.AddScoped<ProfileValidator>(s => new ProfileValidator(appSettings, profileRepository));

            

            services.AddScoped<IFuneralRepository>(x => funeralRespository);

            services.AddScoped<FuneralValidator>(s => new FuneralValidator(appSettings, funeralRespository));

            services.AddScoped<IProfileService, ProfileService>();

            services.AddScoped<IFuneralService, FuneralService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            return services;
        }
    }
}
