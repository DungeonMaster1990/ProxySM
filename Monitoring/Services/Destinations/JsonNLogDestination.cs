using Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Services
{
    class JsonNLogDestination : IDestination
    {
        public void Send(IEnumerable<MonitoringItemBase> items, IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups)
        {
            throw new NotImplementedException();
        }
    }
}
