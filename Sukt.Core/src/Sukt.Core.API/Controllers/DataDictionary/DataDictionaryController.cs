using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sukt.Core.Application;
using Sukt.Core.AspNetCore.ApiBase;
using Sukt.Core.Dtos.DataDictionaryDto;
using Sukt.Core.Shared.AjaxResult;
using Sukt.Core.Shared.Audit;
using Sukt.Core.Shared.Entity;
using Sukt.Core.Shared.Extensions;
using Sukt.Core.Shared.OperationResult;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Sukt.Core.API.Controllers.DataDictionary
{
    [Description("数据字典管理")]
    public class DataDictionaryController : ApiControllerBase
    {
        private readonly IDictionaryContract _dictionary = null;
        private readonly ILogger<DataDictionaryController> _logger = null;

        public DataDictionaryController(IDictionaryContract dictionary, ILogger<DataDictionaryController> logger)
        {
            _dictionary = dictionary;
            _logger = logger;
        }

        /// <summary>
        /// 添加一个数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("创建一个数据字典")]
        [AuditLog]
        public async Task<AjaxResult> CreateAsync(DataDictionaryInputDto input)
        {
            return (await _dictionary.InsertAsync(input)).ToAjaxResult();
        }

        /// <summary>
        /// 修改一个数据字典
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Description("修改一个数据字典")]
        [AuditLog]
        public async Task<AjaxResult> UpdateAsync(DataDictionaryInputDto input)
        {
            return (await _dictionary.UpdateAsync(input)).ToAjaxResult();
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        [Description("分页获取数据字典")]
        public async Task<PageList<DataDictionaryOutDto>> GetPageAsync([FromBody] PageRequest query)
        {
            return (await _dictionary.GetResultAsync(query)).PageList();
        }

        /// <summary>
        /// 删除一个数据字典
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Description("异步删除数据字典")]
        [HttpDelete]
        [AuditLog]
        public async Task<AjaxResult> DeleteAsyc(Guid? id)
        {
            return (await _dictionary.DeleteAsync(id.Value)).ToAjaxResult();
        }
    }
}
