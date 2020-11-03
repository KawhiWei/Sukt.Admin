using System.Web;

namespace Sukt.Core.CodeGenerator
{
    public class CodeData
    {
        private string _sourceCode;

        /// <summary>
        /// 获取或设置 源代码字符串
        /// </summary>
        public string SourceCode
        {
            get => _sourceCode;
            set => _sourceCode = HttpUtility.HtmlDecode(value);
        }

        /// <summary>
        /// 获取或设置 代码存储路径
        /// </summary>
        public string FileName { get; set; }
    }
}