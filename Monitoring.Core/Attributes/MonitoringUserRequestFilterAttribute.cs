﻿using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
using Monitoring.Models;

namespace Monitoring.Attributes
{
    /// <summary>
    /// [ServiceFilterAttribute(typeof(MonitoringSendRequestFilterAttribute))] Синхронный атрибут для записи информации о запросе и юзере
    /// RequestUserMonitoringItem подключается как Scoped
    /// </summary>
    public abstract class MonitoringUserRequestFilterAttribute : Attribute, IActionFilter
    {
        private readonly RequestUserMonitoringItem _item;
        public MonitoringUserRequestFilterAttribute(RequestUserMonitoringItem item)
        {
            _item = item;
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
            _item.UserInfo = GetUser(context);
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _item.Finish = DateTime.Now;
            _item.ResponseOutput = null;
        }

        public abstract object GetUser(ActionExecutingContext context);
    }
}
