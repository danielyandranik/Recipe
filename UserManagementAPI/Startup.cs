using DatabaseAccess.Repository;
using DatabaseAccess.SpExecuters;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Net;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI
{
    /// <summary>
    /// Startup class for User Management API
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
                        options.ApiName = "UserManagementAPI";
                    });

            // adding policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsAdmin", policy => policy.RequireClaim("current_profile", "admin"));
            });

            // adding singletons
            services.AddSingleton(new Repo<UserFullInfo>(
                new MapInfo(this.Configuration["Mappers:Users"]),
                new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));

            services.AddSingleton(new Repo<UserPublicInfo>(
                new MapInfo(this.Configuration["Mappers:Users"]),
                new SpExecuter(this.Configuration["ConnectionStrings:UsersDB"])));

            services.AddSingleton(new Verifier());

            services.AddSingleton(new MailService(
                new NetworkCredential(this.Credentials["Username"],
                                      this.Credentials["Password"])));

           
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
