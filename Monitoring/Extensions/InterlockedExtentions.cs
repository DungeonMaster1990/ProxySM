using System.Threading;

namespace Monitoring.Extensions
{
    /// <summary>
    /// Методы расширения для потокобезопасных операций с item's
    /// </summary>
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

    }
}
