using Common.Models.VmModels;
using System.Collections.Generic;

namespace Common.Helpers.ApiHelper
{
    public interface IWebRequestHelper
    {
        T WebApiRequestPost<T>(string url, object requestData);

        T WebApiRequestGet<T>(string url, Dictionary<string, object> data = null);

        T WebApiAuthorizeRequestPost<T>(string url, VmAuthorizeCredentials credentials);

        T WebApiAuthorizeRequestGet<T>(string url, VmAuthorizeCredentials credentials);
    }
}
