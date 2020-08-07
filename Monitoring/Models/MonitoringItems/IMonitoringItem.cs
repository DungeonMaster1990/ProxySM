using Monitoring.ConcurrentCounters;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;

namespace Monitoring.Models
{
    public interface IMonitoringItem
    {
        string Name { get; }
        JObject GetJObject();
    }
}
