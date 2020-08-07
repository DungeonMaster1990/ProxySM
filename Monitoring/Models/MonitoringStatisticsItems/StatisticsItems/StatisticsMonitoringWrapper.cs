using System;
using System.Collections.Generic;
using System.Text;
using Monitoring.Models;
using Monitoring.Models.MonitoringStatisticsItems;

namespace Monitoring.Services
{
    public class StatisticsMonitoringWrapper<T>: IStatisticsMonitoringWrapper<T> where T: StatisticsMonitoringItemBase
    {
        public T Item { get; }

        public StatisticsMonitoringWrapper(MonitoringControl monitoring, T item)
        {
            if (!monitoring.Statistics.Items.ContainsKey(typeof(T).Name))
                monitoring.Statistics.Items[typeof(T).Name] = item;

            Item = item;
        }
    }
}
