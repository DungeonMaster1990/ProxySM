using Monitoring.Models;
using NLog;

namespace Monitoring.Services
{
    /// <summary>
    /// базовый destination для отправки
    /// </summary>
    public interface IDestination
    {
        void SendStatistics(StatisticsItemsFullSet items);
        void SendOneItem(ILogger log, IMonitoringItem monitoringItem);
    }
}
