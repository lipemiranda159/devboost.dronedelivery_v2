using devboost.dronedelivery.felipe.DTO.Constants;
using devboost.dronedelivery.felipe.DTO.Models;
using devboost.dronedelivery.felipe.EF.Data;
using devboost.dronedelivery.felipe.EF.Repositories;
using devboost.dronedelivery.felipe.EF.Repositories.Interfaces;
using devboost.dronedelivery.felipe.Facade;
using devboost.dronedelivery.felipe.Facade.Interface;
using devboost.dronedelivery.felipe.Security;
using devboost.dronedelivery.felipe.Security.Extensions;
using devboost.dronedelivery.felipe.Security.Interfaces;
using devboost.dronedelivery.felipe.Services;
using devboost.dronedelivery.felipe.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace devboost.dronedelivery.felipe
{
    /// <summary>
    /// Service collections extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        private const string TOKEN_CONFIGURATION = "TokenConfigurations";

        public static void AddSingletons(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDroneRepository, DroneRepository>();
            services.AddSingleton<IPedidoDroneRepository, PedidoDroneRepository>();
            services.AddSingleton<IPedidoService, PedidoService>();
            services.AddSingleton<IDroneService, DroneService>();
            services.AddSingleton<ICoordinateService, CoordinateService>();
            services.AddSingleton<IPedidoFacade, PedidoFacade>();
            services.AddSingleton<IDroneFacade, DroneFacade>();
            services.AddDbContext<DataContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString(ProjectConsts.CONNECTION_STRING_CONFIG)), ServiceLifetime.Singleton);

        }

        /// <summary>
        /// Add auth configuration in service collection
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString(ProjectConsts.CONNECTION_STRING_CONFIG)));

            services.AddIdentity<Cliente, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<AccessManager>();
            services.AddScoped<ISecurityClientProvider, SecurityClientProvider>();
            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                configuration.GetSection(TOKEN_CONFIGURATION))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);
            services.AddJwtSecurity(
                signingConfigurations, tokenConfigurations);

            services.AddCors();

        }
        /// <summary>
        /// Add swagger configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ProjectConsts.API_VERSION,
                    new OpenApiInfo
                    {
                        Title = ProjectConsts.PROJECT_NAME,
                        Version = ProjectConsts.API_VERSION
                    });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                 new OpenApiSecurityScheme
                 {
                   Reference = new OpenApiReference
                   {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                   }
                  },
                  new string[] { }
                }});

                var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ProjectConsts.XML_EXTENSION;
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

    }


}
