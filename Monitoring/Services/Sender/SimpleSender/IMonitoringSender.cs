using Monitoring.Models;
using NLog;

namespace Monitoring.Services
{
    /// <summary>
    /// Интерфейс для отправки простых мониторинговых объектов
    /// </summary>
    public interface IMonitoringSender
    {
        void Send(ILogger log, IMonitoringItem monitoringItem);
    }
}
