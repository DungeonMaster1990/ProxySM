using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Monitoring.Models
{
    /// <summary>
    /// Вспомогательный класс для склеивания JSON мониторигового item и общего монитоингового объекта
    /// </summary>
    /// <typeparam name="T">тип мониторингового item</typeparam>
    internal class MonitoringItemWrapper<T> where T : IMonitoringItem
    {
        private CommonMonitoringSet _commonSet;
        public T Item;

        public MonitoringItemWrapper(T monitoringItem, CommonMonitoringSet commonSet)
        {
            Item = monitoringItem;
            _commonSet = commonSet;
        }

        /// <summary>
        /// склеивание объектов, поданных в конструктор
        /// </summary>
        /// <returns></returns>
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
