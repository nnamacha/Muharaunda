using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Muharaunda.Core.Contracts;
using Muharaunda.Domain.Models;
using Munharaunda.Application;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Application.Orchestration.Implementation;
using Munharaunda.Application.Orchestration.Services;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Domain.Contracts;
using Munharaunda.Domain.Models;
using Munharaunda.Infrastructure.Implementation;
using Munharaunda.Infrastructure.Implementation.CosmoDB;
using Munharaunda.Resources.Implementation;
using System;

namespace Munharaunda.Api.Extensions
{
    public static class DependencyInjectionExtension
    {

        public static IServiceCollection AddResources(this IServiceCollection services, IConfiguration configuration)
        {
            Application.AppSettings appSettings = new(configuration);

            services.AddTransient((Func<IServiceProvider, Muharaunda.Core.Contracts.IAppSettings>)(a => appSettings));

            #region MongoDB Definition
            //var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDB"));

            //var mongoClient = new MongoClient(settings);

            //services.AddScoped<IMongoClient>(s => mongoClient);

            //MongoDBProfileRepository profileRepository = new(mongoClient);

            //MongoDBFuneralRepository funeralRespository = new(mongoClient);

            #endregion

            #region CosmoDB Definition

            var _endpointUri = configuration["CosmoDbSettings:Endpoint"];

            var _primaryKey = configuration["CosmoDbSettings:Key"];

            var connectionString = configuration.GetConnectionString("CosmoDB");

            var _dbId = configuration["CosmoDbSettings:DbId"];

            var cosmoDBClient = new CosmosClient(connectionString, new CosmosClientOptions { AllowBulkExecution = true });

            var cosmoDB = cosmoDBClient.GetDatabase(_dbId);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<Database>(s => cosmoDB);

            var cosmosUtils = new CosmosUtilities(cosmoDB, configuration);

            services.AddScoped<ICosmosUtilities>(s => new CosmosUtilities(cosmoDB, configuration));

            CosmoDBProfileRepository profileRepository = new(cosmosUtils);

            CosmoDBFuneralRepository funeralRespository = new(cosmosUtils, profileRepository);

            #endregion

            services.AddScoped<IProfile>(r => profileRepository);

            services.AddScoped<ProfileValidator>(s => new ProfileValidator(appSettings, profileRepository));            

            services.AddScoped<IFuneralRepository>(x => funeralRespository);

            services.AddScoped<FuneralValidator>(s => new FuneralValidator());

            services.AddScoped<IProfileService, ProfileService>();

            services.AddScoped<IFuneralService, FuneralService>();

            services.AddScoped<IProfileBase, CosmosProfile>();

            

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


            return services;
        }
    }
}
