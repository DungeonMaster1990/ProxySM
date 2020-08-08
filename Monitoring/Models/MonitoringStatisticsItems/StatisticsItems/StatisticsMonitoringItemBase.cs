using Monitoring.ConcurrentCounters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Monitoring.Models
{
    public abstract class StatisticsMonitoringItemBase : IMonitoringItem
    {
        public string Name { get; set; }

        [JsonIgnore]
        internal readonly IDictionary<string, IReinitableThreadSafeOperation> Properties;

        public StatisticsMonitoringItemBase(string name = null)
        {
            Properties = GetType().GetProperties()
                .Where(p => typeof(IReinitableThreadSafeOperation).IsAssignableFrom(p.PropertyType))
                .ToDictionary(x => x.Name, x => (IReinitableThreadSafeOperation)x.GetValue(this));
            
            if (name != null)
                Name = name;
            else
                Name = this.GetType().Name;
        }

        internal void ReInit()
        {
            foreach (var monitoringItemProperty in Properties.Values)
                monitoringItemProperty.ReInit();
        }

        public JObject GetJObject()
        {
            return JObject.FromObject(this);
        }
    }
}
