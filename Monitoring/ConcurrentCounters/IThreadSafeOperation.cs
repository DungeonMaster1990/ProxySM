namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Интерфейс потокобезопасных операций
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IThreadSafeOperation<T>: IReinitableThreadSafeOperation where T : struct
    {
        void Add(T item);
        T Value { get; }
    }
}
