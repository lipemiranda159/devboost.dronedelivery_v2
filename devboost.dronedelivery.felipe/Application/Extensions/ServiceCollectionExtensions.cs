using devboost.dronedelivery.felipe.DTO.Constants;
using devboost.dronedelivery.felipe.DTO.Models;
using devboost.dronedelivery.felipe.EF.Data;
using devboost.dronedelivery.felipe.EF.Repositories;
using devboost.dronedelivery.felipe.EF.Repositories.Interfaces;
using devboost.dronedelivery.felipe.Facade;
using devboost.dronedelivery.felipe.Facade.Interface;
using devboost.dronedelivery.felipe.Security;
using devboost.dronedelivery.felipe.Security.Extensions;
using devboost.dronedelivery.felipe.Services;
using devboost.dronedelivery.felipe.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace devboost.dronedelivery.felipe
{
    /// <summary>
    /// Service collections extensions
    /// </summary>
    public static class ServiceCollectionExtensions
    {

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

        public static void AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            // Configurando o uso da classe de contexto para
            // acesso às tabelas do ASP.NET Identity Core
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("InMemoryDatabase"));

            // Ativando a utilização do ASP.NET Identity, a fim de
            // permitir a recuperação de seus objetos via injeção de
            // dependências
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Configurando a dependência para a classe de validação
            // de credenciais e geração de tokens
            services.AddScoped<AccessManager>();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(
                configuration.GetSection("TokenConfigurations"))
                    .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            // Aciona a extensão que irá configurar o uso de
            // autenticação e autorização via tokens
            services.AddJwtSecurity(
                signingConfigurations, tokenConfigurations);

            services.AddCors();

        }
        public static void AddSwagger(this IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(ProjectConsts.API_VERSION, new OpenApiInfo { Title = ProjectConsts.PROJECT_NAME, Version = API_VERSION });
                var xmlFile = Assembly.GetExecutingAssembly().GetName().Name + ProjectConsts.XML_EXTENSION;
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

    }


}
