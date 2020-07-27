using System;
using System.Collections.Generic;
using System.Text;

namespace Upstox_Service.Services
{
    public interface IMonitor
    {
        void StartMonitoring();
        void StopMonitoring();
    }
}
