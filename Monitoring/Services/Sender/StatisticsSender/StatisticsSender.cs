using Microsoft.Extensions.Options;
using Monitoring.Configurations;
using System;
using System.Collections.Generic;
using System.Threading;
using Monitoring.Extensions;
using Monitoring.Models;
using Monitoring.Services.Sender;

namespace Monitoring.Services
{
    public class StatisticsSender: IStatisticsSender, IDisposable
    {
        private readonly MonitoringOptions _monitoringOptions;
        private readonly IEnumerable<IDestination> _destinations;

        private readonly CancellationToken _token;
        private Timer _timer = null;
        private StatisticsItemsFullSet _statisticItems;
        private ManualResetEvent _timerDisposed;

        public StatisticsSender(
            IOptions<MonitoringOptions> monitoringOptions,
            StatisticsItemsFullSet statisticItems,
            IMonitoringSender sender,
            CancellationToken token)
        {
            _monitoringOptions = monitoringOptions.Value;
            _statisticItems = statisticItems;
            _token = token;

            if (_monitoringOptions.RunImmediately)
                StartMonitoring();
        }

        public void StartMonitoring()
        {
            if (_monitoringOptions.EnableMonitoring)
            {
                var callback = new TimerCallback(SendStatistics);
                var _timer = new Timer(callback, null, TimeSpan.Zero, _monitoringOptions.SendInterval);
                _timerDisposed = new ManualResetEvent(false);
            }
        }

        public void StopMonitoring()
        {
            _timer?.Dispose(_timerDisposed);
            _timerDisposed?.WaitOne();
            _timerDisposed?.Dispose();
        }


        private void SendStatistics(object obj)
        {
            if (_token.IsCancellationRequested)
            {
                SendToReInitMonitoringItems();
                StopMonitoring();
                return;
            }

            SendToReInitMonitoringItems();
        }

        private void SendToReInitMonitoringItems()
        {
            foreach (var destination in _destinations)
                destination.SendStatistics(_statisticItems);

            _statisticItems.ReInit();
        }

        public void Dispose()
        {
            StopMonitoring();
        }
    }
}
