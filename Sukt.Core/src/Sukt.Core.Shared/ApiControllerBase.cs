using Microsoft.AspNetCore.Mvc;
using System;

namespace Sukt.Core.Shared
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class ApiControllerBase
    {
    }
}
