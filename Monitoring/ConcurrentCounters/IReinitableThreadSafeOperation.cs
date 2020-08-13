namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Интерфей реинициализации мониторингового item
    /// </summary>
    public interface IReinitableThreadSafeOperation
    {
        void ReInit();
    }
}
