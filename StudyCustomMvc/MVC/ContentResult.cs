#region 版权说明
/**************************************************************************
 * 文 件 名：ContentResult
 * 命名空间：StudyCustomMvc.MVC
 * 项　　目：$projectname$
 * 描　　述：
 * 版 本 号：V1.0.0
 * 作　　者：Administrator
 * 创建时间：2019/1/23 10:52:50
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
    public class ContentResult:ActionResult
    {
        public string Data { get; set; }
        public override void ExecuteResult()
        {
            HttpContext.Current.Response.Write(Data);
        }
    }
}