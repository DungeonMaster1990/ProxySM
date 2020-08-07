using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Models.MonitoringStatisticsItems
{
    public interface IStatisticsMonitoringWrapper<out T> where T: StatisticsMonitoringItemBase
    {
        T Item { get; }
    }
}
