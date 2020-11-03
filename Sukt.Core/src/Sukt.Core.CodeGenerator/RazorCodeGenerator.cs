using RazorEngine;
using RazorEngine.Templating;
using Sukt.Core.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Sukt.Core.CodeGenerator
{
    /// <summary>
    /// Razor引擎生成器
    /// </summary>
    public class RazorCodeGenerator : ICodeGenerator
    {
        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="projectMetadata"></param>
        /// <param name="filePath"></param>
        public void GenerateCode(ProjectMetadata projectMetadata, string filePath)
        {
            List<CodeData> codes = new List<CodeData>();

            codes.Add(GenerateEntityCode(projectMetadata));
            codes.Add(GenerateEntityConfigurationCode(projectMetadata));
            //codes.Add(GenerateInputDtoCode(projectMetadata));
            //codes.Add(GenerateOutputDtoCode(projectMetadata));
            //codes.Add(GeneratePageDtoCode(projectMetadata));
            foreach (var code in codes.OrderBy(o => o.FileName))
            {
                var saveFilePath = $"{Path.Combine(@"{0}\{1}", filePath, code.FileName)}";
                var path = Path.GetDirectoryName(saveFilePath);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (var fs = new FileStream(saveFilePath, FileMode.Create, FileAccess.Write))
                {
                    using (var sw = new StreamWriter(fs))
                    {
                        sw.Write(code.SourceCode);
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
            }
        }

        /// <summary>
        /// 得到模版
        /// </summary>
        /// <param name="metadata"></param>
        /// <param name="codeType"></param>
        /// <returns></returns>
        private string GetTemplateCode(ProjectMetadata metadata, CodeType codeType)
        {
            string template = GetInternalTemplate(codeType);
            var key = GetKey(codeType, template);
            return Engine.Razor.RunCompile(template, key, metadata.GetType(), metadata);
        }

        /// <summary>
        /// 创建键
        /// </summary>
        /// <param name="codeType"></param>
        /// <param name="template"></param>
        /// <returns></returns>

        private ITemplateKey GetKey(CodeType codeType, string template)
        {
            string name = $"{codeType.ToString()}-{Guid.NewGuid()}";
            return Engine.Razor.GetKey(name);
        }

        /// <summary>
        /// 生成实体代码
        /// </summary>
        /// <param name="metadata">元数据</param>
        /// <returns></returns>

        public CodeData GenerateEntityCode(ProjectMetadata metadata)
        {
            var template = GetTemplateCode(metadata, CodeType.Entity);
            var code = new CodeData()
            {
                SourceCode = template,
                FileName = $"Entity/{metadata.EntityMetadata.EntityName}.cs"
            };
            return code;
        }

        /// <summary>
        /// 生成实体配置代码
        /// </summary>
        /// <param name="metadata">元数据</param>
        /// <returns></returns>
        public CodeData GenerateEntityConfigurationCode(ProjectMetadata metadata)
        {
            var template = GetTemplateCode(metadata, CodeType.EntityConfiguration);
            var code = new CodeData()
            {
                SourceCode = template,
                FileName = $"EntityConfigurations/{metadata.EntityMetadata.EntityName}Configuration.cs"
            };
            return code;
        }

        /// <summary>
        /// 创建输入代码
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public CodeData GenerateInputDtoCode(ProjectMetadata metadata)
        {
            var template = GetTemplateCode(metadata, CodeType.InputDto);
            var code = new CodeData()
            {
                SourceCode = template,
                FileName = $"Dtos/{metadata.EntityMetadata.EntityName}InputDto.cs"
            };
            return code;
        }

        /// <summary>
        /// 创建输出代码
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public CodeData GenerateOutputDtoCode(ProjectMetadata metadata)
        {
            var template = GetTemplateCode(metadata, CodeType.OutputDto);
            var code = new CodeData()
            {
                SourceCode = template,
                FileName = $"Dtos/{metadata.EntityMetadata.EntityName}OutputDto.cs"
            };
            return code;
        }

        /// <summary>
        /// 创建分页Dto代码
        /// </summary>
        /// <param name="metadata"></param>
        /// <returns></returns>
        public CodeData GeneratePageDtoCode(ProjectMetadata metadata)
        {
            var template = GetTemplateCode(metadata, CodeType.PageListDto);
            var code = new CodeData()
            {
                SourceCode = template,
                FileName = $"Dtos/{metadata.EntityMetadata.EntityName}PageListDto.cs"
            };
            return code;
        }

        /// <summary>
        /// 读取指定代码类型的内置代码模板
        /// </summary>
        /// <param name="type">代码类型</param>
        /// <returns></returns>
        private string GetInternalTemplate(CodeType type)
        {
            string projectName = Assembly.GetExecutingAssembly().GetName().Name.ToString();
            string resName = $"{projectName}.Templates.{type.ToString()}.cshtml";
            Stream stream = GetType().Assembly.GetManifestResourceStream(resName);
            if (stream == null)
            {
                throw new SuktAppException("没有找到对应的模板");
            }
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public enum CodeType
    {
        /// <summary>
        /// 实体类
        /// </summary>
        Entity,

        /// <summary>
        /// 实体配置
        /// </summary>
        EntityConfiguration,

        /// <summary>
        /// 输出Dto
        /// </summary>
        OutputDto,

        /// <summary>
        /// 输入Dto
        /// </summary>
        InputDto,

        /// <summary>
        /// 分页Dto
        /// </summary>
        PageListDto,
    }
}