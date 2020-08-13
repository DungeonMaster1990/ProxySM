using Monitoring.ConcurrentCounters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Monitoring.Models
{
    public interface IMonitoringItem
    {
        string Name { get; set; }
        string GroupName { get; set; }

        [JsonIgnore]
        IDictionary<string, IReinitableThreadSafeOperation> Properties { get; }
        void SetProperties();

        JObject GetJObject();
    }
}
