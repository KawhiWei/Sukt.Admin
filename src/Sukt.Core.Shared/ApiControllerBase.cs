using Microsoft.AspNetCore.Mvc;
using System;

namespace Sukt.Core.Shared
{
    [Route("admin/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase
    {
    }
}
