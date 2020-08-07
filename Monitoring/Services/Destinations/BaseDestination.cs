using Monitoring.Models;
using System.Runtime.CompilerServices;
using NLog;

namespace Monitoring.Services
{
    public interface IDestination
    {
        void SendStatistics(StatisticsItemsFullSet items);
        void SendOneItem(ILogger log, IMonitoringItem monitoringItem);
    }
}
