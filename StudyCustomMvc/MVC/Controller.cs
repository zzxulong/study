#region 版权说明
/**************************************************************************
 * 文 件 名：Controller
 * 命名空间：StudyCustomMvc.MVC
 * 项　　目：$projectname$
 * 描　　述：
 * 版 本 号：V1.0.0
 * 作　　者：Administrator
 * 创建时间：2019/1/23 10:50:28
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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyCustomMvc.MVC
{
    public class Controller : ControllerBase
    {
        public IActionInvoker ActionInvoker { get; set; }
        public Type Type { get; set; }
        public Controller()
        {
            ActionInvoker = new ControllerActionInvoker();
            Type = this.GetType();
        }
        protected override void ExecuteCore(string action)
        {
            ActionInvoker.InvokeAction(Type, action);
        }

        public ContentResult Content(string msg)
        {
            return new ContentResult() { Data = msg };
        }
    }
}