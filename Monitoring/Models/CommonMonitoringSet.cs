using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Monitoring.Models
{
    internal class CommonMonitoringSet
    {
        public string AppName { get; }
        public string Version { get; }
        public string MachineName { get; }
        public string Configuration { get; }
        [JsonIgnore]
        public JToken JToken { get; }
        public CommonMonitoringSet()
        {
            AppName = Assembly.GetExecutingAssembly().GetName().Name;
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MachineName = Environment.MachineName;
            Configuration = Assembly.GetExecutingAssembly().;
            JToken = JToken.FromObject(this);
        }
    }
}
