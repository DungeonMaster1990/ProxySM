using System;
using System.Collections;
using Monitoring.Models;

namespace Monitoring.Extensions
{
    public static class StatisticsItemsExtension
    {
        public static void ReInit(this StatisticsItemsFullSet items)
        {
            items.ForEach(x => x.ReInit());
        }

        public static void ForEach(this StatisticsItemsFullSet items, Action<IStatisticsMonitoringItem> action)
        {
            foreach (var item in items.GroupItems.Values)
                action(item);

            foreach (var item in items.Items.Values)
                action(item);
        }
    }
}
