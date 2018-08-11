using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeApi.Context;
using RecipeApi.Repositories;
using RecipeApi.Services;
using System.IO;
using System.Net;
using UserManagementConsumer.Client;

namespace RecipeApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private IConfiguration Credentials = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("MailCredentials.json").Build();

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.Configure<Settings.Settings>(
                options =>
                {
                    options.ConnectionString = Configuration.GetSection("MongoConnection:ConnectionString").Value;
                    options.Database = Configuration.GetSection("MongoConnection:Database").Value;
                });

            services.AddTransient<IRecipeContext, RecipeContext>();
            services.AddTransient<IRecipeRepository, RecipeRepository>();

            services.AddTransient<IRecipeHistoryContext, RecipeHistoryContext>();
            services.AddTransient<IRecipeHistoryRepository, RecipeHistoryRepository>();

            services.AddSingleton(new QrCodeService());
            services.AddSingleton(new QrMailSender(
                new NetworkCredential(Credentials["Username"], Credentials["Password"])));

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = this.Configuration["Endpoints:AuthApi"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "RecipeAPI";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DoctorProfile", policy =>
                {
                    policy.RequireClaim("current_profile", "doctor");
                });

                options.AddPolicy("PharmacistProfile", policy =>
                {
                    policy.RequireClaim("current_profile", "pharmacist");
                });

                options.AddPolicy("CanWorkWithRecipe", policy =>
                {
                    policy.RequireClaim("current_profile", new[]
                    {
                        "doctor", "pharmacist", "patient"
					});
                });

                options.AddPolicy("CanChangeRecipe", policy =>
                {
                    policy.RequireClaim("current_profile", "doctor" );
                });

                options.AddPolicy("CanChangeRecipeHistory", policy => 
                {
                    policy.RequireClaim("current_profile", "pharmacist" );
                });

				options.AddPolicy("CanWorkWithRecipeHistory", policy =>
				{
					policy.RequireClaim("current_profile", new[]
					{
						"pharmacist", "doctor", "patient"
					});
				});
			});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
