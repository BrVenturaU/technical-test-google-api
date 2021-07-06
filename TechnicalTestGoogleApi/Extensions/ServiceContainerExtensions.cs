using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TechnicalTestGoogleApi.Extensions
{
    public static class ServiceContainerExtensions
    {
        public static void ConfigureAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureDataServices(configuration);
        }
    }
}
