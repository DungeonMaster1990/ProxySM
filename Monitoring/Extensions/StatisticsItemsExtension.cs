using System;
using Monitoring.Models;

namespace Monitoring.Extensions
{
    /// <summary>
    /// Методы расширения для полного набора мониторинговыз item's и групп
    /// </summary>
    public static class StatisticsItemsExtension
    {
        /// <summary>
        /// Вызов реинициализации для item's
        /// </summary>
        /// <param name="items"></param>
        public static void ReInit(this StatisticsItemsFullSet items)
        {
            items.ForEach(x => x.ReInit());
        }

        /// <summary>
        /// Применение action для каждого элемента fullset
        /// </summary>
        /// <param name="items"></param>
        /// <param name="action"></param>
        public static void ForEach(this StatisticsItemsFullSet items, Action<IStatisticsMonitoringItem> action)
        {
            foreach (var item in items.GroupItems.Values)
                action(item);

            foreach (var item in items.Items.Values)
                action(item);
        }
    }
}
