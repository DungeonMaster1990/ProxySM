using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.ConcurrentCounters
{
    public interface IThreadSafeOperation
    {
        void Reinit();
    }
}
