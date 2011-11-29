using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using StructureMap;
using StructureMap.Configuration.DSL;
using AutoMapper;
using Shipping.Business.Entities;
using OnBoarding = Shipping.Mvc.Models.OnBoarding;
using Shipping.Logging;
using Shipping.Business.Services;
using Shipping.Caching;
using Shipping.Configuration;
using Shipping.Web.Security;
using Shipping.Data;
using Shipping.Data.Sql;
using Shipping.Mvc.Models.Supplier;
using Shipping.Business.Services;
using Shipping.Mvc.Models.Customer;

namespace Shipping.Mvc
{
    /// <summary>
    /// Utility class to handle application initialization code.
    /// </summary>
    public static class Bootstrapper
    {
        public static void Bootstrap()
        {
            InitializeAutoMapper();
            InitializeLog4Net();
        }

        public static IContainer GetStructureMapContainer()
        {
            return new Container(new StructureMapRegistry());
        }

        public static void InitializeLog4Net()
        {
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void InitializeAutoMapper()
        {
            Mapper.CreateMap<User, OnBoarding.IndexModel>()
                .ForMember(dest => dest.Password, opt => opt.Ignore())
                .ForMember(dest =>dest.ConfirmPassword, opt => opt.Ignore());

            Mapper.CreateMap<Category, SelectListItem>()
               .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Text, opt => opt.MapFrom(src => src.CategoryName))
               .ForMember(dest => dest.Selected, opt => opt.Ignore());

            Mapper.CreateMap<Supplier, SupplierModel>();
            Mapper.CreateMap<Customer, CustomerModel>();

        }
        public class StructureMapRegistry : Registry
        {
            public StructureMapRegistry()
            {
                var mainConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ShippingMain"].ConnectionString;
                var satellitesString = System.Configuration.ConfigurationManager.AppSettings["Shipping.Data.Sql.SatelliteConnectionStrings"];
                var satellites = satellitesString.Split(',');
                Dictionary<string, string> satelliteConnectionStrings = new Dictionary<string, string>(satellites.Length);

                for (var i = 0; i < satellites.Length; i++)
                {
                    satelliteConnectionStrings.Add(satellites[i], System.Configuration.ConfigurationManager.ConnectionStrings[satellites[i]].ConnectionString);
                }

                For<ILogger>().Singleton().Use<Web.Logging.Log4NetLogger>();
                
                For<IUserService>().Singleton().Use<UserService>();
                For<ISupplierService>().Singleton().Use<SupplierService>();
                For<ICategoryService>().Singleton().Use<CategoryService>();
                For<ICustomerService>().Singleton().Use<CustomerService>();


                For<ICacheProvider>().Use<CacheProvider>()
                .Ctor<bool>("enabled").Is(bool.Parse(System.Configuration.ConfigurationManager.AppSettings["shipping.Caching.CacheProvider.Enabled"]));

                For<IFormsAuthenticationService>().Singleton().Use<FormsAuthenticationService>();

                For<IConfigurationManager>().Singleton().Use<ConfigurationManager>();

                For<IUserRepository>().Singleton().Use<SqlUserRepository>()
                    .Ctor<string>("mainConnectionString").Is(mainConnectionString)
                    .Ctor<Dictionary<string, string>>("satelliteConnectionStrings").Is(satelliteConnectionStrings);

                For<ISupplierRepository>().Singleton().Use<SqlSupplierRepository>()
                    .Ctor<string>("mainConnectionString").Is(mainConnectionString)
                    .Ctor<Dictionary<string, string>>("satelliteConnectionStrings").Is(satelliteConnectionStrings);

                For<ICategoryRepository>().Singleton().Use<SqlCategoryRepository>()
                        .Ctor<string>("mainConnectionString").Is(mainConnectionString)
                        .Ctor<Dictionary<string, string>>("satelliteConnectionStrings").Is(satelliteConnectionStrings);

                For<ICustomerRepository>().Singleton().Use<SqlCustomerRepository>()
                        .Ctor<string>("mainConnectionString").Is(mainConnectionString);
                        //.Ctor<Dictionary<string, string>>("satelliteConnectionStrings").Is(satelliteConnectionStrings);
            }
        }
    }
}