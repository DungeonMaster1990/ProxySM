using System.Threading;

namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Потокобезопасное получение максимального значения
    /// </summary>
    public class ReinitableThreadSafeMax: IThreadSafeOperation<long>
    {
        private long _value = 0;

        public void ReInit()
        {
            _value = 0;
        }

        /// <summary>
        /// Сравнение значений и выборм максимального
        /// </summary>
        /// <param name="value"></param>
        public void Add(long value)
        {
            if (_value < value)
                Interlocked.Exchange(ref _value, value);
        }

        public long Value { get; }
    }
}
