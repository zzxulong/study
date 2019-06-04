#region 版权说明
/**************************************************************************
 * 文 件 名：Interceptor
 * 命名空间：CastleCoreTest.Core
 * 描　　述：
 * 版 本 号：V1.0.0
 * 作　　者：long
 * 创建时间：2019/6/4 9:40:04
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
    public class Interceptor : StandardInterceptor
    {
        /// <summary>
        /// 调用前的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PreProceed(IInvocation invocation)
        {
            Console.WriteLine("调用前的拦截器，方法名是：{0}。", invocation.Method.Name);// 方法名   获取当前成员的名称。 
        }
        /// <summary>
        /// 拦截的方法返回时调用的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PerformProceed(IInvocation invocation)
        {
            Console.WriteLine("拦截的方法返回时调用的拦截器，方法名是：{0}。", invocation.Method.Name);
            base.PerformProceed(invocation);
        }

        /// <summary>
        /// 调用后的拦截器
        /// </summary>
        /// <param name="invocation"></param>
        protected override void PostProceed(IInvocation invocation)
        {
            Console.WriteLine("调用后的拦截器，方法名是：{0}。", invocation.Method.Name);
        }
    }
}
