using Monitoring.ConcurrentCounters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Monitoring.Models.MonitoringItems
{
    public abstract class BaseMonitoringItem : IMonitoringItem
    {
        public string Name { get; set; }

        public IDictionary<string, IReinitableThreadSafeOperation> Properties { get; private set; }
        public string GroupName { get; set; }

        public JObject GetJObject()
        {
            return JObject.FromObject(this);
        }

        public void SetProperties()
        {
            Properties = GetType().GetProperties()
                .Where(p => typeof(IReinitableThreadSafeOperation).IsAssignableFrom(p.PropertyType))
                .ToDictionary(x => x.Name, x => (IReinitableThreadSafeOperation)x.GetValue(this));
        }
    }
}
