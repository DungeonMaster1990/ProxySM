using System;
using System.Collections.Generic;
using System.Linq;

namespace Monitoring.Models
{
    public class StatisticsItemsFullSet
    {
        public readonly IDictionary<string, StatisticsMonitoringItemBase> Items;
        public readonly IDictionary<string, StatisticsMonitoringGroup<StatisticsMonitoringItemBase>> Groups;
        private readonly HashSet<(string Name, Type MonitoringItemType)> _groupNameAndGroupMonitoringItemsTypeSet;

        public StatisticsItemsFullSet(IDictionary<string, StatisticsMonitoringItemBase> items,
            IDictionary<string, StatisticsMonitoringGroup<StatisticsMonitoringItemBase>> groups)
        {
            Items = items;
            Groups = groups;
        }

        public object GetOrCreateMonitoringGroup<MonitoringItem>(string groupName)
        where MonitoringItem: StatisticsMonitoringItemBase
        {
            if (Groups.ContainsKey(groupName) && _groupNameAndGroupMonitoringItemsTypeSet.Contains((groupName, typeof(MonitoringItem))))
            {
                return Groups[groupName];
            }
            else
            {
                if (_groupNameAndGroupMonitoringItemsTypeSet.Any(x=>x.Name == groupName))
                    throw new Exception("GroupName with such name already exist but use other monitoringItem");
                _groupNameAndGroupMonitoringItemsTypeSet.Add((groupName, typeof(MonitoringItem)));
                return new StatisticsMonitoringGroup<MonitoringItem>(groupName);
            }
        }
    }
}
