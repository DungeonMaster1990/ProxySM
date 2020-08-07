using Monitoring.ConcurrentCounters;
using System.Diagnostics;

namespace Monitoring.Models
{
    class MonitoringItemEntryCounter : StatisticsMonitoringItemBase
    {
        public ReinitableThreadSafeCounter Entries { get; set; } = new ReinitableThreadSafeCounter();
        public ReinitableThreadSafeCounter Exits { get; set; } = new ReinitableThreadSafeCounter();
        public ReinitableThreadSafeCounter Errors { get; set; } = new ReinitableThreadSafeCounter();
        public Stopwatch Watcher { get; set; } = new Stopwatch();
    }
}
