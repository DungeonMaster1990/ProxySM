using System.Linq;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Monitoring.ConcurrentCounters;

namespace Monitoring.Models
{
    /// <summary>
    /// Базовый статистический мониториговый item. Используется для создания простых объектов
    /// </summary>
    public abstract class StatisticsMonitoringItemBase : IStatisticsMonitoringItem
    {
        public string Name { get; set; }
        public string GroupName { get; set; }

        [JsonIgnore]
        public IDictionary<string, IReinitableThreadSafeOperation> Properties { get; private set; }

        public void ReInit()
        {
            foreach (var monitoringItemProperty in Properties.Values)
                monitoringItemProperty.ReInit();
        }

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
