using Monitoring.ConcurrentCounters;
using System;
using System.Linq;

namespace Monitoring.Models
{
    public class StatisticsItemFactory<T> where T : IStatisticsMonitoringItem
    {
        public T CreateItem(string name = null)
        {
            var item = (T)Activator.CreateInstance(typeof(T));

            item.Properties = typeof(T).GetProperties()
                .Where(p => typeof(IReinitableThreadSafeOperation).IsAssignableFrom(p.PropertyType))
                .ToDictionary(x => x.Name, x => (IReinitableThreadSafeOperation)x.GetValue(item));

            item.Name = name ?? item.GetType().Name;
            return item;
        }
    }
}
