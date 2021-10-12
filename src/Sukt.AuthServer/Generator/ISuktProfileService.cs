using Sukt.AuthServer.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sukt.AuthServer.Generator
{
    public interface ISuktProfileService
    {
        Task GetProfileDataAsync(SuktProfileDataRequestContext context);
    }
}
