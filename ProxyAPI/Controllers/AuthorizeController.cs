using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.Helpers.ApiHelper;
using Common.Models.VmModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProxyAPI.Controllers
{
    public class AuthorizeController : Controller
    {
        private IWebRequestHelper _helper;
        
        public AuthorizeController (IWebRequestHelper helper)
        {
            _helper = helper;
        }


        [HttpPost]
        public IActionResult Authorize(VmAuthorizeCredentials credentials)
        {
            throw new NotImplementedException();
        }
    }
}
