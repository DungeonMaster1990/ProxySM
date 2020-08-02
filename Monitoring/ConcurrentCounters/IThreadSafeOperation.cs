using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.ConcurrentCounters
{
    interface IThreadSafeOperation
    {
        void Reinit();
    }
}
