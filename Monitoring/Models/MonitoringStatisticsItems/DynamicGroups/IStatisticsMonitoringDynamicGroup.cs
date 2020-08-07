using System.Collections.Generic;

namespace Monitoring.Models
{
    interface IStatisticsMonitoringDynamicGroup<T> where T : IMonitoringItem
    {
        IDictionary<string, T> MonitoringItems { get; }
    }
}
