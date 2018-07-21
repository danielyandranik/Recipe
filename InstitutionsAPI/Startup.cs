using System.IO;
using DatabaseAccess.Repository;
using DatabaseAccess.SpExecuters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstitutionsAPI
{
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
                        options.ApiName = "InstitutionsAPI";
                    });

            // adding policies
            services.AddAuthorization(options =>
            {
                //options.AddPolicy("PharmacistProfile", policy =>
                //{
                //    policy.RequireClaim("current_profile", "pharmacist");
                //});

                //options.AddPolicy("PharmacyAdminProfile", policy =>
                //{
                //    policy.RequireClaim("current_profile", "pharmacy_admin");
                //});

                //options.AddPolicy("CanUpdateInstitution", policy =>
                //{
                //    policy.RequireClaim("current_profile",
                //        new[]
                //        {
                //            "pharmacy_admin","hospital_admin", "ministry_worker", "admin"
                //        });
                //});

                //options.AddPolicy("InstitutionAdminProfile", policy =>
                //{
                //    policy.RequireClaim("current_profile",
                //        new[]
                //        {
                //            "pharmacy_admin","hospital_admin"
                //        });
                //});

                //options.AddPolicy("HighLevel", policy =>
                //{
                //    policy.RequireClaim("current_profile",
                //        new[]
                //        {
                //            "MinistryWorker","Admin"
                //        });
                //});

                options.AddPolicy("has_profile", policy =>
                {
                    policy.RequireClaim("current_profile",
                        new[]
                        {
                            "doctor"
                            //,"pharmacist","ministry_worker","patient",
                            //"admin","hospital_admin"
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
