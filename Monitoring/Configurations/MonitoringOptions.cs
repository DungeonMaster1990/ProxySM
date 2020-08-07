using System;
using Newtonsoft.Json;

namespace Monitoring.Configurations
{
    public class MonitoringOptions
    {
        public bool EnableMonitoring { get; set; } = true;
        public TimeSpan SendInterval { get; set; } = TimeSpan.FromSeconds(10);
        public bool RunImmediately { get; set; } = true;
        public Formatting JsonFormatingInSimpleLog { get; set; } = Formatting.None;
    }
}
