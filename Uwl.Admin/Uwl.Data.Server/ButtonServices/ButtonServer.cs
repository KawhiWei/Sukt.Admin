using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Uwl.Common.LambdaTree;
using Uwl.Data.Model.Assist;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.VO.ButtonVO;
using Uwl.Domain.ButtonInterface;
using Uwl.Domain.MenuInterface;
using Uwl.Domain.RoleInterface;
using Uwl.Extends.Utility;

namespace Uwl.Data.Server.ButtonServices
{
    /// <summary>
    /// 服务层按钮管理接口实现
    /// </summary>
    public class ButtonServer:IButtonServer
    {
        private readonly IButtonRepositoty _buttonRepositoty;
        private readonly IMenuRepositoty _menuRepositoty;
        private readonly IRoleRightAssigRepository _roleRightAssigRepository;//定义角色权限领域层对象
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="menuRepositoty"></param>
        /// <param name="buttonRepositoty"></param>
        public ButtonServer(IMenuRepositoty menuRepositoty, IButtonRepositoty buttonRepositoty, IRoleRightAssigRepository roleRightAssigRepository)
        {
            this._buttonRepositoty = buttonRepositoty;
            this._menuRepositoty = menuRepositoty;
            this._roleRightAssigRepository = roleRightAssigRepository;
        }
        /// <summary>
        /// 添加按钮
        /// </summary>
        /// <param name="sysButton"></param>
        /// <returns></returns>
        public async Task<bool> AddButton(SysButton sysButton)
        {
            return await _buttonRepositoty.InsertAsync(sysButton);
        }
        /// <summary>
        /// 修改按钮
        /// </summary>
        /// <param name="sysButton"></param>
        /// <returns></returns>
        public async Task<bool> UpdateButton(SysButton sysButton)
        {
            sysButton.UpdateDate = DateTime.Now;
            return await _buttonRepositoty.UpdateNotQueryAsync(sysButton,
                x=>x.Name, x => x.Memo, x => x.KeyCode, x => x.APIAddress,x => x.ButtonStyle, x => x.MenuId, x => x.IsShow,
                 x => x.UpdateDate, x => x.UpdateId, x => x.UpdateName,
                x => x.Sort) >0;
        }
        public async Task<bool> DeleteButton(List<Guid> guids)
        {

            var list = GetAllListById(guids);
            list.ForEach(x=>
            {
                x.IsDrop = true;
            });
            return await _buttonRepositoty.UpdateAsync(list);
        }
        /// <summary>
        /// 分页获取按钮列表
        /// </summary>
        /// <param name="baseQuery"></param>
        /// <param name="Total"></param>
        /// <returns></returns>
        public (List<ButtonViewMoel>,int) GetQueryByPage(ButtonQuery buttonQuery)
        {
            var query = ExpressionBuilder.True<SysButton>();
            query = query.And(menu => menu.IsDrop == false);
            var menuquery = ExpressionBuilder.True<SysMenu>();
            menuquery = menuquery.And(menu => menu.IsDrop == false);
            if (!buttonQuery.Name.IsNullOrEmpty())
            {
                query = query.And(m => m.Name.Contains(buttonQuery.Name.Trim()));
            }
            if (!buttonQuery.JSKeyCode.IsNullOrEmpty())
            {
                query = query.And(m => m.KeyCode.Contains(buttonQuery.JSKeyCode.Trim()));
            }
            if (!buttonQuery.APIAddress.IsNullOrEmpty())
            {
                query = query.And(m => m.APIAddress.Contains(buttonQuery.APIAddress.Trim()));
            }
            if(!buttonQuery.MenuName.IsNullOrEmpty())
            {
                 menuquery= menuquery.And(m => m.Name.Contains(buttonQuery.MenuName.Trim()));
            }
            var list = (from a in _buttonRepositoty.GetAll(query)
                        join  b in _menuRepositoty.GetAll(menuquery) on a.MenuId equals b.Id
                        select new ButtonViewMoel
                        {
                            Id=a.Id,
                            Name = a.Name,
                            APIAddress=a.APIAddress,
                            KeyCode=a.KeyCode,
                            Memo=a.Memo,
                            ButtonStyle=a.ButtonStyle,
                            IsShow=a.IsShow,
                            CreatedDate=a.CreatedDate,
                            MenuId=b.Id,
                            MenuName = b.Name,
                        });
            int Total = list.Count();//查询符合添加的总数执行一次
            return (list.PageBy(buttonQuery.PageSize, buttonQuery.PageIndex-1).ToList(), Total);//再查询符合条件的数据在查一次
        }
        /// <summary>
        /// 获取所有的按钮列表
        /// </summary>
        /// <returns></returns>
        public List<SysButton> GetButtonAllList()
        {
            return _buttonRepositoty.GetAll().ToList();
        }
        /// <summary>
        /// 根据ID获取所有的按钮列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public List<SysButton> GetAllListById(List<Guid> guids)
        {
            return _buttonRepositoty.GetAll(x => guids.Contains(x.Id)).ToList();
        }
    }
}
