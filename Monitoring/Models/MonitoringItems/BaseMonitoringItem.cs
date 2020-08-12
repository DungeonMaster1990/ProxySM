using Monitoring.ConcurrentCounters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Monitoring.Models.MonitoringItems
{
    public abstract class BaseMonitoringItem: IMonitoringItem
    {
        public string Name { get; set; }

        public IDictionary<string, IReinitableThreadSafeOperation> Properties { get; set; }

        public JObject GetJObject()
        {
            return JObject.FromObject(this);
        }
    }
}
