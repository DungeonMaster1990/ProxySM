using System;
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

        public AuthorizeController (IWebRequestHelper helper, IOptions<SMApiConfigurationModel> smApiConfigModel)
        {
            _helper = helper;
            _smApiConfigModel = smApiConfigModel.Value;
        }


        [HttpPost]
        public IActionResult Authorize(VmAuthorizeCredentials credentials)
        {
            throw new NotImplementedException();
            //_helper.Authorize(credentials);
        }

        [HttpGet]
        public IActionResult Get()
        {
            throw new NotImplementedException();
            //_helper.Authorize(credentials);
        }
    }
}
