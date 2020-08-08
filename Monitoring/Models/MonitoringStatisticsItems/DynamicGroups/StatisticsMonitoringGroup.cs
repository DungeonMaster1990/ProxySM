using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Monitoring.Models
{
    public class StatisticsMonitoringGroup<T> : IStatisticsMonitoringGroup<T> where T : StatisticsMonitoringItemBase
    {
        public IDictionary<string, T> MonitoringItems { get; internal set; }
        private bool _addedNewGroups = false;
        public string Name { get; }

        public StatisticsMonitoringGroup(string name)
        {
            Name = name;
            MonitoringItems = new ConcurrentDictionary<string, T>();
        }

        public void AddItem(T item)
        {
            MonitoringItems.Add(item.Name, item);
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
