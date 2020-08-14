using System;

namespace Monitoring.Services.Sender
{
   /// <summary>
   /// Интерфейс для отправки статистических мониторинговых объектов
   /// </summary>
    public interface IStatisticsSender : IDisposable
    {
        void StartMonitoring();
        void StopMonitoring();
    }
}
