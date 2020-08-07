namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Не сбрасывается при реинициализации
    /// </summary>
    public class ReinitableThreadSafeTotalCounter: ReinitableThreadSafeCounter
    {
        public override void ReInit()
        {
        }
    }
}
