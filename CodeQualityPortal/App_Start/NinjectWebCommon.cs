using System.Web.Http.ExceptionHandling;
using CodeQualityPortal.Controllers;
using log4net;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CodeQualityPortal.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CodeQualityPortal.NinjectWebCommon), "Stop")]

namespace CodeQualityPortal
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using CodeQualityPortal.Data;
    using System.Web.Mvc;
    using CodeQualityPortal.Common;

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
            kernel.Bind<ILog>().ToMethod(ctx => LogManager.GetLogger(typeof(HomeController)));
            kernel.Bind<IExceptionLogger>().To<GlobalExceptionLogger>().InRequestScope();
            kernel.Bind<ICodeChurnRepository>().To<CodeChurnRepository>();
            kernel.Bind<IMetricsRepository>().To<MetricsRepository>();
        }        
    }
}
