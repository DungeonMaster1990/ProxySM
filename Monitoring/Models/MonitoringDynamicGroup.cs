using System.Collections.Generic;

namespace Monitoring.Models
{
    public class MonitoringDynamicGroup<T>: IMonitoringDynamicGroup<T> where T : MonitoringItemBase
    {
        public IList<T> MonitoringItems { get; }
        private bool _addedNewGroups = false;
        public string Name { get; }
        public MonitoringDynamicGroup(string name)
        {
            Name = name;
            MonitoringItems = new List<T>();
        }

        public void Add(T monitoringItem)
        {
            MonitoringItems.Add(monitoringItem);
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
