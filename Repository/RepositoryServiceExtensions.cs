using Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public static class RepositoryServiceExtensions
    {
        public static void ConfigureRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryManager, RepositoryManager>();
        }
    }
}
