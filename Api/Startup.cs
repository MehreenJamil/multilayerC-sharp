using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using core;
using core.Services;
using GVInsightDataAccess;
using Services;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace Api
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
            services.AddCors();

            services.AddControllers();

#if DEBUG
            var testDbConnstring = Configuration.GetConnectionString("Default");
#else
                var testDbConnstring = Configuration.GetConnectionString("GreenConDevDB");
#endif

        //    var testDbConnstring = Configuration.GetConnectionString("GreenConDevDB");

            services.AddDbContext<MyCustomerDbContext>(options => options.UseSqlServer(testDbConnstring, x => x.MigrationsAssembly("GVInsight.GVInsightDataAccess")));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<ICustomerImportService, CustomerImportService>();
            services.AddTransient<IWorkerServiceLogService, WorkerServiceLogService>();
            services.AddTransient<ICustomerInspectionService, CustomerInspectionService>();
            services.AddTransient<IActiveUserService, ActiveUserService>();

            services.AddTransient<IModuleService, ModuleService>();
            services.AddTransient<ICustomerModuleService, CustomerModuleService>();
            services.AddTransient<ICustomerPropertyService, CustomerPropertiesService>();


            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "My Customer", Version = "v1" });
            });

            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            app.UseDeveloperExceptionPage();
            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My Customer V1");
            });
        }
    }
}
