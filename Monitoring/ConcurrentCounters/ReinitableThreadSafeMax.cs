using System;
using System.Collections.Generic;
using System.Text;
using Monitoring.Extensions;

namespace Monitoring.ConcurrentCounters
{
    public class ReinitableThreadSafeMax: IThreadSafeOperation<long>
    {
        private long _value = 0;

        public void ReInit()
        {
            _value = 0;
        }

        public void Add(long item)
        {
            _value.GetMax(item);
        }

        public long Value { get; }
    }
}
