using Monitoring.ConcurrentCounters;
using Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProxyAPI.Monitoring
{
    public class ProxyAPIMonitoringItem : IMonitoringItem
    {
        public IThreadSafeOperation CountOfRequests { get; set; } = new ThreadSafeCounter();

        public void Clear()
        {
            throw new NotImplementedException();
        }
    }
}
