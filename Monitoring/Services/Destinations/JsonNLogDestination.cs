using System.Collections.Concurrent;
using Monitoring.Models;
using NLog;
using System.Collections.Generic;
using Monitoring.Extensions;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Options;
using Monitoring.Configurations;

namespace Monitoring.Services
{
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

        public void SendStatistics(StatisticsItemsFullSet items)
        {
            var methodName = nameof(SendStatistics);
            items.ForEach(x=>SendOneItem(_log, x));
        }

        public void SendOneItem(ILogger log, IMonitoringItem monitoringItem)
        {
            var wrappedItem = new MonitoringItemWrapper(monitoringItem, _commonMonitoringSet);
            var loggerName = $"{_prefix}.{log.Name}";
            //loggers are cached in NLog core
            var jsonLog = LogManager.GetLogger(loggerName);

            var theEvent = new LogEventInfo() {Level = LogLevel.Info};

            theEvent.Properties["class"] = log.Name;
            theEvent.Properties["Detail"] = wrappedItem.GetJson();

            jsonLog.Log(theEvent);
        }
    }
}
