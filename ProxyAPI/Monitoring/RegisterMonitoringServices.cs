using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Monitoring.Models;

namespace ProxyAPI.Monitoring
{
    public class RegisterMonitoringServices
    {
        public static void RegisterMonitoring (IConfigurationBuilder configurationBuilder)
        {

        }

        public IEnumerable<IMonitoringItem> FindAllIMonitoringItems()
        {
            AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p));
        }
    }
}
