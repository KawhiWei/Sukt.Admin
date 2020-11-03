using Microsoft.Extensions.DependencyInjection;
using Sukt.Core.Shared.Modules;

namespace Sukt.Core.CodeGenerator
{
    public class CodeGeneratorModeule : SuktAppModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddSingleton<ICodeGenerator, RazorCodeGenerator>();
        }
    }
}