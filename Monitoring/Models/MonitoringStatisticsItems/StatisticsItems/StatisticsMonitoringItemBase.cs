using Monitoring.ConcurrentCounters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Newtonsoft.Json;

namespace Monitoring.Models
{
    public abstract class StatisticsMonitoringItemBase : IMonitoringItem
    {
        public string Name { get; }

        [JsonIgnore]
        internal readonly IDictionary<string, IThreadSafeOperation> Properties;

        public StatisticsMonitoringItemBase(string name = null)
        {
            Properties = GetType().GetProperties()
                .Where(p => p.GetType().IsAssignableFrom(typeof(IThreadSafeOperation)))
                .ToDictionary(x => x.Name, x => x.GetValue(x) as IThreadSafeOperation);

            if (name != null)
                Name = name;
            else
            {
                Name = this.GetType().Name;
            }
        }

        internal void ReInit()
        {
            foreach (var monitoringItemProperty in Properties.Values)
                monitoringItemProperty.Reinit();
        }

        public JObject GetJObject()
        {
            return JObject.FromObject(this);
        }
    }
}
