﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Muharaunda.Core.Contracts;
using Munharaunda.Application.Orchestration.Contracts;
using Munharaunda.Application.Orchestration.Implementation;
using Munharaunda.Application.Orchestration.Services;
using Munharaunda.Application.Validators.Implementations;
using Munharaunda.Domain.Contracts;
using Munharaunda.Infrastructure.Implementation;
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

            var settings = MongoClientSettings.FromConnectionString(configuration.GetConnectionString("MongoDB"));

            var mongoClient = new MongoClient(settings);

            services.AddScoped<IMongoClient>(s => mongoClient);

            MongoDBProfileRepository profileRespository = new MongoDBProfileRepository(mongoClient);

            services.AddScoped<IProfile>(r => profileRespository);

            services.AddScoped<ProfileValidator>(s => new ProfileValidator(appSettings, profileRespository));

            MongoDBFuneralRepository funeralRespository = new MongoDBFuneralRepository(mongoClient);

            services.AddScoped<IFuneralRepository>(x => funeralRespository);

            services.AddScoped<FuneralValidator>(s => new FuneralValidator(appSettings, funeralRespository));

            services.AddScoped<IProfileService, ProfileService>();

            services.AddScoped<IFuneralService, FuneralService>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }
}
