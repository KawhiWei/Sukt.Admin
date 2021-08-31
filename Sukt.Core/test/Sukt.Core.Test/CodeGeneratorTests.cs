using Microsoft.Extensions.DependencyInjection;
using Sukt.CodeGenerator;
using Sukt.TestBase;
using System.Collections.Generic;
using Xunit;

namespace Sukt.Core.Test
{
    public class CodeGeneratorTests : IntegratedTest<CodeGeneratorModeule>
    {
        public CodeGeneratorTests()
        {
        }

        [Fact]
        public void CodeGenerate_Test()
        {
            ProjectMetadata projectMetadata = new ProjectMetadata();
            projectMetadata.Company = "XXXX开车公司";
            projectMetadata.SiteUrl = "http://admin.destinycore.club";
            projectMetadata.Creator = "王莫某";
            projectMetadata.Copyright = "王莫某";
            projectMetadata.Namespace = "Sukt.Core.Domain.Models";
            List<PropertyMetadata> propertyMetadatas = new List<PropertyMetadata>();
            propertyMetadatas.Add(new PropertyMetadata()
            {
                IsNullable = false,
                IsPrimaryKey = false,
                CSharpType = "string",
                DisplayName = "名字",
                PropertyName = "Name",
                IsPageDto = true,
            });
            propertyMetadatas.Add(new PropertyMetadata()
            {
                IsNullable = false,
                IsPrimaryKey = false,
                CSharpType = "string",
                DisplayName = "名字1",
                PropertyName = "Name1"
            });
            propertyMetadatas.Add(new PropertyMetadata()
            {
                IsNullable = false,
                IsPrimaryKey = false,
                CSharpType = "int",
                DisplayName = "价格",
                PropertyName = "Price",
                IsPageDto = false
            });
            projectMetadata.EntityMetadata = new EntityMetadata()
            {
                EntityName = "TestCode",
                DisplayName = "代码生成",
                PrimaryKeyType = "Guid",
                PrimaryKeyName = "Id",
                Properties = propertyMetadatas,
                IsCreation = true,
                IsModification = true,
                IsSoftDelete = false,
                AuditedUserKeyType = "Guid",
            };
            var savePath = @"E:\TestCodeGenerator";
            ICodeGenerator codeGenerator = ServiceProvider.GetService<ICodeGenerator>();
            codeGenerator.GenerateCode(projectMetadata);
        }
    }
}