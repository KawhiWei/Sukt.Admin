using System;
using System.Collections.Generic;
using System.Text;
using Uwl.Common.AutoMapper;
using Uwl.Data.Model.BaseModel;
using Uwl.Data.Model.MenuViewModel;
using Uwl.Domain.MenuInterface;
using System.Linq;
using Uwl.Domain.ButtonInterface;
using Uwl.Domain.RoleInterface;
using System.Threading.Tasks;
using Uwl.Extends.Utility;
using Uwl.Data.Model.RoleAssigVO;
using Newtonsoft.Json;
using Uwl.Domain.IRepositories;
using Uwl.Common.Cache.RedisCache;
using Microsoft.EntityFrameworkCore;
using Uwl.Common.Helper;
using Uwl.Common.LogsMethod;
using Uwl.Common.SendEmail;

namespace Uwl.Data.Server.RoleAssigServices
{
    /// <summary>
    /// 角色权限分配服务层
    /// </summary>
    public class SysRoleAssigServer : IRoleAssigServer
    {
        private IMenuRepositoty _menuRepositoty;
        private IButtonRepositoty _buttonRepositoty;
        private IRoleRightAssigRepository _roleRightAssigRepository;//定义角色权限领域层对象
        private IRoleRepositoty _roleRepositoty;
        public  IUnitofWork _unitofWork;
        private readonly IRedisCacheManager _redisCacheManager;
        public SysRoleAssigServer(IMenuRepositoty menuRepositoty, 
            IButtonRepositoty buttonRepositoty, 
            IRoleRightAssigRepository roleRightAssigRepository, IRoleRepositoty roleRepositoty, 
            IUnitofWork unitofWork, IRedisCacheManager redisCacheManager
            )
        {
            this._menuRepositoty = menuRepositoty;
            this._buttonRepositoty = buttonRepositoty;
            this._roleRightAssigRepository = roleRightAssigRepository;
            this._roleRepositoty = roleRepositoty;
            this._roleRightAssigRepository = roleRightAssigRepository;
            this._redisCacheManager = redisCacheManager;
            this._unitofWork = unitofWork;
        }
        public async Task<RoleAssigMenuViewModel> GetRoleAssigMenuViewModels(Guid RoleId)
        {
            #region 获取所有的菜单和按钮
            var list = this._menuRepositoty.GetAll(x => x.IsDrop == false).ToList();//获取所有的菜单列表
            var RoleAssigMenu = MyMappers.ObjectMapper.Map<List<SysMenu>, List<RoleAssigMenuViewModel>>(list);
            var buttonlist = _buttonRepositoty.GetAll(x => x.IsDrop == false);//获取所有的按钮列表
            //var RoleAssigButton = MyMappers.ObjectMapper.Map<List<SysButton>, List<RoleAssigButtonViewModel>>(buttonlist);
            RoleAssigMenuViewModel roleAssigMenuView = new RoleAssigMenuViewModel()
            {
                Id = Guid.Empty,
                expand = true,
                title = "根节点",
                disabled = false,
                @checked = false,
            };
            var SysbuttonList = await _buttonRepositoty.GetAllListAsync(x => x.IsDrop == false);//获取所有的菜单按钮列表
            #endregion


            #region 获取该角色下的所有权限
            var roleRight = await _roleRightAssigRepository.GetAllListAsync(x => x.RoleId == RoleId);

            #endregion

            //调用递归事件
            CreateRoleAssigTree(RoleAssigMenu, roleAssigMenuView, SysbuttonList, roleRight);

            //dynamic var =  LinqTosql();

            return roleAssigMenuView;
        }
        #region 权限树形帮助方法        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AllroleAssig">所有菜单</param>
        /// <param name="roleAssigMenuView">根节点</param>
        /// <param name="sysMenuButtonList">菜单按钮列表数据</param>
        /// <param name="roleAssigButtonViews">按钮列表</param>
        /// /// <param name="sysRoleRights">已拥有的权限对象</param>
        public void CreateRoleAssigTree(List<RoleAssigMenuViewModel> AllroleAssig,
                                        RoleAssigMenuViewModel roleAssigMenuView,
                                        List<SysButton> sysButtonList,
                                        List<SysRoleRight> sysRoleRights
        )
        {
            var child = AllroleAssig.FindAll(m => m.ParentId == roleAssigMenuView.Id);
            if (child.Any())//判断是否存在子级
            {
                roleAssigMenuView.children.AddRange(child);
            }
            foreach (var item in child)
            {
                var buttonlist = sysButtonList.Where(x => x.MenuId == item.Id).Select(x => x.Id).ToList();//获取到此菜单下的所有按钮Id
                #region //根据菜单ID判断角色权限内是否存在该菜单的权限(如果存在证明已有改权限)
                var model = sysRoleRights.Where(x => x.MenuId == item.Id).FirstOrDefault();
                if (model != null)
                {
                    item.@checked = true;
                }
                if (buttonlist.Any())
                {
                    var button = sysButtonList.Where(x =>x.MenuId == item.Id).ToList();//拿到该菜单下面的所有按钮
                    var nButton = new List<RoleAssigButtonViewModel>();
                    //判断是否存在角色权限表中
                    if (model != null)
                    {
                        foreach (var btnitem in button)
                        {
                            var buttons = model.ButtonIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.ToGuid()).ToList();
                            var btn = buttons.Where(x => x == btnitem.Id).FirstOrDefault();
                            if(btn!=Guid.Empty)
                            {
                                nButton.Add(new RoleAssigButtonViewModel{Id = btnitem.Id,@checked = true,lable = btnitem.Name});
                            }
                            else
                            {
                                nButton.Add(new RoleAssigButtonViewModel{Id = btnitem.Id,@checked = false,lable = btnitem.Name});
                            }
                        }
                    }
                    else
                    {
                        //如果不存在将所有的按钮设置为未选择状态
                        button.ForEach(x =>
                        {
                            nButton.Add(new RoleAssigButtonViewModel
                            {
                                Id = x.Id,
                                @checked = false,
                                lable = x.Name
                            });
                        });
                    }
                    item.ButtonsList.AddRange(nButton);

                }
                #endregion

                CreateRoleAssigTree(AllroleAssig, item, sysButtonList, sysRoleRights);
            }
        }
        #endregion
        /// <summary>
        /// 保存权限方法
        /// </summary>
        /// <param name="saveRoleAssigView"></param>
        /// <returns></returns>
        public async Task<bool> SaveRoleAssig(SaveRoleAssigViewModel saveRoleAssigView)
        {
            List<Guid> BtnIdlist = new List<Guid>(); 
            BtnIdlist.AddRange(JsonConvert.DeserializeObject<List<Guid>>(saveRoleAssigView.BtnIds));//获取选中的按钮ID
            List<RoleAssigMenuViewModel> roleAssigMenuViews = new List<RoleAssigMenuViewModel>();
            roleAssigMenuViews.AddRange(JsonConvert.DeserializeObject<List<RoleAssigMenuViewModel>>(saveRoleAssigView.menuIds));//获取选中的按钮ID
            try
            {
                var sysButtonlist= await _buttonRepositoty.GetAllListAsync(x => BtnIdlist.Contains(x.Id));//根据按钮ID获取所有的按钮及对应的菜单
                var DeleteroleRight = await _roleRightAssigRepository.GetAllListAsync(x => x.RoleId == saveRoleAssigView.RoleId);
                List<SysRoleRight> SysRoleRightList = new List<SysRoleRight>();
                var menuIds = sysButtonlist.Select(x => new { x.MenuId }).Distinct().ToList();//根据按钮ID获取所有的按钮及对应的菜单,并且根据菜单ID去重
                foreach (var item in menuIds)//将去重的菜单ID添加到角色权限List容器中
                {
                    SysRoleRightList.Add(new SysRoleRight {
                        MenuId = item.MenuId,
                        RoleId= saveRoleAssigView.RoleId,
                        CreatedId= saveRoleAssigView.CreatedId,
                        CreatedName=saveRoleAssigView.CreatedName,
                    });
                }
                var ReadMenulist = SysRoleRightList.ToSelectList(x => x.MenuId);
                var MenuList= roleAssigMenuViews.Where(x => !ReadMenulist.Contains(x.Id)).ToList();//获取到还未选中的菜单
                SysRoleRightList.AddRange(MenuList.Select(x => new SysRoleRight  //将未选中的菜单添加到角色权限对象中
                {
                    MenuId=x.Id,
                    RoleId = saveRoleAssigView.RoleId,
                    CreatedId = saveRoleAssigView.CreatedId,
                    CreatedName = saveRoleAssigView.CreatedName,
                }).ToList());
                SysRoleRightList.ForEach(item =>//根据菜单ID获取菜单下面所有的按钮
                {
                    var BtnList= sysButtonlist.Where(x => x.MenuId == item.MenuId).ToList();
                    item.ButtonIds=string.Join(",", BtnList.Select(x => x.Id));
                });

                _unitofWork.BeginTransaction();
                await _roleRightAssigRepository.Delete(DeleteroleRight);
                await    _roleRightAssigRepository.InsertAsync(SysRoleRightList);
                _unitofWork.Commit();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Commit 异常：{ex.InnerException}/r{ ex.Message}");
            }
        }


        /// <summary>
        /// 在自定义策略处理器中调用方法
        /// </summary>
        /// <param name="roleArr"></param>
        /// <returns></returns>
        public async Task<List<RoleActionModel>> GetRoleAction(Guid [] roleArr)
        {
            try
            {
                var RoleModuleList = new List<RoleActionModel>();
                var Rolelist=await _roleRepositoty.GetAllListAsync(x => roleArr.Contains(x.Id));//根据Httpcontext存储的角色名称获取角色ID
                var RoleAssig =await  _roleRightAssigRepository.GetAllListAsync(x => Rolelist.Select(s=>s.Id).Contains(x.RoleId));//根据角色ID获取到所有的权限
                var Btnlist = await _buttonRepositoty.GetAllListAsync(x=>x.IsDrop==false);//获取所有的按钮
                List<SysMenu> Menulist = new List<SysMenu>();
                if (await _redisCacheManager.Get(Appsettings.app(new string[] { "CacheOptions", "Menukey" })))//判断菜单缓存是否存在，如果存在则取缓存不存在则取数据库
                {
                    Menulist = await _redisCacheManager.GetList<SysMenu>(Appsettings.app(new string[] { "CacheOptions", "Menukey" }));//.Where(x=>MenuIds.Contains(x.Id)).ToList();
                }
                else
                {
                    Menulist = await this._menuRepositoty.GetAll(x => x.IsDrop == false).AsNoTracking().ToListAsync();
                    await _redisCacheManager.Set(Appsettings.app(new string[] { "CacheOptions", "Menukey" }), Menulist);
                }
                if (!Menulist.Any())
                {
                    Menulist = await _menuRepositoty.GetAllListAsync(x => x.IsDrop == false);//根据菜单ID获取菜单列表 x=>MenuIds.Contains(x.Id)
                }
                foreach (var item in RoleAssig)
                {
                    var RoleModel = Rolelist.Where(x => x.Id == item.RoleId).FirstOrDefault();//获取角色实体
                    var MenuModel=Menulist.Where(x => x.Id == item.MenuId).FirstOrDefault();//获取菜单实体
                    RoleModuleList.Add(new RoleActionModel {RoleName=RoleModel.Id,ActionName=MenuModel.APIAddress });
                    if (!item.ButtonIds.IsNullOrEmpty()) //判断是否存在按钮
                    {
                        List<Guid> guids = new List<Guid>();
                        var btnArr = item.ButtonIds.Split(',').Select(x=>x.ToGuid()).ToList();
                        var RoleBtn=  Btnlist.Where(x => btnArr.Contains(x.Id)).ToList();
                        RoleModuleList.AddRange(RoleBtn.Select(x => new RoleActionModel
                        {
                            RoleName = RoleModel.Id,//在这里代表的是
                            ActionName=x.APIAddress,
                        }));
                    }
                }
                return RoleModuleList;
            }
            catch (Exception ex)
            {
                var FromMailAddres = Appsettings.app(new string[] { "FromMailConfig", "FromMailAddres" });
                var FromMailPwd = Appsettings.app(new string[] { "FromMailConfig", "FromMailPwd" });
                var ToMail = Appsettings.app(new string[] { "FromMailConfig", "ToMail" });
                await SendEmail.SendMailAvailableAsync(FromMailAddres, FromMailPwd, ToMail, $"{ DateTime.Now.ToString("yyyy-MM-dd")}Redis超出限制错误", "Redis链接错误");
                LogServer.WriteErrorLog($"{ DateTime.Now.ToString("yyyy-MM-dd hh:mm:dd")}Redis超出限制错误", $"Redis链接错误", ex);
                throw ex;
            }
        }
        /// <summary>
        /// Linq多表链接查询
        /// </summary>
        /// <returns></returns>
        //public dynamic LinqTosql()
        //{
        //    var query = (from a in _menuRepositoty.GetAll(x => x.IsDrop == false)
        //                join b in _buttonRepositoty.GetAll() on a.Id equals b.MenuId
        //                select new RoleActionModel
        //                {
        //                    RoleName=a.Name
        //                }).ToList();
        //    return query;
        //}


    }
}
