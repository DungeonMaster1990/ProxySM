using Microsoft.Extensions.Options;
using Monitoring.Configurations;
using Monitoring.Models;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Monitoring.Services
{
    public class MonitoringSender: IDisposable
    {
        private readonly MonitoringOptions _monitoringOptions;
        private readonly IEnumerable<IDestination> _destinations;
        private readonly IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> _dynamicGroups;
        private readonly CancellationToken _token;
        private Timer _timer;
        private ManualResetEvent _timerDisposed;
        public MonitoringSender(IOptions<MonitoringOptions> monitoringOptions, IEnumerable<MonitoringItemBase> baseGroup, IList<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups, IEnumerable<IDestination> destinations, CancellationToken token)
        {
            var baseMonitoringGroup = new MonitoringDynamicGroup<>
            dynamicGroups.Add(baseGroup)
            _monitoringOptions = monitoringOptions.Value;
            _token = token;
        }

        public void StartMonitoring()
        {
            var callback = new TimerCallback(Send);
            var _timer = new Timer(callback, null, TimeSpan.Zero, _monitoringOptions.SendInterval);
            _timerDisposed = new ManualResetEvent(false);
        }

        public void StopMonitoring()
        {
            _timer.Dispose(_timerDisposed);
            _timerDisposed.WaitOne();
            _timerDisposed.Dispose();
        }


        private void Send(object obj)
        {
            if (_token.IsCancellationRequested)
            {
                SendToMonitoringAndReinitMonitoringItems();   
                StopMonitoring();
                return;
            }

            SendToMonitoringAndReinitMonitoringItems();
        }

        private void SendToMonitoringAndReinitMonitoringItems()
        {
            foreach (var destination in _destinations)
                destination.Send(_monitoringItems, _dynamicGroups);

            foreach (var group in _dynamicGroups)
                foreach (var item in group.MonitoringItems)
                    item.Reinit();
        }


    public void Dispose()
    {
        StopMonitoring();
    }
}
}
