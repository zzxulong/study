#region 版权说明
/**************************************************************************
 * 文 件 名：TransactionWithRetries
 * 命名空间：CastleCoreTest.Core
 * 描　　述：
 * 版 本 号：V1.0.0
 * 作　　者：long
 * 创建时间：2019/6/3 17:50:23
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
using System.Transactions;

namespace CastleCoreTest.Core
{
    public class TransactionWithRetries : IInterceptor
     {
         private readonly Int32 _maxRetries;
 
         public TransactionWithRetries(Int32 maxRetries)
         {
             _maxRetries = maxRetries;
         }
 
         public void Intercept(IInvocation invocation)
         {
 
             Console.WriteLine("before22222");
             var successed = false;
             var retries = _maxRetries;
             while (!successed)
             {
                 using (var trans = new TransactionScope())
                 {
                     try
                     {
                         Console.WriteLine("before");
                         invocation.Proceed();
                         Console.WriteLine("End");
                         trans.Complete();
                     }
                     catch
                     {
                         if (retries >= 0)
                         {
                             Console.WriteLine("Retrying");
                             retries--;
                         }
                         else
                         {
                             throw;
                         }
                     }
                 }
             }
 
         }
 
 
     }
}
