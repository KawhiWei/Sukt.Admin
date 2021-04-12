using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sukt.Core.Application.Test;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    [Route("api/health")]
    [ApiController]
    [AllowAnonymous]
    public class HealthController : ControllerBase
    {
        private readonly ITestIRequest _test;

        public HealthController(ITestIRequest test)
        {
            _test = test;
        }

        /// <summary>
        /// 健康监测
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            await _test.TestIRequset("asdjlasdmlaslda");
            return Ok("ok");
        }

    }
}