using Newtonsoft.Json.Linq;


namespace Monitoring.Models
{
    public class MonitoringItemWrapper<T> where T : IMonitoringItem
    {
        private CommonMonitoringSet _commonSet;
        public T Item;

        public MonitoringItemWrapper(T monitoringItem, CommonMonitoringSet commonSet)
        {
            Item = monitoringItem;
            _commonSet = commonSet;
        }

        //public string GetJson()
        //{
        //    var monitoringJO = JObject.FromObject(_monitoringItem);
        //    var commonSet = JToken.FromObject(_commonSet);
        //    monitoringJO.AddAfterSelf(commonSet);


        //    return monitoringJO.ToString();
        //}
    }
}
