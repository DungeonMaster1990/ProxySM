using Newtonsoft.Json.Linq;


namespace Monitoring.Models
{
    internal class MonitoringItemWrapper
    {
        private CommonMonitoringSet _commonSet;
        private IMonitoringItem _monitoringItem;

        public MonitoringItemWrapper(IMonitoringItem monitoringItem, CommonMonitoringSet commonSet)
        {
            _monitoringItem = monitoringItem;
            _commonSet = commonSet;
        }

        public string GetJson()
        {
            var monitoringJO = JObject.FromObject(_monitoringItem);
            var commonSet =  JToken.FromObject(_commonSet);
            monitoringJO.AddAfterSelf(commonSet);


            return monitoringJO.ToString();
        }
    }
}
