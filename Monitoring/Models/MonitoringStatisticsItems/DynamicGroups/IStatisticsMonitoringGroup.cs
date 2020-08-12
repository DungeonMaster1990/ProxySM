using System.Collections.Concurrent;

namespace Monitoring.Models
{
    interface IStatisticsMonitoringGroup<T> where T : IMonitoringItem
    {
        ConcurrentDictionary<string, T> MonitoringItems { get; }
    }
}
