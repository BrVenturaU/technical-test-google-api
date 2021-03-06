using Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechnicalTestGoogleApi.Filters;
using TechnicalTestGoogleApi.Utils;

namespace TechnicalTestGoogleApi.Extensions
{
    public static class ServiceContainerExtensions
    {
        public static void ConfigureAllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.ConfigureSwagger();
            services.ConfigureDataServices(configuration);
            services.ConfigureRepositoryServices();
            services.ConfigureJWT(configuration);
            services.ConfigureProjectServices();
            services.ConfigureExtraServices();
            services.ConfigureCorsPolicies();
        }

        private static void ConfigureCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(configuration =>
            {
                configuration.AddPolicy("WebClientAll", builder =>
                    builder.WithOrigins("https://technical-test-client.herokuapp.com", "http://localhost:8080")
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
        }

        private static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { 
                    Title = "TechnicalTestGoogleApi",
                    Version = "v1",
                    Description = "The TechnicalTest documentation for the API",
                    Contact = new OpenApiContact { Name = "Brandon Ventura", Email = "brandonventura16@gmail.com"}
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"Encabezado de autorización bajo el esquema Bearer. <br/>
                                    Coloque 'Bearer' [barra de espacio] y luego su token en la entrada de texto a continuación. <br/>
                                    <b>Ejemplo:</b> 'Bearer eyJhbGciOiJIUzR5ceyJ1bmlxdWVfbm...'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });
                
                c.OperationFilter<AuthOperationFilter>();
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

            });
        }

        private static void ConfigureExtraServices(this IServiceCollection services)
        {
            services.AddControllers(options => {
                options.Conventions.Add(new SwaggerVersionGroups());
                options.Filters.Add(typeof(ErrorsFilterAttribute));
            }).AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore)
                .ConfigureApiBehaviorOptions(options => {
                    options.InvalidModelStateResponseFactory = context => new BadRequestObjectResult( new ApiResponse((int)HttpStatusCode.BadRequest) {
                        Message = "Se produjeron uno o más errores de validación",
                        Body = ApiResponse.GetMessageList(context.ModelState)
                    });
                });
        }
        
        private static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = jwtSettings.GetSection("secret").Value;

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
              .AddJwtBearer(options => {
                  options.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,

                      ValidIssuer = jwtSettings.GetSection("issuer").Value,
                      ValidAudience = jwtSettings.GetSection("audience").Value,
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

                  };
               });
        }
    }
}
