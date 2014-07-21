using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject;
using Tt.Framework.Service;

namespace Tt.Framework
{
    /// <summary>
    /// Ideally, these will come from some global configuration system such as a resource file or database
    /// </summary>
    public static class DiServiceResources
    {
        private const string BasePath = @"C:\CollectorData";

        private const string ConnectionString =
            @"Data Source=(LocalDB)\v11.0;AttachDbFilename=D:\Docs\Documents\Visual Studio 2013\Projects\Tt_TakeHome_Vivek\Tt.Framework\Data\TtTakeHome.mdf;Integrated Security=True;";
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        public static IKernel CreateKernel()
        {
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
            kernel.Bind<IFileReader>().To<CsvFileReader>();
            kernel.Bind<ICollector>().To<TtCollector>().WithConstructorArgument("collectorBasePath", BasePath);
            kernel.Bind<IPersistence>()
                .To<SqlPersistence>()
                .WithConstructorArgument("connectionString", ConnectionString);
        }
    }
}
