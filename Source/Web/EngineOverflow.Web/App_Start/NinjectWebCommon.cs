[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(EngineOverflow.Web.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(EngineOverflow.Web.NinjectWebCommon), "Stop")]

namespace EngineOverflow.Web
{
    using System;
    using System.Data.Entity;
    using System.Web;

    using EngineOverflow.Data;
    using EngineOverflow.Data.Common.Repository;
    using EngineOverflow.Data.Models;
    using EngineOverflow.Web.Infrastructure;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start()
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
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
            kernel.Bind<DbContext>().To<ApplicationDbContext>();

            kernel.Bind(typeof(IRepository<Post>)).To(typeof(IDeletableEntityRepository<Post>));

            kernel.Bind(typeof(IDeletableEntityRepository<>))
                .To(typeof(DeletableEntityRepository<>));

            kernel.Bind(typeof(IRepository<>)).To(typeof(GenericRepository<>));

            kernel.Bind<ISanitizer>().To<HtmlSanitizerAdapter>();
        }
    }
}
