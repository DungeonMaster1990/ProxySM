using System.Collections.Generic;

namespace Monitoring.Models
{
    public class MonitoringDynamicGroup<T>: IMonitoringDynamicGroup<T> where T : MonitoringItemBase
    {
        public IDictionary<string, T> MonitoringItems { get; }
        private bool _addedNewGroups = false;
        public string Name { get; }
        public MonitoringDynamicGroup(string name)
        {
            Name = name;
            MonitoringItems = new Dictionary<string, T>();
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
