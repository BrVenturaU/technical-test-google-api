using Contracts.Services.User;
using Microsoft.Extensions.DependencyInjection;
using Service.Services.UserServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public static class ProjectServiceExtensions
    {
        public static void ConfigureProjectServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
        }
    }
}
