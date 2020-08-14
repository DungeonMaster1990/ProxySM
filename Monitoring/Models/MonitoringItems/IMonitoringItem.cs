using Newtonsoft.Json.Linq;

namespace Monitoring.Models
{
    /// <summary>
    /// Мониторинговй item
    /// </summary>
    public interface IMonitoringItem
    {
        string Name { get; set; }

        JObject GetJObject();
    }
}
