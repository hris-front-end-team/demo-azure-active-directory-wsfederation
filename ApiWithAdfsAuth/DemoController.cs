using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ApiWithAdfsAuth
{
    [Route("demo")]
    public sealed class DemoController : Controller
    {
        private string CurrentUTC
        {
            get
            {
                var now = DateTime.UtcNow;
                return now.ToString("ddd, dd MMM yyyy HH:mm:ss") + " (UTC ~ GMT)";
            }
        }


        [HttpGet("open-date-time")]
        public Task<string> OpenDateTime()
        {
            return Task.FromResult($"OPEN-DATE-TIME { CurrentUTC }");
        }

        [Authorize]
        [HttpGet("authenticated-date-time")]
        public Task<string> AuthenticatedDateTime()
        {
            return Task.FromResult($"AUTHENTICATED-DATE-TIME { CurrentUTC }");
        }
    }

}
