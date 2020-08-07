using Monitoring.ConcurrentCounters;
using Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Monitoring.Models.MonitoringItems;

namespace ProxyAPI.Monitoring
{
    public class ProxyAPIMonitoringItem : StatisticsMonitoringItemBase
    {
        public ThreadSafeCounter CountOfRequests { get; set; } = new ThreadSafeCounter();
        public ThreadSafeCounter CountOfFailedRequests { get; set; } = new ThreadSafeCounter();
    }
}
