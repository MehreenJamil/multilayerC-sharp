using System;
using core;
using core.Services;
using GVInsightDataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Services;

namespace GVInsgihtWorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //  "C:\GVInsights\GVWorkerService";   For Server
            //  "C:\Users\gclmehjam\workerservice\LogFile.txt";   For Local
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.File(@"C:\GVInsights\GVWorkerService\LogFile.txt")
                .CreateLogger();
            try
            {
                Log.Information("Starting Up the Service");
                CreateHostBuilder(args).Build().Run();
                return;

            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "There was a problem starting the service");
                return;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           
            
            
            Host.CreateDefaultBuilder(args)
            .UseWindowsService()
                .ConfigureServices((hostContext, services) =>
                {
                    // var connectionString = hostContext.Configuration.GetConnectionString("Default");
#if DEBUG
                    var connectionString = hostContext.Configuration.GetConnectionString("Default");
#else
                               var connectionString = hostContext.Configuration.GetConnectionString("GreenConDevDB");
#endif

                    //connectionString = hostContext.Configuration.GetConnectionString("GreenConDevDB");

                    services.AddDbContext<MyCustomerDbContext>(options => options.UseSqlServer(connectionString, x => x.MigrationsAssembly("GVInsight.GVInsightDataAccess")), ServiceLifetime.Singleton);

                    services.AddSingleton<IUnitOfWork, UnitOfWork>();
                    services.AddSingleton<ICustomerService, CustomerService>();
                    services.AddSingleton<ICustomerImportService, CustomerImportService>();
                    services.AddSingleton<ICustomerInspectionService, CustomerInspectionService>();
                    services.AddSingleton<IWorkerServiceLogService, WorkerServiceLogService>();
                    services.AddSingleton<IActiveUserService, ActiveUserService>();
                    services.AddSingleton<IModuleService, ModuleService>();
                    services.AddSingleton<ICustomerModuleService, CustomerModuleService>();
                    services.AddSingleton<ICustomerPropertyService, CustomerPropertiesService>();

                    services.AddHostedService<Worker>()
                    ;
                })
            .UseSerilog();
    }
}
