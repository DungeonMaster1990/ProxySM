namespace Monitoring.Models
{
    /// <summary>
    /// Дполнение мониторингового item методом реинициализации
    /// </summary>
    public interface IStatisticsMonitoringItem : IMonitoringItem
    {
        void ReInit();
    }
}
