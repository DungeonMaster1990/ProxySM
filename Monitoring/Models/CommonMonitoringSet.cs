using System;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Monitoring.Models
{
    /// <summary>
    /// Общий мониторинговый set
    /// </summary>
    public class CommonMonitoringSet
    {

        public string AppName { get; }
        public string Version { get; }
        public string MachineName { get; }
        public string Configuration { get; }
        [JsonIgnore]
        public JObject JObject { get; }

        public CommonMonitoringSet(string environmentName)
        {
            AppName = Assembly.GetExecutingAssembly().GetName().Name;
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MachineName = Environment.MachineName;
            Configuration = environmentName;
            JObject = JObject.FromObject(this);
        }
    }
}
