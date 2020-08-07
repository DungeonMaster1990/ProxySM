using Common.Helpers.ApiHelper;
using Common.Models.ConfigModels;
using Common.Models.VmModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ProxyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizeController : Controller
    {
        private IWebRequestHelper _helper;
        private SMApiConfigurationModel _smApiConfigModel;

        public AuthorizeController(IWebRequestHelper helper, IOptions<SMApiConfigurationModel> smApiConfigModel)
        {
            _helper = helper;
            _smApiConfigModel = smApiConfigModel.Value;
        }


        [HttpPost]
        public string Authorize(VmAuthorizeCredentials credentials)
        {
            var session = _helper.WebApiAuthorizeRequestPost<string>(_smApiConfigModel.ApiAddress, credentials);
            return session;
        }

        [HttpGet]
        public string Get(VmAuthorizeCredentials credentials)
        {
            var session = _helper.WebApiAuthorizeRequestGet<string>(_smApiConfigModel.ApiAddress, credentials);
            return session;
        }
    }
}
