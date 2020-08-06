using Monitoring.Models;
using NLog;
using System.Collections.Generic;

namespace Monitoring.Services
{
    class JsonNLogDestination : BaseDestination
    {
        private ILogger _log = LogManager.GetLogger("jsonElasticLog");
        private CommonMonitoringSet _commonMonitoringSet;
        public JsonNLogDestination(CommonMonitoringSet commonMonitoringSet,
            IEnumerable<MonitoringItemBase> monitoringItems,
            IEnumerable<MonitoringDynamicGroup<MonitoringItemBase>> dynamicGroups) : base(monitoringItems, dynamicGroups)
        {
            _commonMonitoringSet = commonMonitoringSet;
        }

        public override void Send()
        {
            foreach (var item in _monitoringItems)
                SendItem(item);

            foreach(var group in _dynamicGroups)
                foreach(var item in group.MonitoringItems)
                    SendItem(item);
        }
        private void SendItem(MonitoringItemBase monitoringItem)
        {
            var wrappedItem = new MonitoringItemWrapper(monitoringItem, _commonMonitoringSet);
            var type = monitoringItem.GetType();
            var theEvent = new LogEventInfo();
            theEvent.Level = LogLevel.Trace;
            theEvent.Properties["jsonlogname"] = _log.Name;
            theEvent.Properties["class"] = type;
            theEvent.Properties["Detail"] = wrappedItem.GetJson();
            _log.Log(theEvent);
        }
    }
}
