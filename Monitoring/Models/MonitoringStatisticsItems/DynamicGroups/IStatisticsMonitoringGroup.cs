using System.Collections.Generic;

namespace Monitoring.Models
{
    interface IStatisticsMonitoringGroup<T> where T : IMonitoringItem
    {
        IDictionary<string, T> MonitoringItems { get; }
    }
}
