using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ninject;
using Tt.Framework;
using Tt.Framework.Service;

namespace Tt.Service
{
    public partial class CollectorService : ServiceBase
    {
        private readonly IKernel _kernel;
        private readonly Timer _timer;

        /// <summary>
        /// Init
        /// </summary>
        public CollectorService()
        {
            InitializeComponent();

            _kernel = DiServiceResources.CreateKernel();
            _timer = new Timer(TryProcessCollectedFiles, null, Timeout.Infinite, Timeout.Infinite);
        }

        /// <summary>
        /// Start polling now...
        /// </summary>
        /// <param name="args"></param>
        protected override void OnStart(string[] args)
        {
            _timer.Change(0, Timeout.Infinite);
        }

        /// <summary>
        /// Process any unprocessed files, if present
        /// </summary>
        /// <param name="state"></param>
        private void TryProcessCollectedFiles(object state)
        {
            var collector = _kernel.Get<ICollector>();

            Guid nextFile;
            //First, check if there is an unprocessed file
            while ((nextFile = collector.GetNextUnprocessedFile()) != Guid.Empty)
            {
                //Process the next file
                Trace.WriteLine("Processing file " + nextFile);
                collector.ProcessFile(nextFile);
            }

            //Wait for 10 seconds
            _timer.Change(10000, Timeout.Infinite);
        }

        /// <summary>
        /// Bye
        /// </summary>
        protected override void OnStop()
        {
        }

        internal void StartService(string[] args)
        {
            OnStart(args);
        }

        internal void StopService()
        {
            OnStop();
        }
    }
}
