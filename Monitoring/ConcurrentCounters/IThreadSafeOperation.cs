namespace Monitoring.ConcurrentCounters
{
    public interface IThreadSafeOperation<T>: IReinitableThreadSafeOperation where T : struct
    {
        void Add(T item);
        T Value { get; }
    }
}
