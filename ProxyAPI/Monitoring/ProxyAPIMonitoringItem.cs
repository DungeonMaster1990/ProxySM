using Monitoring.ConcurrentCounters;
using Monitoring.Models;

namespace ProxyAPI.Monitoring
{
    public class ProxyAPIMonitoringItem : StatisticsMonitoringItemBase
    {
        public ReinitableThreadSafeCounter CountOfRequests { get; set; } = new ReinitableThreadSafeCounter();
        public ReinitableThreadSafeCounter CountOfFailedRequests { get; set; } = new ReinitableThreadSafeCounter();
    }
}
