using System;
using System.Collections.Generic;
using System.Text;

namespace Monitoring.Core
{
    public class MonitoringEngine
    {
        public void Execute(Action action)
        {
            var attr = action.Method.GetCustomAttributes(typeof(BaseMonitoringAttribute), true).First() as BaseMonitoringAttribute;
            var method1 = action.Target.GetType().GetMethod(attr.PreAction);
            var method2 = action.Target.GetType().GetMethod(attr.PostAction);

            // now first invoke the pre-action method
            method1.Invoke(null, null);
            // the actual action
            action();
            // the post-action
            method2.Invoke(null, null);
        }
    }
    public class BaseMonitoringAttribute : Attribute
    {
        public string PreAction;
        public string PostAction;
    }
}
