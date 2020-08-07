using Monitoring.Models;
using NLog;

namespace Monitoring.Services
{
    public interface IMonitoringSender
    {
        void Send(ILogger log, IMonitoringItem monitoringItem);
    }
}
