using System;

namespace Monitoring.Configurations
{
    public class MonitoringOptions
    {
        public TimeSpan SendInterval { get; set; } = TimeSpan.FromSeconds(10);
    }
}
