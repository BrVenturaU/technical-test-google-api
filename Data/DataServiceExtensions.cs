using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public static class DataServiceExtensions
    {
        public static void ConfigureDataServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("default"), builder => builder.MigrationsAssembly("Data"));
            });
            services.ConfigureIdentity();
        }

        private static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentityCore<User>(setup =>
            {
                setup.Password.RequireDigit = true;
                setup.Password.RequireLowercase = false;
                setup.Password.RequireUppercase = false;
                setup.Password.RequireNonAlphanumeric = false;
                setup.Password.RequiredLength = 10;
                setup.User.RequireUniqueEmail = true;
            });

            builder = new IdentityBuilder(builder.UserType, typeof(IdentityRole), builder.Services);
            builder.AddEntityFrameworkStores<DataContext>();
        }
    }
}
