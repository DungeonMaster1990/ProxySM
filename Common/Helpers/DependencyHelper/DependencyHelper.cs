using System;
using System.Collections.Generic;
using Common.Helpers.ApiHelper;

namespace Common.Helpers.DependencyHelper
{
    public static class DependencyHelper
    {
        public static Dictionary<Type, Type> GetCommonDependencies()
        {
            var dependencies = new Dictionary<Type, Type>
            {
                { typeof(IWebRequestHelper), typeof(WebRequestHelper)},
            };

            return dependencies;
        }
    }
}
