using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace Monitoring.Models
{
    public class StatisticsItemsFullSet
    {
        public readonly IDictionary<string, StatisticsMonitoringItemBase> Items;
        public readonly IDictionary<string, StatisticsMonitoringGroup<IStatisticsMonitoringItem>> Groups;
        private readonly HashSet<(string Name, Type MonitoringItemType)> _groupNameAndGroupMonitoringItemsTypeSet;
        private readonly ConcurrentDictionary<string, StatisticsItemFactory<IStatisticsMonitoringItem>> _itemFactories;
        public StatisticsItemsFullSet(IDictionary<string, StatisticsMonitoringItemBase> items,
            IDictionary<string, StatisticsMonitoringGroup<IStatisticsMonitoringItem>> groups)
        {
            Items = items;
            Groups = groups;
            _groupNameAndGroupMonitoringItemsTypeSet = new HashSet<(string Name, Type MonitoringItemType)>();
            _itemFactories = new ConcurrentDictionary<string, StatisticsItemFactory<IStatisticsMonitoringItem>>();
        }

        public StatisticsMonitoringGroup<IStatisticsMonitoringItem> GetOrCreateMonitoringGroup<MonitoringItem>(string groupName)
            where MonitoringItem : IStatisticsMonitoringItem
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
                var factory = _itemFactories.GetOrAdd(groupName, new StatisticsItemFactory<IStatisticsMonitoringItem>());

                var group = new StatisticsMonitoringGroup<IStatisticsMonitoringItem>(groupName, factory);
                Groups.Add(groupName, group);
                return group;
            }
        }
    }
}
