
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upstox_Service.Services;

namespace Upstox_Test.Providers
{
    public class WinRunner
    {
        private readonly ILogger _logger;
        private readonly IEnumerable<IMonitor> _monitor;
        private readonly IList<Task> _tasks = new List<Task>();
        public WinRunner(IEnumerable<IMonitor> monitor, ILogger logger, IList<Task> tasks)
        {
            _monitor = monitor;
            _logger = logger;
            _tasks = tasks;
        }

        public void Start()
        {
            try
            {
                _logger.Information("*** Started monitors ***");
                foreach (var monitor in _monitor)
                {
                    StartMonitor(monitor);
                }
            }
            catch (Exception exception)
            {
                _logger.Error(exception, "Error starting monitors");
            }
        }
        private void StartMonitor(IMonitor monitor)
        {
            var task = new Task(monitor.StartMonitoring);
            task.Start();
            _tasks.Add(task);
        }
        public void Stop()
        {
            _logger.Information("*** Stopping monitors ***");
            foreach (var monitor in _monitor)
            {
                monitor.StopMonitoring();
            }

            _logger.Information("*** Waiting for monitors to safely shut down ***");
            Task.WaitAll(_tasks.ToArray());

            _logger.Information("*** Stopped Winservice ***");
        }
    }
}
