using System.Collections.Generic;

namespace Monitoring.Models
{
    interface IMonitoringDynamicGroup<T> where T : MonitoringItemBase
    {
        IList<T> MonitoringItems { get; }
        string Name { get; }
    }
}
