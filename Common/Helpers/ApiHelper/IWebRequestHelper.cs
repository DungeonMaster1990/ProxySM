using System.Collections.Generic;

namespace Common.Helpers.ApiHelper
{
    public interface IWebRequestHelper
    {
        T WebApiRequestPost<T>(string url, object requestData);

        T WebApiRequestGet<T>(string url, Dictionary<string, object> data = null);
    }
}
