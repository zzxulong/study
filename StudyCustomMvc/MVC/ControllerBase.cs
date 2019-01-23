#region 版权说明
/**************************************************************************
 * 文 件 名：ControllerBase
 * 命名空间：StudyCustomMvc.MVC
 * 项　　目：$projectname$
 * 描　　述：
 * 版 本 号：V1.0.0
 * 作　　者：Administrator
 * 创建时间：2019/1/23 10:43:18
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
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;

namespace StudyCustomMvc.MVC
{
    public abstract class ControllerBase : IController
    {
        public static ICollection<Type> Types { get; set; }
        protected abstract void ExecuteCore(string action);
        public virtual void Execute(HttpContext context)
        {
            //获取应用程序根目录的虚拟路径，并通过对应用程序根目录使用波形符(~)表示法，将该路径转变为相对路径(如 "~/page.aspx" 的形式)。
            var strs = context.Request.AppRelativeCurrentExecutionFilePath.Substring(2).Split('/');
            var control = string.Empty;
            var action = string.Empty;
            if (strs.Length == 2)
            {
                control = strs[0];
                action = strs[1];
            }
            else
            {
                control = "Home";
                action = "Index";
            }
            if (Types == null)
            {
                Types = new Collection<Type>();
                var assms = BuildManager.GetReferencedAssemblies();
                foreach (Assembly assembly in assms)
                {
                    var types = assembly.GetTypes();
                    foreach (var type in types)
                    {
                        Types.Add(type);
                    }
                }
            }
            var controlType = Types.SingleOrDefault(o => o.Name == control + "Controller");
            if (controlType != null)
            {
                var baseControl = Activator.CreateInstance(controlType) as Controller;
                if (baseControl != null) baseControl.ExecuteCore(action);
            }
        }
    }
}