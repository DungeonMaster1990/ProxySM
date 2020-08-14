using System;
using System.Collections.Generic;
using Monitoring.Models.MonitoringItems;

namespace Monitoring.Models
{
    /// <summary>
    /// Простой мониторинговый объект, используемый для записи информации о запросе
    /// </summary>
    public class RequestMonitoringItem: BaseMonitoringItem
    {
        public DateTime Start { get; set; }
        public DateTime Finish { get; set; }
        public TimeSpan Duration => Finish - Start;
        public string Action { get; set; }
        public string UserHostAddress { get; set; }
        public string UserHostName { get; set; }
        public int? Port { get; set; }
        public string HttpMethod { get; set; }
        public string RequestString { get; set; }
        public IDictionary<string, object> RequestParameters { get; set; }
        public int StatusCode { get; set; }
        public string Exception { get; set; }
        public IDictionary<string, object> ResponseOutput { get; set; }
        public string TraceIdentifier { get; set; }
    }
}
