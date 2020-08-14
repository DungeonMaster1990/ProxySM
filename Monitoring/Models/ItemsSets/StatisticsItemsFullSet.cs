using System.Collections.Generic;

namespace Monitoring.Models
{
    /// <summary>
    /// fullset мониторинговых item's и групп item's
    /// </summary>
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

        /// <summary>
        /// Обобщенный типом item'а метод получения или создания группы по имени item'а и группы
        /// </summary>
        /// <typeparam name="MonitoringItem"></typeparam>
        /// <param name="itemName">имя item</param>
        /// <param name="groupName">имя группы</param>
        /// <returns>item</returns>
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
