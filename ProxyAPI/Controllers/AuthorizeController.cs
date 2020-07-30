using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers.ApiHelper;
using Common.Models.ConfigModels;
using Common.Models.VmModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Ini;
using Microsoft.Extensions.Options;

namespace ProxyAPI.Controllers
{
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
    }
}
