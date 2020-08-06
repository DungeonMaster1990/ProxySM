using Monitoring.Models;
using System.Collections.Generic;

namespace Monitoring.Services
{
    public abstract class BaseDestination
    {
        protected readonly IEnumerable<MonitoringItemBase> _monitoringItems;
        protected readonly IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> _dynamicGroups;
        public BaseDestination(IEnumerable<MonitoringItemBase> monitoringItems, IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups)
        {
            _monitoringItems = monitoringItems;
            _dynamicGroups = dynamicGroups;
        }
        public abstract void Send();
    }
}
