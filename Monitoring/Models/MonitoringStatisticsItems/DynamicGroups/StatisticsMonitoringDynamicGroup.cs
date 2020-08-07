using System.Collections.Generic;
using System.Linq;

namespace Monitoring.Models
{
    public class StatisticsMonitoringDynamicGroup<T> : IStatisticsMonitoringDynamicGroup<T> where T : IMonitoringItem
    {
        public IDictionary<string, T> MonitoringItems { get; }
        private bool _addedNewGroups = false;
        public string Name { get; }

        public StatisticsMonitoringDynamicGroup(string name, IEnumerable<T> items)
        {
            Name = name;

            if (items == null)
                MonitoringItems = new Dictionary<string, T>();
            else
                MonitoringItems = items.ToDictionary(x => x.Name, x=>(T)x);
        }

        public void Add(T monitoringItem)
        {
            MonitoringItems.Add(monitoringItem.Name, monitoringItem);
            _addedNewGroups = true;
        }

        public bool CheckAdded()
        {
            if (_addedNewGroups)
            {
                _addedNewGroups = false;
                return true;
            }

            return false;
        }
    }
}
