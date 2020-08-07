using Monitoring.ConcurrentCounters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Monitoring.Models
{
    public abstract class StatisticsMonitoringItemBase : IMonitoringItem
    {
        public string Name { get; }

        [JsonIgnore]
        internal readonly IDictionary<string, IReinitableThreadSafeOperation> Properties;

        public StatisticsMonitoringItemBase()
        {
            Properties = GetType().GetProperties()
                .Where(p => p.GetType().IsAssignableFrom(typeof(IReinitableThreadSafeOperation)))
                .ToDictionary(x => x.Name, x => x.GetValue(x) as IReinitableThreadSafeOperation); 
            
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
