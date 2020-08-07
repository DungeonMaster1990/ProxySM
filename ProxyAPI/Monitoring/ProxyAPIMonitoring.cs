﻿using Monitoring.Models;

namespace ProxyAPI.Monitoring
{
    public class ProxyAPIMonitoring: BaseStatisticsItemsSet
    {
        public ProxyAPIExceptionMonitoringItem ExceptionMonitoring { get;}
        public ProxyAPIMonitoringItem BasicMonitoring { get; }

        public ProxyAPIMonitoring(ProxyAPIExceptionMonitoringItem exceptionMonitoring,
            ProxyAPIMonitoringItem basicMonitoring)
        {
            ExceptionMonitoring = exceptionMonitoring;
            BasicMonitoring = basicMonitoring;
        }
    }
}
