using System;
using System.Reflection;

namespace Monitoring.Models
{
    public class CommonMonitoringSet
    {
        public string AppName { get; }
        public string Version { get; }
        public string MachineName { get; }
        public string Configuration { get; }

        public CommonMonitoringSet(string environmentName)
        {
            AppName = Assembly.GetExecutingAssembly().GetName().Name;
            Version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            MachineName = Environment.MachineName;
            Configuration = environmentName;
        }
    }
}
