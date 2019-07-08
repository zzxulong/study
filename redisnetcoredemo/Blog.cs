#region 版权说明
/**************************************************************************
 * 文 件 名：Blog
 * 命名空间：redisnetcoredemo
 * 描　　述：
 * 版 本 号：V1.0.0
 * 作　　者：long
 * 创建时间：2019/7/5 11:05:54
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
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace redisnetcoredemo
{
	[Serializable]
	[ProtoContract]
	public class Article
	{
		[ProtoMember(1)]
		public int Id { get; set; }
		[ProtoMember(2)]
		public string Title { get; set; }
		[ProtoMember(4)]
		public string Content { get; set; }
	}
}
