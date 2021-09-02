using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sukt.Core.Application.Test;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers
{
    [Route("api/healthchecks")]
    //[AllowAnonymous]
    public class HealthController : ControllerBase
    {
        private readonly ITestIRequest _test;
        private readonly ILogger<HealthController> _logger;

        public HealthController(ITestIRequest test, ILogger<HealthController> logger)
        {
            _test = test;
            _logger = logger;
        }

        /// <summary>
        /// 健康监测通过liveness来探测微服务的存活性
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("liveness")]
        public IActionResult GetLiveness()
        {
            //await _test.TestIRequset("asdjlasdmlaslda");
            //_logger.LogError("健康探针{liveness}");
            return Ok("ok");
        }
        /// <summary>
        /// 健康监测通过readiness来探测微服务
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("readiness")]
        public IActionResult GetReadiness()
        {
            //await _test.TestIRequset("asdjlasdmlaslda");
            //_logger.LogError("健康探针{readiness}");
            return Ok("ok");
        }

    }
}