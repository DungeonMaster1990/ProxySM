using System.Collections.Concurrent;

namespace Monitoring.Models
{
    public class StatisticsMonitoringGroup<T> : IStatisticsMonitoringGroup<T> where T : IStatisticsMonitoringItem
    {
        public ConcurrentDictionary<string, T> MonitoringItems { get; internal set; }
        private bool _addedNewGroups = false;
        private readonly StatisticsItemFactory<T> _statisticsItemFactory;
        public string Name { get; }

        public StatisticsMonitoringGroup(string name, StatisticsItemFactory<T> statisticsItemFactory)
        {
            Name = name;
            MonitoringItems = new ConcurrentDictionary<string, T>();
            _statisticsItemFactory = statisticsItemFactory;
        }

        public void AddItem(T item)
        {
            MonitoringItems.GetOrAdd(item.Name, item);
            _addedNewGroups = true;
        }

        public T GetOrAddItem(string name)
        {
            if (MonitoringItems.ContainsKey(name))
                return MonitoringItems[name];
            else
            {
                T item = _statisticsItemFactory.CreateItem(name);
                MonitoringItems.GetOrAdd(name, item);
                return item;
            }
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
