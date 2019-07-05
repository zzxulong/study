#region 版权说明
/**************************************************************************
 * 文 件 名：MyClass
 * 命名空间：CastleCoreTest.Core
 * 描　　述：
 * 版 本 号：V1.0.0
 * 作　　者：long
 * 创建时间：2019/6/3 17:48:46
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
using System.Text;

namespace CastleCoreTest.Core
{
    /// <summary>
    /// virtual这个算是castle的一个标志，不管是方法或者是属性都要这个
    /// </summary>
    public class MyClass : IMyClass
    {
        public virtual void MyMethod()
        {
            Console.WriteLine("My Mehod");
        }
    }
}
