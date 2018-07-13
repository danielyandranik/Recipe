using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecipeApi.Context;
using RecipeApi.Repositories;

namespace RecipeApi
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

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = this.Configuration["Endpoints:AuthApi"];
                    options.RequireHttpsMetadata = false;
                    options.ApiName = "RecipeApi";
                });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("DoctorProfile", policy =>
                {
                    policy.RequireClaim("current_profile", "Doctor");
                });

                options.AddPolicy("PharmacistProfile", policy =>
                {
                    policy.RequireClaim("current_profile", "Pharmacist");
                });

                options.AddPolicy("ChiefDoctorProfile", policy => 
                {
                    policy.RequireClaim("currentProfile", "ChiefDoctor");
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

            app.UseMvc();
        }
    }
}
