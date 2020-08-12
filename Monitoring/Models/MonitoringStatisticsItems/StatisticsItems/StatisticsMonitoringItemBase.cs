using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Monitoring.ConcurrentCounters;

namespace Monitoring.Models
{
    public abstract class StatisticsMonitoringItemBase : IStatisticsMonitoringItem
    {
        public string Name { get; set; }

        [JsonIgnore]
        public IDictionary<string, IReinitableThreadSafeOperation> Properties { get; set; }

        //public StatisticsMonitoringItemBase(string name = null)
        //{
        //    Properties = GetType().GetProperties()
        //        .Where(p => typeof(IReinitableThreadSafeOperation).IsAssignableFrom(p.PropertyType))
        //        .ToDictionary(x => x.Name, x => (IReinitableThreadSafeOperation)x.GetValue(this));
            
        //    if (name != null)
        //        Name = name;
        //    else
        //        Name = this.GetType().Name;
        //}

        public void ReInit()
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
