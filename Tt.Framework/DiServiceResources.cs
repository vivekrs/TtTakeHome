using Ninject;
using Tt.Framework.Service;

namespace Tt.Framework
{
    /// <summary>
    /// Ideally, these will come from some global configuration system such as a resource file or database
    /// I did not put it in web.config in the webapi project, or the app.config in the service because of maintenance concerns
    /// </summary>
    public static class DiServiceResources
    {
        private const string BasePath = @"C:\CollectorData";

        private const string ConnectionString =
            @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Docs\Documents\Visual Studio 2013\Projects\TtTakeHome\Tt.Framework\Data\TtTakeHome.mdf;Integrated Security=True;";

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
        {
            //log4net.Config.XmlConfigurator.Configure();
            //var settings = new NinjectSettings { LoadExtensions = false };
            var kernel = new StandardKernel();
            try
            {
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
        public static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IFileReader>().To<CsvFileReader>();//.Intercept().With<LoggerInterceptor>();
            kernel.Bind<IPersistence>().To<SqlPersistence>().WithConstructorArgument("connectionString", ConnectionString);//.Intercept().With<LoggerInterceptor>();
            kernel.Bind<ICollector>().To<TtCollector>().WithConstructorArgument("collectorBasePath", BasePath);//.Intercept().With<LoggerInterceptor>();
        }
    }

    //public class LoggerInterceptor : SimpleInterceptor
    //{
    //    readonly Stopwatch _stopwatch = new Stopwatch();

    //    protected override void BeforeInvoke(IInvocation invocation)
    //    {
    //        _stopwatch.Start();
    //    }

    //    protected override void AfterInvoke(IInvocation invocation)
    //    {
    //        _stopwatch.Stop();
    //        var message = string.Format("Execution of {0} took {1}.", invocation.Request.Method, _stopwatch.Elapsed);
    //        Console.WriteLine(message); //Replace this with a logger of some sort...
    //        _stopwatch.Reset();
    //    }
    //}
}
