[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(Hello.WebUI.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(Hello.WebUI.App_Start.NinjectWebCommon), "Stop")]

namespace Hello.WebUI.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Hello.Data.Interface;
    using Hello.Data;
    using Hello.Core.Interface.Data;
    using Hello.Core.Model;
    using Hello.Core.Interface.Service;
    using Hello.Service;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IDatabaseFactory>().To<DatabaseFactory>().InRequestScope();

            kernel.Bind<IRepository<Country>>().To<Repository<Country>>().InRequestScope();
            kernel.Bind<IRepository<Province>>().To<Repository<Province>>().InRequestScope();
            kernel.Bind<IRepository<District>>().To<Repository<District>>().InRequestScope();
            kernel.Bind<IRepository<Device>>().To<Repository<Device>>().InRequestScope();
            kernel.Bind<IRepository<Account>>().To<Repository<Account>>().InRequestScope();
            kernel.Bind<IRepository<Property>>().To<Repository<Property>>().InRequestScope();
            kernel.Bind<IRepository<Marker>>().To<Repository<Marker>>().InRequestScope();
            kernel.Bind<IRepository<ProductsViewModel>>().To<Repository<ProductsViewModel>>().InRequestScope();
            kernel.Bind<IRepository<ProductsModel>>().To<Repository<ProductsModel>>().InRequestScope();
            kernel.Bind<IRepository<Features>>().To<Repository<Features>>().InRequestScope();
            kernel.Bind<IRepository<Favourite>>().To<Repository<Favourite>>().InRequestScope();
            kernel.Bind<IRepository<RecentlyViewed>>().To<Repository<RecentlyViewed>>().InRequestScope();
            kernel.Bind<IRepository<Direction>>().To<Repository<Direction>>().InRequestScope();
            kernel.Bind<IRepository<Product>>().To<Repository<Product>>().InRequestScope();
            kernel.Bind<IRepository<Information>>().To<Repository<Information>>().InRequestScope();
            kernel.Bind<IRepository<Advertising>>().To<Repository<Advertising>>().InRequestScope();
            kernel.Bind<IRepository<Warning>>().To<Repository<Warning>>().InRequestScope();

            kernel.Bind<IRepository<Notify>>().To<Repository<Notify>>().InRequestScope();
            kernel.Bind<IRepository<GiftCard>>().To<Repository<GiftCard>>().InRequestScope();

            kernel.Bind<ICountryService>().To<CountryService>().InRequestScope();
            kernel.Bind<IProvinceService>().To<ProvinceService>().InRequestScope();
            kernel.Bind<IDistrictService>().To<DistrictService>().InRequestScope();
            kernel.Bind<IDeviceService>().To<DeviceService>().InRequestScope();
            kernel.Bind<IAccountService>().To<AccountService>().InRequestScope();
            kernel.Bind<IPropertyService>().To<PropertyService>().InRequestScope();
            kernel.Bind<IMarkerService>().To<MarkerService>().InRequestScope();
            kernel.Bind<IFeaturesService>().To<FeaturesService>().InRequestScope();
            kernel.Bind<IDirectionService>().To<DirectionService>().InRequestScope();
            kernel.Bind<IProductService>().To<ProductService>().InRequestScope();
            kernel.Bind<IProductsService>().To<ProductsService>().InRequestScope();
            kernel.Bind<IRecentlyViewedService>().To<RecentlyViewedService>().InRequestScope();
            kernel.Bind<IFavouriteService>().To<FavouriteService>().InRequestScope();
            kernel.Bind<IInfoService>().To<InfoService>().InRequestScope();
            kernel.Bind<IAdvService>().To<AdvService>().InRequestScope();
            kernel.Bind<IWarningService>().To<WarningService>().InRequestScope();

            kernel.Bind<INotifyService>().To<NotifyService>().InRequestScope();
            kernel.Bind<IGiftCardService>().To<GiftCardService>().InRequestScope();
        }
    }
}
