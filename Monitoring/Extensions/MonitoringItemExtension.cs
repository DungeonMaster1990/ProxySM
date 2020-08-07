using System;
using System.Collections.Generic;
using System.Text;
using Monitoring.Models;
using Monitoring.Services;
using NLog;

namespace Monitoring.Extensions
{
    public static class MonitoringItemExtension
    {
        public static void Send(this IMonitoringItem monitoringItem, ILogger log, IEnumerable<IDestination> destinations)
        {
            foreach (var destination in destinations)
                destination.SendOneItem(log, monitoringItem);
        }
    }
}