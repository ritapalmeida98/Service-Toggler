using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Toggler_Service.Repositories;
using Toggler_Service.Services;

namespace Toggler_Service
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; set; }

        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors();

            services.AddControllers();

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ServiceToggler", Version = "v1" });
            });

            int chosenDB = Configuration.GetValue("ChosenDB", 0);
            switch ((DataProviderEnum)chosenDB)
            {
                case DataProviderEnum.SQLServer:
                    services.AddDbContext<ServiceTogglerContext>(options =>
                           options.UseSqlServer(Configuration.GetConnectionString("SqlServer")));
                    break;
                case DataProviderEnum.InMemory:
                default:
                    services.AddDbContext<ServiceTogglerContext>(options =>
                           options.UseInMemoryDatabase(Configuration.GetConnectionString("InMemory")));
                    break;
            }


            services.AddTransient<IServiceService, ServiceService>();
            services.AddTransient<IToggleService, ToggleService>();
            services.AddTransient<IToggleServiceService, ToggleServiceService>();

            services.AddTransient<IServiceRepository, ServiceRepository>();
            services.AddTransient<IToggleRepository, ToggleRepository>();
            services.AddTransient<IToggleServiceRepository, ToggleServiceRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}");
            });

            app.UseCors(builder => builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod());
        }
    }
    public enum DataProviderEnum
    {
        InMemory = 0,
        SQLServer = 1

    }
}
