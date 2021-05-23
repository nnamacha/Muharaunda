using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Muharaunda.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munharaunda.Application
{
    public static class ApplicationDependencyInjectionExtension
    {
        public static IServiceCollection AppApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IAppSettings, AppSettings>(a => new AppSettings(configuration));
            return services;
        }
    }
}
