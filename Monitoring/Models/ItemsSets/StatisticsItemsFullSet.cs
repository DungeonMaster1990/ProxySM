using System.Collections.Generic;

namespace Monitoring.Models
{
    public class StatisticsItemsFullSet
    {
        public readonly IDictionary<string, IStatisticsMonitoringItem> Items;
        public readonly IDictionary<(string GroupName, string ItemName), IStatisticsMonitoringItem> GroupItems;
        public StatisticsItemsFullSet(IDictionary<string, IStatisticsMonitoringItem> items,
            IDictionary<(string GroupName, string ItemName), IStatisticsMonitoringItem> groupItems)
        {
            Items = items;
            GroupItems = groupItems;
        }

        public MonitoringItem GetOrCreateGroupItem<MonitoringItem>(string itemName, string groupName)
            where MonitoringItem: IStatisticsMonitoringItem, new()
        {
            if (GroupItems.ContainsKey((groupName, itemName)))
            {
                return (MonitoringItem)GroupItems[(groupName, itemName)];
            }
            else
            {
                var item = new MonitoringItem() { Name = itemName, GroupName = groupName };
                item.SetProperties();
                GroupItems.Add((groupName, itemName), item);
                return item;
            }
        }
    }
}
