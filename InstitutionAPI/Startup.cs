using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstitutionAPI
{
    /// <summary>
    /// Startup class for Institution API
    /// </summary>
    public class Startup
    {
        private IConfiguration Configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json").Build();

        private IConfiguration Credentials = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("MailCredentials.json").Build();

        /// <summary>
        /// Configures services.
        /// This method gets called by the runtime which uses this method to add services to the container.
        /// </summary>
        /// <param name="services">Services</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // adding MVC Core,authorization and JSON formatting
            services.AddMvcCore()
                    .AddAuthorization()
                    .AddJsonFormatters();

            // adding authentication info
            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.Authority = this.Configuration["Endpoints:AuthAPI"];
                        options.RequireHttpsMetadata = false;
                        options.ApiName = "InstitutionAPI";
                    });

            // adding policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PharmacistProfile", policy =>
                {
                    policy.RequireClaim("current_profile", "Pharmacist");
                });

                options.AddPolicy("InstitutionAdminProfile", policy =>
                {
                    policy.RequireClaim("current_profile",
                        new[]
                        {
                            "PharmacyAdmin","HospitalAdmin"
                        });
                });

                options.AddPolicy("HighLevel", policy =>
                {
                    policy.RequireClaim("current_profile",
                        new[]
                        {
                            "MinistryWorker","Admin"
                        });
                });
            });
        }

        /// <summary>
        /// Configures app and environment.
        /// This method gets called by the runtime which uses this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">App</param>
        /// <param name="env">Environment</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
