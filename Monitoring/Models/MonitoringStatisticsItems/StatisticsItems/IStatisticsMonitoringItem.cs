using Monitoring.ConcurrentCounters;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Monitoring.Models
{
    /// <summary>
    /// Дполнение мониторингового item методом реинициализации
    /// </summary>
    public interface IStatisticsMonitoringItem : IMonitoringItem
    {
        string GroupName { get; set; }

        [JsonIgnore]
        IDictionary<string, IReinitableThreadSafeOperation> Properties { get; }
        void SetProperties();
        void ReInit();
    }
}
