using Monitoring.ConcurrentCounters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Monitoring.Models
{
    public interface IMonitoringItem
    {
        string Name { get; set; }

        [JsonIgnore]
        IDictionary<string, IReinitableThreadSafeOperation> Properties { get; set; }

        JObject GetJObject();
    }
}
