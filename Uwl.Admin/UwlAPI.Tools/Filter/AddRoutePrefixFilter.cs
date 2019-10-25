using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UwlAPI.Tools.Filter
{
    /// <summary>
    /// 全局路由公约前缀设置
    /// </summary>
    public class AddRoutePrefixFilter : IApplicationModelConvention
    {
        private readonly AttributeRouteModel _attributeRouteModel;
        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="routeTemplateProvider"></param>
        public AddRoutePrefixFilter(IRouteTemplateProvider routeTemplateProvider)
        {
            _attributeRouteModel = new AttributeRouteModel(routeTemplateProvider);
        }
        /// <summary>
        /// 路由前缀实现方法
        /// </summary>
        /// <param name="application"></param>
        public void Apply(ApplicationModel application)
        {
            //遍历所有的控制器
            foreach (var controller in application.Controllers)
            {
                // 已经标记了 RouteAttribute 的 Controller
                var matchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel != null).ToList();
                if (matchedSelectors.Any())
                {
                    foreach (var selectorModel in matchedSelectors)
                    {
                        // 在 当前路由上 再 添加一个 路由前缀
                        selectorModel.AttributeRouteModel = AttributeRouteModel.CombineAttributeRouteModel(_attributeRouteModel,
                            selectorModel.AttributeRouteModel);
                    }
                }

                // 没有标记 RouteAttribute 的 Controller
                var unmatchedSelectors = controller.Selectors.Where(x => x.AttributeRouteModel == null).ToList();
                if (unmatchedSelectors.Any())
                {
                    foreach (var selectorModel in unmatchedSelectors)
                    {
                        // 添加一个 路由前缀
                        selectorModel.AttributeRouteModel = _attributeRouteModel;
                    }
                }
            }
        }
    }
}
