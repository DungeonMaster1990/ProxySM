using System;
using Monitoring.Extensions;

namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Потокобезопасный счетчик среднего времени
    /// </summary>
    public class ReinitableThreadSafeAverageTime: IThreadSafeOperation<TimeSpan>
    {
        /// <summary>
        /// Количество тиков
        /// </summary>
        private long _count = 0;
        /// <summary>
        /// Сумма тиков
        /// </summary>
        private long _ticks = 0;

        /// <summary>
        /// Расчет среднего тика
        /// </summary>
        public TimeSpan Value => TimeSpan.FromTicks(_count == 0 ? (long)0 : _ticks / _count);

        /// <summary>
        /// Перегрузка оператора + для добавления тика
        /// </summary>
        /// <param name="value">значение</param>
        /// <param name="valueToAdd">добавляемое значение</param>
        /// <returns></returns>
        public static ReinitableThreadSafeAverageTime operator +(ReinitableThreadSafeAverageTime value, TimeSpan valueToAdd)
        {
            value.Add(valueToAdd);
            return value;
        }

        /// <summary>
        /// Реинициализация (обнуление количество тиков и тиков)
        /// </summary>
        public void ReInit()
        {
            _count = 0;
            _ticks = 0;
        }

       /// <summary>
       /// Добавление тиков и инкремент счетчика тиков
       /// </summary>
       /// <param name="item"></param>
        public void Add(TimeSpan item)
        {
            _count.Increment();
            _ticks.Add(item.Ticks);
        }
    }
}
