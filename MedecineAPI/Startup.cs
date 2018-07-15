using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MedicineAPI.Context;
using MedicineAPI.Repositories;

namespace MedecineAPI
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

			services.AddAuthentication("Bearer")
				.AddIdentityServerAuthentication(options =>
				{
					options.Authority = this.Configuration["Endpoints:AuthApi"];
					options.RequireHttpsMetadata = false;
					options.ApiName = "MedicieApi";
				});


			services.AddTransient<IMedicineContext, MedicineContext>();
            services.AddTransient<IMedicineRepository, MedicineRepository>();
               
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
