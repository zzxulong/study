﻿#region 版权说明
/**************************************************************************
 * 文 件 名：TestIntercept
 * 命名空间：CastleCoreTest.Core
 * 描　　述：
 * 版 本 号：V1.0.0
 * 作　　者：long
 * 创建时间：2019/6/3 17:49:49
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
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;

namespace CastleCoreTest.Core
{
    public class TestIntercept : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            Console.WriteLine("111111111111");
            invocation.Proceed();
            Console.WriteLine("222");
        }
    }
}
