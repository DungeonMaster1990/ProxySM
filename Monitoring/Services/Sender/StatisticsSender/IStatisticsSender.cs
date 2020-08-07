using System;

namespace Monitoring.Services.Sender
{
    public interface IStatisticsSender : IDisposable
    {
        void StartMonitoring();
        void StopMonitoring();
    }
}
