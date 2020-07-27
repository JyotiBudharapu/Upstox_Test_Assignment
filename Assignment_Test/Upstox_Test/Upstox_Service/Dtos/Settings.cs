using System;
using System.Collections.Generic;
using System.Text;

namespace Upstox_Service.Dtos
{
    public class Settings
    {
        public string LogFilePath { get; set; }
        public string PollingInterval { get; set; }
        public string IntervalDurationMinutes { get; set; }
        public bool StartRequestRetryMonitor { get; set; }
    
    }
}
