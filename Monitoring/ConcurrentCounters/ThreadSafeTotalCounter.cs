using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.ConcurrentCounters
{
    /// <summary>
    /// Не сбрасывается при реинициализации
    /// </summary>
    public class ThreadSafeTotalCounter: ThreadSafeCounter
    {
        public override void Reinit()
        {
        }
    }
}
