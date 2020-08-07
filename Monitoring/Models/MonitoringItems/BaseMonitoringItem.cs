﻿using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Monitoring.Models.MonitoringItems
{
    public abstract class BaseMonitoringItem: IMonitoringItem
    {
        public string Name { get; }

        public JObject GetJObject()
        {
            return JObject.FromObject(this);
        }
    }
}
