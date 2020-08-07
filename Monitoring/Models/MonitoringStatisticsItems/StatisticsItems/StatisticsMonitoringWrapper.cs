namespace Monitoring.Models
{
    public class StatisticsMonitoringWrapper<T>: IStatisticsMonitoringWrapper<T> where T: StatisticsMonitoringItemBase
    {
        public T Item { get; }

        public StatisticsMonitoringWrapper(StatisticsItemsFullSet monitoring, T item)
        {
            if (!monitoring.Items.ContainsKey(typeof(T).Name))
                monitoring.Items[typeof(T).Name] = item;

            Item = item;
        }
    }
}
