using Monitoring.Models;
using System.Collections.Generic;

namespace Monitoring.Services
{
    public interface IDestination
    {
        void Send(IEnumerable<MonitoringItemBase> items, IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups);
    }
}
