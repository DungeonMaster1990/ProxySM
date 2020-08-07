using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Monitoring.Extensions
{
    public static class InterlockedExtentions
    {
        public static long Increment(this ref long value)
        {
            Interlocked.Increment(ref value);
            return value;
        }
        public static int Increment(this ref int value)
        {
            Interlocked.Increment(ref value);
            return value;
        }

        public static long Add(this ref long value, long addValue)
        {
            Interlocked.Add(ref value, addValue);
            return value;
        }

        public static int Add(this ref int value, int addValue)
        {
            Interlocked.Add(ref value, addValue);
            return value;
        }

        public static int GetMax(this ref int value1, int value2)
        {
            if (value1 < value2)
                Interlocked.Exchange(ref value1, value2);
            return value1;
        }

        public static long GetMax(this ref long value1, long value2)
        {
            if (value1 < value2)
                Interlocked.Exchange(ref value1, value2);
            return value1;
        }
    }
}
