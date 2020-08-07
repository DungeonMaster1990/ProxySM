using System;
using Monitoring.Models;

namespace Monitoring.Extensions
{
    public static class StatisticsItemsExtension
    {
        public static void ReInit(this StatisticsItemsFullSet items)
        {
            items.ForEach(x=>x.ReInit());
        }

        public static void ForEach(this StatisticsItemsFullSet items, Action<StatisticsMonitoringItemBase> action)
        {
            foreach (var group in items.Groups.Values)
            foreach (var item in group.MonitoringItems.Values)
                action(item);

            foreach (var item in items.Items.Values)
                action(item);
        }
    }
}
