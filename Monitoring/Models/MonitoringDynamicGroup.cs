using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Models
{
    public class MonitoringDynamicGroup<T>: IMonitoringDynamicGroup<T> where T : MonitoringItemBase
    {
        public IDictionary<string, T> MonitoringGroup { get; }
        private bool _addedNewGroups = false;
        public MonitoringDynamicGroup()
        {
            MonitoringGroup = new Dictionary<string, T>();
        }

        public void AddNewGroup(string groupName, T monitoringItem)
        {
            MonitoringGroup.Add(groupName, monitoringItem);
            _addedNewGroups = true;
        }

        public bool CheckNewGroupsAdded()
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
