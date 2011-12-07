using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using StructureMap;
using StructureMap.Configuration.DSL;
using AutoMapper;
using VisitaJayaPerkasa.Business.Entities;
using VisitaJayaPerkasa.Business.Services;
using VisitaJayaPerkasa.Caching;
using VisitaJayaPerkasa.Configuration;
using VisitaJayaPerkasa.Web.Security;
using VisitaJayaPerkasa.Data;
using VisitaJayaPerkasa.Data.Sql;
using VisitaJayaPerkasa.Mvc.Models;

namespace VisitaJayaPerkasa.Mvc
{
    /// <summary>
    /// Utility class to handle application initialization code.
    /// </summary>
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            InitializeAutoMapper();
        }

        public static IContainer GetStructureMapContainer()
        {
            return new Container(new StructureMapRegistry());
        }

        public static void InitializeAutoMapper()
        {
            Mapper.CreateMap<User, UserModel>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ToString()));
            Mapper.CreateMap<Supplier, SupplierModel>();
            Mapper.CreateMap<Category, SelectListItem>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.CategoryCode))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.CategoryName))
            .ForMember(dest => dest.Selected, opt => opt.Ignore());

            Mapper.CreateMap<Customer, CustomerModel>();
            Mapper.CreateMap<Category, CategoryModel>();
            Mapper.CreateMap<City, CityModel>();
            Mapper.CreateMap<Condition, ConditionModel>();
            Mapper.CreateMap<LeadTime, LeadTimeModel>();
            Mapper.CreateMap<Role, RoleModel>();
            Mapper.CreateMap<TypeCont, TypeContModel>();

            Mapper.CreateMap<Role, SelectListItem>()
            .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Selected, opt => opt.Ignore());

            Mapper.CreateMap<Vessel, VesselModel>();
        }
        public class StructureMapRegistry : Registry
        {
            public StructureMapRegistry()
            {
                For<IUserService>().Singleton().Use<UserService>();
                For<ISupplierService>().Singleton().Use<SupplierService>();
                For<ISupplierService>().Singleton().Use<SupplierService>();
                For<ICategoryService>().Singleton().Use<CategoryService>();
                For<ICustomerService>().Singleton().Use<CustomerService>();
                For<ICityService>().Singleton().Use<CityService>();
                For<IConditionService>().Singleton().Use<ConditionService>();
                For<ILeadTimeService>().Singleton().Use<LeadTimeService>();
                For<IRoleService>().Singleton().Use<RoleService>();
                For<ITypeContService>().Singleton().Use<TypeContService>();

                For<IVesselService>().Singleton().Use<VesselService>();


                For<ICacheProvider>().Use<CacheProvider>()
                .Ctor<bool>("enabled").Is(bool.Parse(System.Configuration.ConfigurationManager.AppSettings["VisitaJayaPerkasa.Caching.CacheProvider.Enabled"]));

                For<IFormsAuthenticationService>().Singleton().Use<FormsAuthenticationService>();

                For<IConfigurationManager>().Singleton().Use<ConfigurationManager>();

                For<IUserRepository>().Singleton().Use<UserRepository>()
                    .Ctor<string>("mainConnectionString").Is("ShippingMain");

                For<ISupplierRepository>().Singleton().Use<SupplierRepository>()
                .Ctor<string>("mainConnectionString").Is("ShippingMain");

                For<ICategoryRepository>().Singleton().Use<CategoryRepository>()
                .Ctor<string>("mainConnectionString").Is("ShippingMain");

                For<ICustomerRepository>().Singleton().Use<CustomerRepository>()
                    .Ctor<string>("mainConnectionString").Is("ShippingMain");

                For<ICityRepository>().Singleton().Use<CityRepository>()
                    .Ctor<string>("mainConnectionString").Is("ShippingMain");

                For<IConditionRepository>().Singleton().Use<ConditionRepository>()
                    .Ctor<string>("mainConnectionString").Is("ShippingMain");

                For<ILeadTimeRepository>().Singleton().Use<LeadTimeRepository>()
                    .Ctor<string>("mainConnectionString").Is("ShippingMain");

                For<IRoleRepository>().Singleton().Use<RoleRepository>()
                    .Ctor<string>("mainConnectionString").Is("ShippingMain");

                For<ITypeContRepository>().Singleton().Use<TypeContRepository>()
                    .Ctor<string>("mainConnectionString").Is("ShippingMain");

                For<IVesselRepository>().Singleton().Use<VesselRepository>()
                    .Ctor<string>("mainConnectionString").Is("ShippingMain");
            }
        }
    }
}