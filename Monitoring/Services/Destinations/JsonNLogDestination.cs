using Microsoft.Extensions.Options;
using NLog;
using Monitoring.Models;
using Monitoring.Extensions;
using Monitoring.Configurations;

namespace Monitoring.Services
{
    /// <summary>
    /// JSON destiantion
    /// </summary>
    public class JsonNLogDestination : IDestination
    {
        private ILogger _log = LogManager.GetLogger("Statistics");
        private const string _prefix = "json";

        private CommonMonitoringSet _commonMonitoringSet;
        private MonitoringOptions _monitoringOptions;
        public JsonNLogDestination(CommonMonitoringSet commonMonitoringSet, IOptions<MonitoringOptions> options)
        {
            _monitoringOptions = options.Value;
            _commonMonitoringSet = commonMonitoringSet;
        }

        /// <summary>
        /// Отправка item's в лог
        /// </summary>
        /// <param name="items">item's</param>
        public void SendStatistics(StatisticsItemsFullSet items)
        {
            if (_monitoringOptions.EnableMonitoring)
                items.ForEach(x => SendOneItem(_log, x));
        }

        /// <summary>
        /// Отправка item в лог
        /// </summary>
        /// <param name="log">логгер</param>
        /// <param name="monitoringItem">item</param>
        public void SendOneItem(ILogger log, IMonitoringItem monitoringItem)
        {
            var wrappedItem = new MonitoringItemWrapper<IMonitoringItem>(monitoringItem, _commonMonitoringSet);
            var loggerName = $"{_prefix}.{log.Name}";

            //loggers are cached in NLog core
            var jsonLog = LogManager.GetLogger(loggerName);

            var theEvent = new LogEventInfo() { Level = LogLevel.Info };

            theEvent.Properties["class"] = log.Name;
            theEvent.Properties["Detail"] = wrappedItem.GetJson();

            jsonLog.Log(theEvent);
        }
    }
}
