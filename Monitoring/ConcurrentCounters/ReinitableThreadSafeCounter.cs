using System.Threading;
using Monitoring.Extensions;

namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Сбрасывается после реинициализации (отправка)
    /// </summary>
    public class ReinitableThreadSafeCounter: IThreadSafeOperation<long>
    {
        long _value = 0;

        public long Value { get => _value; }

        public void Add(long addValue)
        {
            Interlocked.Add(ref _value, addValue);
        }

        public static ReinitableThreadSafeCounter operator ++(ReinitableThreadSafeCounter counter)
        {
            counter._value.Increment();
            return counter;
        }

        public static ReinitableThreadSafeCounter operator +(ReinitableThreadSafeCounter value, long valueToAdd)
        {
            value.Add(valueToAdd);
            return value;
        }

        public virtual void ReInit() => _value = 0;
    }
}
