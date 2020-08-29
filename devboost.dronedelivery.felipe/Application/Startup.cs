using devboost.dronedelivery.felipe.DTO.Constants;
using devboost.dronedelivery.felipe.DTO.Models;
using devboost.dronedelivery.felipe.EF.Data;
using devboost.dronedelivery.felipe.Security;
using devboost.dronedelivery.felipe.Security.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace devboost.dronedelivery.felipe
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingletons(Configuration);
            services.AddAuth(Configuration);
            services.AddControllers();
            services.AddSwagger();
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            ApplicationDbContext context,
            UserManager<Cliente> userManager,
            RoleManager<IdentityRole> roleManager,
            ISecurityClientProvider securityClientProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            new IdentityInitializer(context, userManager, roleManager, securityClientProvider).Initialize().Wait();

            // Swagger
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.RoutePrefix = string.Empty;
                   c.SwaggerEndpoint(ProjectConsts.SWAGGERFILE_PATH, ProjectConsts.PROJECT_NAME + ProjectConsts.API_VERSION);
               });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
