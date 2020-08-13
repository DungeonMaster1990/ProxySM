using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Monitoring.Models
{
    internal class MonitoringItemWrapper<T> where T : IMonitoringItem
    {
        private CommonMonitoringSet _commonSet;
        public T Item;

        public MonitoringItemWrapper(T monitoringItem, CommonMonitoringSet commonSet)
        {
            Item = monitoringItem;
            _commonSet = commonSet;
        }

        public string GetJson()
        {
            var monitoringJO = JObject.FromObject(Item);
            foreach (var property  in _commonSet.JObject)
            {
                monitoringJO.Add(property.Key, property.Value);
            }

            return monitoringJO.ToString(Formatting.None);
        }
    }
}
