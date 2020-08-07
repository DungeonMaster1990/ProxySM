using Monitoring.ConcurrentCounters;
using System.Diagnostics;

namespace Monitoring.Models
{
    class MonitoringItemEntryCounter : StatisticsMonitoringItemBase
    {
        public ThreadSafeCounter Entries { get; set; } = new ThreadSafeCounter();
        public ThreadSafeCounter Exits { get; set; } = new ThreadSafeCounter();
        public ThreadSafeCounter Errors { get; set; } = new ThreadSafeCounter();
        public Stopwatch Watcher { get; set; } = new System.Diagnostics.Stopwatch();
    }
}
