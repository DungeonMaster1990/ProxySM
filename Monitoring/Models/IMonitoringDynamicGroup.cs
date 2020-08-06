using System.Collections.Generic;

namespace Monitoring.Models
{
    interface IMonitoringDynamicGroup<T> where T : MonitoringItemBase
    {
        IDictionary<string, T> MonitoringItems { get; }
        string Name { get; }
    }
}
