using System.Threading;
using Monitoring.Extensions;

namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Потокобезопасный счетчик
    /// </summary>
    public class ReinitableThreadSafeCounter: IThreadSafeOperation<long>
    {
        long _value = 0;

        /// <summary>
        /// Получением значения счетчика
        /// </summary>
        public long Value { get => _value; }

        /// <summary>
        /// Увелчиение счетчика
        /// </summary>
        /// <param name="addValue">значение увеличения счетчика</param>
        public void Add(long addValue)
        {
            Interlocked.Add(ref _value, addValue);
        }

        /// <summary>
        /// Перегрузка оператора ++ для увелчиения счетчика на 1
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        public static ReinitableThreadSafeCounter operator ++(ReinitableThreadSafeCounter counter)
        {
            counter._value.Increment();
            return counter;
        }

        /// <summary>
        /// Перегрузка оператора + для увеличения счетчика
        /// </summary>
        /// <param name="value">счетчик</param>
        /// <param name="valueToAdd">значение увеличения</param>
        /// <returns></returns>
        public static ReinitableThreadSafeCounter operator +(ReinitableThreadSafeCounter value, long valueToAdd)
        {
            value.Add(valueToAdd);
            return value;
        }

        /// <summary>
        /// Сброс счетчика
        /// </summary>
        public virtual void ReInit() => _value = 0;
    }
}
