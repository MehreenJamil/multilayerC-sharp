using System;
using AutoMapper;
using Api.Resources;
using core.Models;

namespace Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Resource
            CreateMap<Customer, CustomerResource>().ReverseMap();
            CreateMap<CustomerImport, CustomerImportResource>();
            CreateMap<WorkerServiceLog, WorkerServiceLogResource>();
            CreateMap<CustomerInspection, CustomerInspectionResource>();
            CreateMap<ActiveUser, ActiveUserResource>();
            CreateMap<Module, ModuleResource>();
            CreateMap<CustomerModule, CustomerModuleResource>();
            CreateMap<CustomerProperty, CustomerPropertyResource>();

            // Resource to Domain
            CreateMap<CustomerResource, Customer>();
            CreateMap<CustomerImportResource, CustomerImport>();
            CreateMap<WorkerServiceLogResource, WorkerServiceLog>();
            CreateMap<CustomerInspectionResource, CustomerInspection>();
            CreateMap<ActiveUserResource, ActiveUser>();
            CreateMap<ModuleResource, Module>();
            CreateMap<CustomerModuleResource, CustomerModule>();
            CreateMap<CustomerPropertyResource, CustomerProperty>();



        }
    }
}
