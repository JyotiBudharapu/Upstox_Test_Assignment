using System;
using System.Collections.Generic;
using System.Text;
using Topshelf;
using Upstox_Test.Providers;

namespace Upstox_Test.Services
{
    public class ServiceWrapper : ServiceControl
    {
        private readonly WinRunner _runner;

        public ServiceWrapper(WinRunner runner)
        {
            _runner = runner;
        }

        public bool Start(HostControl hostControl)
        {
            _runner.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            _runner.Stop();
            return true;
        }
    }
}
