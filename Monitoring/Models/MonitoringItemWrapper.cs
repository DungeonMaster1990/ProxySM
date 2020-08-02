using Newtonsoft.Json.Linq;

namespace Monitoring.Models
{
    internal class MonitoringItemWrapper
    {
        private CommonMonitoringSet _commonSet;
        private MonitoringItemBase _monitoringItem;

        public MonitoringItemWrapper(MonitoringItemBase monitoringItem, CommonMonitoringSet commonSet)
        {
            _monitoringItem = monitoringItem;
            _commonSet = commonSet;
        }

        public string GetJson()
        {
            var monitoringJO = JObject.FromObject(_monitoringItem);

            monitoringJO.AddAfterSelf(_commonSet);

            return monitoringJO.ToString();
        }
    }
}
