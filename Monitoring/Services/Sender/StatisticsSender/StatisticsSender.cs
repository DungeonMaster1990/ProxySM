using System;
using System.Threading;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using Monitoring.Configurations;
using Monitoring.Extensions;
using Monitoring.Models;
using Monitoring.Services.Sender;

namespace Monitoring.Services
{
    /// <summary>
    /// sender статистических мониторинговых объектов в лог
    /// </summary>
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
            _destinations = destinations;
            _monitoringOptions = monitoringOptions.Value;
            _fullSet = statisticItems;

            if (_monitoringOptions.RunImmediately)
                StartMonitoring();
        }

        /// <summary>
        /// Стар мониторинга и начало сбора статистики
        /// </summary>
        public void StartMonitoring()
        {
            if (_monitoringOptions.EnableMonitoring)
            {
                var callback = new TimerCallback(SendStatistics);
                _timer = new Timer(callback, null, TimeSpan.Zero, _monitoringOptions.SendInterval);
                _timerDisposed = new ManualResetEvent(false);
            }
        }

        /// <summary>
        /// Остановка мониторинга
        /// </summary>
        public void StopMonitoring()
        {
            _timer?.Dispose(_timerDisposed);
            _timerDisposed?.WaitOne();
            _timerDisposed?.Dispose();
        }


        /// <summary>
        /// Отправка статистики
        /// </summary>
        /// <param name="obj"></param>
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

        /// <summary>
        /// Отправка и реинициализация
        /// </summary>
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
