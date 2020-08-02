using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Models
{
    interface IMonitoringDynamicGroup<T> where T : MonitoringItemBase
    {
        IDictionary<string, T> MonitoringGroup { get; }
    }
}
