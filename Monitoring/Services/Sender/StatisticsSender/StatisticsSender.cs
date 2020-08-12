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

        public CancellationToken _token { get; set; }
        private Timer _timer = null;
        private StatisticsItemsFullSet _fullSet;
        private ManualResetEvent _timerDisposed;

        public StatisticsSender(
            IOptions<MonitoringOptions> monitoringOptions,
            IEnumerable<IDestination> destinations,
            StatisticsItemsFullSet statisticItems)
        {
            var tokenSource = new CancellationTokenSource();
            _token = tokenSource.Token;

            _destinations = destinations;
            _monitoringOptions = monitoringOptions.Value;
            _fullSet = statisticItems;

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
                SendAndReInitMonitoringItems();
                StopMonitoring();
                return;
            }

            SendAndReInitMonitoringItems();
        }

        private void SendAndReInitMonitoringItems()
        {
            foreach (var destination in _destinations)
                destination.SendStatistics(_fullSet);

            _fullSet.ReInit();
        }

        public void Dispose()
        {
            StopMonitoring();
        }
    }
}
