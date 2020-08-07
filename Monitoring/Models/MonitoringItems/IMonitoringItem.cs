using Newtonsoft.Json.Linq;

namespace Monitoring.Models
{
    public interface IMonitoringItem
    {
        string Name { get; }
        JObject GetJObject();
    }
}
