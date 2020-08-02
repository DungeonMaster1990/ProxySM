using Monitoring.ConcurrentCounters;
using System.Collections.Generic;
using System.Linq;

namespace Monitoring.Models
{
    public abstract class MonitoringItemBase
    {
        internal readonly IDictionary<string, IThreadSafeOperation> Properties;

        public MonitoringItemBase()
        {
            Properties = GetType().GetProperties()
                .Where(p => p.GetType().IsAssignableFrom(typeof(IThreadSafeOperation)))
                .ToDictionary(x=>x.Name, x => x.GetValue(x) as IThreadSafeOperation);
        }
        public void Reinit()
        {
            foreach (var monitoringItemProperty in Properties.Values)
                monitoringItemProperty.Reinit();
        }
    }
}
