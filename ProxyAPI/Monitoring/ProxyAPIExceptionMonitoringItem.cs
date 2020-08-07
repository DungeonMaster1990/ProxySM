using Monitoring.ConcurrentCounters;
using Monitoring.Models;

namespace ProxyAPI.Monitoring
{
    public class ProxyAPIExceptionMonitoringItem: StatisticsMonitoringItemBase
    {
        public ReinitableThreadSafeCounter CountOfExceptions { get; set; } = new ReinitableThreadSafeCounter();
    }
}
