using System.Collections.Generic;

namespace Monitoring.Models
{
    public class StatisticsItemsFullSet
    {
        public readonly IDictionary<string, StatisticsMonitoringItemBase> Items;
        public readonly IDictionary<string, StatisticsMonitoringGroup<StatisticsMonitoringItemBase>> Groups;

        public StatisticsItemsFullSet(IDictionary<string, StatisticsMonitoringItemBase> items,
            IDictionary<string, StatisticsMonitoringGroup<StatisticsMonitoringItemBase>> dynamicGroups)
        {
            Items = items;
            Groups = dynamicGroups;
        }
    }
}
