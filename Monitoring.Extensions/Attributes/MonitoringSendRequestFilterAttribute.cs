using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Monitoring.Models;
using Monitoring.Services;
using NLog;

namespace Monitoring.Attributes
{
    /// <summary>
    /// [ServiceFilterAttribute(typeof(MonitoringSendRequestFilterAttribute))] Синхронный атрибут для записи информации о запросе 
    /// RequestMonitoringItem подключается как Scoped
    /// </summary>
    public class MonitoringSendRequestFilterAttribute: Attribute, IActionFilter
    {
        private readonly RequestMonitoringItem _item;
        private readonly IMonitoringSender _monitoringSender;
        private static readonly ILogger _log = LogManager.GetCurrentClassLogger();
        public MonitoringSendRequestFilterAttribute(RequestMonitoringItem item, IMonitoringSender monitoringSender)
        {
            _item = item;
            _monitoringSender = monitoringSender;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _item.Start = DateTime.Now;
            _item.Action = context.ActionDescriptor.DisplayName;
            _item.RequestParameters = context.ActionArguments.ToDictionary(x => x.Key, x => x.Value.GetType().IsSerializable ? x.Value : null);
            _item.HttpMethod = context.HttpContext.Request.Method;
            _item.UserHostAddress = context.HttpContext.Request.Host.Host;
            _item.UserHostName = Dns.GetHostEntry(context.HttpContext.Request.Host.Host).HostName;
            _item.Port = context.HttpContext.Request.Host.Port;
            _item.TraceIdentifier = context.HttpContext.TraceIdentifier;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _item.Finish = DateTime.Now;
            _item.ResponseOutput = null;
            _monitoringSender.Send(_log, _item);
        }
    }
}
