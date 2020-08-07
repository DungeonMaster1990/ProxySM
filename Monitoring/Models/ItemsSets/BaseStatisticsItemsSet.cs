using System.Collections.Generic;
using System.Linq;

namespace Monitoring.Models
{
    public abstract class BaseStatisticsItemsSet
    {
        public IList<StatisticsMonitoringItemBase> GetAllStatisticsMonitoringItems()
        {
            return GetType().GetProperties()
                .Where(p => p.GetType().IsAssignableFrom(typeof(StatisticsMonitoringItemBase)))
                .Select(x => x.GetValue(x) as StatisticsMonitoringItemBase).ToList();
        }
    }
}
