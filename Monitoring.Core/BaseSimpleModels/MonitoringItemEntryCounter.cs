using Monitoring.ConcurrentCounters;

namespace Monitoring.Models
{
    class MonitoringItemEntryCounter : StatisticsMonitoringItemBase
    {
        public ReinitableThreadSafeCounter Entries { get; set; } = new ReinitableThreadSafeCounter();
        public ReinitableThreadSafeCounter Exits { get; set; } = new ReinitableThreadSafeCounter();
        public ReinitableThreadSafeCounter Errors { get; set; } = new ReinitableThreadSafeCounter();
        public ReinitableThreadSafeAverageTime AverageExecutionTime { get; set; } = new ReinitableThreadSafeAverageTime();
    }
}
