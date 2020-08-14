﻿using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Monitoring.Core
{
    /// <summary>
    /// [ServiceFilterAttribute(typeof(MonitoringSendRequestFilter))] атрибут для записи информации о запросе
    /// </summary>
    public class MonitoringSendRequestFilter: Attribute, IAsyncActionFilter
    {
        private readonly RequestMonitoringItem _item;
        public MonitoringSendRequestFilter(RequestMonitoringItem item)
        {
            _item = item;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            _item.Start = DateTime.Now;
            _item.Action = context.ActionDescriptor.DisplayName;
            
            _item.RequestParameters = context.ActionArguments.ToDictionary(x => x.Key, x => x.Value.GetType().IsSerializable ? x.Value : null);
            _item.HttpMethod = context.HttpContext.Request.Method;
            _item.UserHostAddress = context.HttpContext.Request.Host.Host;
            _item.UserHostName = Dns.GetHostEntry(context.HttpContext.Request.Host.Host).HostName;
            _item.Port = context.HttpContext.Request.Host.Port;
            _item.TraceIdentifier = context.HttpContext.TraceIdentifier;
            await next();
            _item.Finish = DateTime.Now;
            _item.ResponseOutput = null;
        }
    }
}
