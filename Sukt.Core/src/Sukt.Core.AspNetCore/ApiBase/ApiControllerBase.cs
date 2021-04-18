using Microsoft.AspNetCore.Mvc;

namespace Sukt.Core.AspNetCore.ApiBase
{
    [Route("admin/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}