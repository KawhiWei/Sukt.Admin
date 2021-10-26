using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Sukt.Core.Shared
{
    [Route("admin/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public abstract class ApiControllerBase
    {
    }
}
