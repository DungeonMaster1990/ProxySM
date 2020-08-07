using System;
using Monitoring.Extensions;

namespace Monitoring.ConcurrentCounters
{
    public class ReinitableThreadSafeAverageTime: IThreadSafeOperation<TimeSpan>
    {
        private long _count = 0;
        private long _ticks = 0;

        public TimeSpan Value => TimeSpan.FromTicks(_count == 0 ? (long)0 : _ticks / _count);

        public static ReinitableThreadSafeAverageTime operator +(ReinitableThreadSafeAverageTime value, TimeSpan valueToAdd)
        {
            value.Add(valueToAdd);
            return value;
        }

        public void ReInit()
        {
            _count = 0;
            _ticks = 0;
        }

        public void Add(TimeSpan item)
        {
            _count.Increment();
            _ticks.Add(item.Ticks);
        }
    }
}
