using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseAccess.Repository;
using DatabaseAccess.SpExecuters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestClient;

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

                options.AddPolicy("PharmacyAdminProfile", policy =>
                {
                    policy.RequireClaim("current_profile", "PharmacyAdmin");
                });

                options.AddPolicy("CanUpdateInstitution", policy =>
                {
                    policy.RequireClaim("current_profile",
                        new[]
                        {
                            "PharmacyAdmin","HospitalAdmin", "MinistryWorker", "Admin"
                        });
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

                options.AddPolicy("HasProfile", policy =>
                {
                    policy.RequireClaim("current_profile",
                        new[]
                        {
                            "Doctor","Pharmacist","MinistryWorker","Patient",
                            "Admin","HospitalAdmin"
                        });
                });
            });

            this.AddSingletons(services);
            this.AddTransients(services);
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

        /// <summary>
        /// Adds singletons
        /// </summary>
        /// <param name="services">Services</param>
        private void AddSingletons(IServiceCollection services)
        {
            // adding singletons
            services.AddSingleton(new MapInfo(this.Configuration["Mappers:Institutions"]));
            services.AddSingleton(new SpExecuter(this.Configuration.GetConnectionString("InstitutionDB")));
           // services.AddSingleton(new Client(""));
        }

        /// <summary>
        /// Adds transients
        /// </summary>
        /// <param name="services">Services</param>
        private void AddTransients(IServiceCollection services)
        {
            // adding transients
            services.AddTransient(typeof(DataManager));
        }
    }
}
