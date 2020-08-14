using System;

namespace Monitoring.Configurations
{
    /// <summary>
    /// Настройки мониторинга
    /// </summary>
    public class MonitoringOptions
    {
        /// <summary>
        /// Включение мониторинга
        /// </summary>
        public bool EnableMonitoring { get; set; } = true;
        /// <summary>
        /// Интервал отправки в destination's
        /// </summary>
        public TimeSpan SendInterval { get; set; } = TimeSpan.FromSeconds(10);
        /// <summary>
        /// Запускать ли мониторинг мониторинг мгновенно(отложенный запуск не добавлен)
        /// </summary>
        public bool RunImmediately { get; set; } = true;
    }
}
