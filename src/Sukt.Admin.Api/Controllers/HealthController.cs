using Microsoft.AspNetCore.Mvc;

namespace Sukt.Admin.Api.Controllers
{
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [Route("/api/healthchecks/liveness")]
        public IActionResult Liveness()
        {
            return Ok();
        }
        [HttpGet]
        [Route("/api/healthchecks/readiness")]
        public IActionResult Readiness()
        {
            return Ok();
        }
    }
}
