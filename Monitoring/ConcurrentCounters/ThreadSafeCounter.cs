using System.Threading;

namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Сбрасывается после реинициализации (отправка)
    /// </summary>
    public class ThreadSafeCounter: IThreadSafeOperation
    {
        long _value = 0;

        public long Value { get => _value; }

        public ThreadSafeCounter Add(long addValue)
        {
            Interlocked.Add(ref _value, addValue);
            return this;
        }
        public ThreadSafeCounter Increment()
        {
            Interlocked.Increment(ref _value);
            return this;
        }

        public static ThreadSafeCounter operator ++(ThreadSafeCounter value)
        {
            return value.Increment();
        }

        public static ThreadSafeCounter operator +(ThreadSafeCounter value, long valueToAdd)
        {
            return value.Add(valueToAdd);
        }

        public virtual void Reinit() => _value = 0;
    }
}
