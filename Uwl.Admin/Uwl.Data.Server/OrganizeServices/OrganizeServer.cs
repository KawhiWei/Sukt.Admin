using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.AutoMapper;
using Uwl.Common.LambdaTree;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.Enum;
using Uwl.Data.Model.OrganizeVO;
using Uwl.Domain.OrganizeInterface;

namespace Uwl.Data.Server.OrganizeServices
{
    /// <summary>
    /// 组织机构服务层实现
    /// </summary>
    public class OrganizeServer : IOrganizeServer
    {
        private IOrganizeRepositoty _organizeRepositoty;
        public OrganizeServer(IOrganizeRepositoty organizeRepositoty)
        {
            this._organizeRepositoty = organizeRepositoty;
        }
        public async Task<bool> AddOrganize(SysOrganize sysOrganize)
        {
            return await this._organizeRepositoty.InsertAsync(sysOrganize);
        }

        public async Task<OrganizeViewModel> GetAll()
        {
            var PoorganizeList=  await this._organizeRepositoty.GetAllListAsync(x=>x.IsDrop==false);
            var organizeVo = MyMappers.ObjectMapper.Map<List<SysOrganize>, List<OrganizeViewModel>>(PoorganizeList);
            OrganizeViewModel organizemodel = new OrganizeViewModel()
            {
                Id = Guid.Empty,
                expand = true,
                title = "根节点",
            };
            OrganizeTree(organizeVo, organizemodel);
            return organizemodel;
        }

        public async Task<SysOrganize> GetOrganize(Guid Id)
        {
            return await _organizeRepositoty.GetModelAsync(Id);
        }

        public  (List<SysOrganize>,int) GetOrganizePage(BaseQuery baseQuery)
        {
            var query = ExpressionBuilder.True<SysOrganize>();
            query = query.And(m => m.IsDrop == false);
            int Total = _organizeRepositoty.Count(query);
            var list= this._organizeRepositoty.PageBy(baseQuery.PageIndex, baseQuery.PageSize, query).ToList();
            return (list, Total);
        }

        public async Task<bool> UpdateOrganize(SysOrganize sysOrganize)
        {
            sysOrganize.UpdateDate = DateTime.Now;
            return await this._organizeRepositoty.UpdateNotQueryAsync(sysOrganize,x=>x.ParentArr, x => x.Name, x => x.ParentId, 
                x => x.Depth, x => x.Sort, x => x.OrganizeState, x => x.OrganizeType, x => x.ParentArr, 
                x => x.UpdateDate, x => x.UpdateId, x => x.UpdateName)>0;
        }
        public async Task<bool> DeleteOrganize(List<Guid> guids)
        {
            var list = await _organizeRepositoty.GetAllListAsync(x => guids.Contains(x.Id));
            list.ForEach(x =>
            {
                x.IsDrop = true;
            });
            return await _organizeRepositoty.UpdateAsync(list);
        }
        #region 组织架构树形帮助方法
        /// <summary>
        /// 创建树形
        /// </summary>
        /// <param name="AllorganizeViews"></param>
        /// <param name="viewModel"></param>
        public void OrganizeTree(List<OrganizeViewModel> AllorganizeViews, OrganizeViewModel viewModel)
        {
            var child = AllorganizeViews.Where(x => x.ParentId == viewModel.Id);
            if(child.Any())
            {
                viewModel.children.AddRange(child);
            }
            foreach (var item in child)
            {
                OrganizeTree(AllorganizeViews, item);
            }
        }
        #endregion
    }
}
