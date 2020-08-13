namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Несбрасываемый при реинициализации счетчик (переполняется на long, не учтено)
    /// </summary>
    public class ReinitableThreadSafeTotalCounter: ReinitableThreadSafeCounter
    {
        public override void ReInit()
        {
        }
    }
}
