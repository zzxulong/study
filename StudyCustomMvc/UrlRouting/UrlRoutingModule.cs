#region 版权说明
/**************************************************************************
 * 文 件 名：UrlRoutingModule
 * 命名空间：StudyCustomMvc.UrlRouting
 * 项　　目：$projectname$
 * 描　　述：
 * 版 本 号：V1.0.0
 * 作　　者：Administrator
 * 创建时间：2019/1/23 10:55:22
 * CLR 版本：4.0.30319.42000
 * 机器名称：DESKTOP-PHQQ0O3
***************************************************************************	
 * 修 改 人：
 * 时    间：
 * 修改说明：
***************************************************************************
 * Copyright  2018 河南广慧会计服务有限公司 Inc. All Rights Reserved
***************************************************************************/
#endregion
using StudyCustomMvc.UrlRouting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyCustomMvc
{
    public class UrlRoutingModule : IHttpModule
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Init(HttpApplication context)
        {
            context.PostResolveRequestCache += context_PostResolveRequestCache;
            //throw new NotImplementedException();
        }

        void context_PostResolveRequestCache(object sender, EventArgs e)
        {
            var httpContext = HttpContext.Current;
            var routeData = RouteTable.Routes.GetRouteData(httpContext);
            if (null == routeData)
                return;
            var requestContext = new RequestContext { RouteData = routeData, HttpContext = httpContext };
            var handler = routeData.RouteHandler.GetHttpHandler(requestContext);
            httpContext.RemapHandler(handler);
        }
    }
    /// <summary>
    /// 包装请求上下文
    /// </summary>
    public class RequestContext
    {
        public RouteData RouteData { get; set; }
        public HttpContext HttpContext { get; set; }
    }
}