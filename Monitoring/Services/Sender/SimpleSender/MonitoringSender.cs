﻿using Microsoft.Extensions.Options;
using Monitoring.Configurations;
using Monitoring.Models;
using System.Collections.Generic;
using NLog;

namespace Monitoring.Services
{
    public class MonitoringSender: IMonitoringSender
    {
        private readonly MonitoringOptions _monitoringOptions;
        private readonly IEnumerable<IDestination> _destinations;
        
        public MonitoringSender(
            IOptions<MonitoringOptions> monitoringOptions,
            IEnumerable<IDestination> destinations)
        {
            _monitoringOptions = monitoringOptions.Value;
            _destinations = destinations;
        }

        public void Send(ILogger log, IMonitoringItem monitoringItem)
        {
            if (_monitoringOptions.EnableMonitoring)
            {
                foreach (var destination in _destinations)
                    destination.SendOneItem(log, monitoringItem);
            }
        }
    }
}
