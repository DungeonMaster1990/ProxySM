namespace Monitoring.Models
{
    public interface IStatisticsMonitoringWrapper<out T> where T: StatisticsMonitoringItemBase
    {
        T Item { get; }
    }
}
