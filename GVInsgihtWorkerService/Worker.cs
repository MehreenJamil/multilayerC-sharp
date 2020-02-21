using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using core.Models;
using core.Services;
using GVInsgihtWorkerService.CustomersImport;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GVInsgihtWorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private ICustomerService _customerService;
        private ICustomerImportService _customerImportService;
        private ICustomerInspectionService _customerInspectionService;
        private IWorkerServiceLogService _workerServiceLogService;
        private IActiveUserService _activeUserService;
        private IModuleService _moduleService;
        private ICustomerModuleService _customerModuleService;
        private ICustomerPropertyService _customerPropertyService;
        // private WorkerServiceLog _workerLogInstance = new WorkerServiceLog(); 

        public Worker(ILogger<Worker> logger, ICustomerService customerService, ICustomerImportService customerImportService, IWorkerServiceLogService workerServiceLogService,ICustomerInspectionService customerInspectionService, IActiveUserService activeUserService, IModuleService moduleService, ICustomerModuleService customerModuleService, ICustomerPropertyService customerPropertyService)
        {
            _logger = logger;
            _customerService = customerService;
            _customerImportService = customerImportService;
            _workerServiceLogService = workerServiceLogService;
            _customerInspectionService = customerInspectionService;
            _activeUserService = activeUserService;
            _moduleService = moduleService;
            _customerModuleService = customerModuleService;
            _customerPropertyService = customerPropertyService;


        }
        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }
        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("---StopAsync---");
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // CreateLOG
                //Start exectuionTIme
                var myCustomerImport = new MyCustomerImport();
                var _workerLogInstance = new WorkerServiceLog();
                    _workerLogInstance.Start_time = DateTime.Now;
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {

                    var customers = await _customerService.GetAllCustomer();
                    foreach (var customer in customers)
                    {
                        var ourCustomerSite = customer.HostUrl;
                       
                        if (!string.IsNullOrEmpty(ourCustomerSite))
                        {

                            // ======================Customer Property start======================================
                            var customerPropertiesDbResult = await _customerPropertyService.GetCustomerPropertyByCustomerId(customer.Id) ;
                          

                                var customerPropertiesApiResult = myCustomerImport.getCustomerProperties(ourCustomerSite, customer.ApiUserName, customer.ApiPassword);

                                 // customerPropertiesApiResult.RemoveAt(0);
                                //var customerPropertiesApiResult = new List<DummyUserData>();
                                //customerPropertiesApiResult.Add(new DummyUserData(1,"St Hilaire","Post Avenue 123","15.5","15.3",100,2000));
                                //customerPropertiesApiResult.Add(new DummyUserData(2,"Big Red McDonalds", "Eagle Lane 169", "15.5", "15.3", 100, 2000));
                                //customerPropertiesApiResult.Add(new DummyUserData(3,"Ol' McBucks House", "Vesta Drive 138", "15.5", "15.3", 100, 2000));
                                //customerPropertiesApiResult.Add(new DummyUserData(4,"Court House", "Maple Court 45", "15.5", "15.3", 100, 2000));

                                foreach (var property in customerPropertiesApiResult)
                                {
                                    var dbModule = customerPropertiesDbResult.FirstOrDefault(x => x.ExternalId == property.Id);
                                    if (dbModule == null)
                                    {

                                        await _customerPropertyService.CreateCustomerProperty(new CustomerProperty
                                        {
                                            Name = property.Name,
                                            CustomerId = customer.Id,
                                            Address = property.Address,
                                            Latitude = property.Latitude,
                                            Longitude = property.Longitude,
                                            Size = property.Size,
                                            Year = property.Year,
                                            ExternalId = property.Id,
                                            Enabled = true



                                        });
                                    }
                                    else
                                    {
                                        await _customerPropertyService.UpdateCustomerProperty(dbModule, new CustomerProperty
                                        {
                                            Name = property.Name,
                                            CustomerId = customer.Id,
                                            Address = property.Address,
                                            Latitude = property.Latitude,
                                            Longitude = property.Longitude,
                                            Size = property.Size,
                                            Year = property.Year,
                                            ExternalId = property.Id,
                                            Enabled = true



                                        });

                                    }
                                }
                                // Check Is Property is Enabled or not (Sold) Check either this property exist in current property Api's list or not
                                // The property exists in our database but does not exist in the import , We set it  as enabled=false in our local db

                                foreach (var dbProperty in customerPropertiesDbResult)
                                {
                                    var dbModule = customerPropertiesApiResult.FirstOrDefault(x => x.Id == dbProperty.ExternalId);
                                    if (dbModule == null)
                                    {
                                        await _customerPropertyService.UpdateCustomerProperty(dbProperty, new CustomerProperty
                                        {
                                            Name = dbProperty.Name,
                                            CustomerId = customer.Id,
                                            Address = dbProperty.Address,
                                            Latitude = dbProperty.Latitude,
                                            Longitude = dbProperty.Longitude,
                                            Size = dbProperty.Size,
                                            Year = dbProperty.Year,
                                            ExternalId = dbProperty.Id,
                                            Enabled = false
                                        });
                                    }
                                }


                            // ======================Customer Property end=======================================
                            // ======================Module start=======================================
                            if (customer.Id == 23) 
                            {
                                var customerModulesDbResult = await _customerModuleService.GetCustomerModuleByCustomerId(customer.Id);
                                // var customerModulesDbResult = _customerModuleService.GetCustomerModuleByCustomerId(customer.Id);
                                var modulesDbResult = await _moduleService.GetAllModule();
                                var customerModulesApiResult = myCustomerImport.GetCustomerModule(ourCustomerSite, customer.ApiUserName, customer.ApiPassword);

                                // This Section is used Only For Dummy data Entries _---------------------------------------
                                //var customerModulesApiResult = new List<DummyUserData>();
                                //   customerModulesApiResult.Add(new DummyUserData("Mapp", "Kartaa", true));
                                //   customerModulesApiResult.Add(new DummyUserData("Component", "KKomponentlista", false));
                                //   customerModulesApiResult.Add(new DummyUserData("PProperty", "FFastighetslista", true));
                                //   customerModulesApiResult.Add(new DummyUserData("File", "DDokument", false));
                                // This Section is used Only For Dummy data Entries _---------------------------------------


                                foreach (var module in customerModulesApiResult)
                                {
                                    var dbModule = modulesDbResult.FirstOrDefault(x => x.Name == module.Name);
                                    if (dbModule == null)
                                    {
                                        var newModule = await _moduleService.CreateModule(new Module { Name = module.Name, Enabled = module.Enabled });
                                        await _customerModuleService.CreateCustomerModule(new CustomerModule
                                        {
                                            Title = module.Title,
                                            ModuleId = newModule.Id,
                                            CustomerId = customer.Id
                                        });

                                    }
                                    else
                                    {

                                        var customerModule = customerModulesDbResult.FirstOrDefault(x => x.ModuleId == dbModule.Id);
                                        if (customerModule == null)
                                        {
                                            await _customerModuleService.CreateCustomerModule(new CustomerModule
                                            {
                                                Title = module.Title,
                                                ModuleId = dbModule.Id,
                                                CustomerId = customer.Id
                                            });

                                        }
                                        else
                                        {
                                            await _customerModuleService.UpdateCustomerModule(customerModule, new CustomerModule
                                            {
                                                Title = module.Title,
                                                ModuleId = dbModule.Id,
                                                CustomerId = customer.Id
                                            });
                                        }


                                    }


                                }

                            }


                            // ======================Module End=========================================
                            // ======================Active User start=======================================

                            var todaysUserHistories = myCustomerImport.GetUserHistoriesForToday(ourCustomerSite, customer.ApiUserName, customer.ApiPassword);
                           

                            var allActiveUsers = await _activeUserService.GetAllActiveUser();

                            var activeUsersForCustomerToday = allActiveUsers.FirstOrDefault(x => 
                            x.UserActiveDate.Date == DateTime.Now.Date &&
                             x.CustomerId == customer.Id
                            );


                            var uniqueUsersTodayCount = todaysUserHistories != null && todaysUserHistories.Any() ? todaysUserHistories.GroupBy(x => x.UserId).Count() : 0;

                            if(activeUsersForCustomerToday == null)
                            {
                                await _activeUserService.CreateActiveUser(new ActiveUser { 
                                    Count = uniqueUsersTodayCount,
                                    UserActiveDate = DateTime.Now.Date,
                                    CustomerId = customer.Id
                                });
                            }
                            else
                            {
                                await _activeUserService.UpdateActiveUser(activeUsersForCustomerToday, 
                                    new ActiveUser { 
                                        UserActiveDate = DateTime.Now.Date,
                                        Count = uniqueUsersTodayCount,
                                        CustomerId = customer.Id
                                    });
                            }

                           


                            // ======================Active User end ========================================


                            // ======================customer inspection start=======================================
                            var customerApiAnspectionResult = myCustomerImport.CallToCustomerInspectionUrl(ourCustomerSite, customer.ApiUserName, customer.ApiPassword);
                            var customerDbInspectionResult = await _customerInspectionService.GetCustomerInspectionByCustomerId(customer.Id);

                            var ourCurrentInspection = customerDbInspectionResult.FirstOrDefault(ci => DateTime.Compare(ci.CompletedDatetime.Date, DateTime.Now.Date) == 0);

                            var newInspection = new CustomerInspection();
                            newInspection.CustomerId = customer.Id;
                            newInspection.Count = customerApiAnspectionResult!= null ?customerApiAnspectionResult.Count():0;
                            newInspection.CompletedDatetime = DateTime.Now.Date;
                            if (ourCurrentInspection == null)
                            {
                                await _customerInspectionService.CreateCustomerInspection(newInspection);
                            }
                            else
                            {
                                await _customerInspectionService.UpdateCustomerInspection(ourCurrentInspection, newInspection);
                            }
                            

                             


                           
                            // ======================customer inspection end ========================================




                            // ======================Customer Import Start===========================================
                            var customerImportResult = myCustomerImport.CallToCustomerImportApiUrl(ourCustomerSite, customer.ApiUserName, customer.ApiPassword);

                            //Get CurrentImportStatusFrom GVINsights
                            var currentImports = await _customerImportService.GetCustomerImportByCustomerId(customer.Id);

                            if (customerImportResult != null)
                            {
                                foreach (var apiImport in customerImportResult)
                                {

                                    var ourCurrentImoport = currentImports.FirstOrDefault(c => c.ExternalImportId == apiImport.Id);
                                    var newCustomerImportObj = new CustomerImport();
                                    newCustomerImportObj.CustomerId = customer.Id;
                                    newCustomerImportObj.LastImport = apiImport.LastImport;
                                    newCustomerImportObj.NextImport = myCustomerImport.CalcNextImportDate(apiImport.LastImport, apiImport.WhenType);
                                    newCustomerImportObj.Name = "";
                                    newCustomerImportObj.Removed = false;
                                    newCustomerImportObj.ExternalImportId = apiImport.Id;

                                    // if(Enabled == false) NextImport 
                                    if (apiImport.Enabled == false)
                                        newCustomerImportObj.NextImport = null;

                                    if (ourCurrentImoport == null)
                                    {
                                        // Create a new Import for User
                                        await _customerImportService.CreateCustomerImport(newCustomerImportObj);
                                    }
                                    else
                                    {
                                        // Update Customer Import data
                                        await _customerImportService.UpdateCustomerImport(ourCurrentImoport, newCustomerImportObj);
                                    }

                                }
                            }
                            // ======================Customer Import End===========================================



                        }
                        else
                        {
                            _logger.LogInformation("String is empty");
                        }

                    }
                }
                catch (SqlException ex) {
                    _workerLogInstance.Exception = ex.ToString();
                    _logger.LogInformation("SQL Exception", ex.ToString());
                }
                catch (Exception ex)
                {
                    _workerLogInstance.Exception = ex.ToString();

                    _logger.LogInformation("Exception:: ", ex.ToString());

                }
                finally {

                        _workerLogInstance.End_time = DateTime.Now;
                        await _workerServiceLogService.CreateWorkerServiceLog(_workerLogInstance);
                        await Task.Delay(60 * 60000, stoppingToken);
                       // await Task.Delay(15000, stoppingToken);

                }
               
            }
        }
    }
}
