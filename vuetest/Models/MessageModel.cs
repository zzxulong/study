using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace vuetest.Models
{
	public class MessageModel<T>
	{
		/// <summary>
		/// 操作是否成功
		/// </summary>
		public bool Success { get; set; }
		/// <summary>
		/// 返回信息
		/// </summary>
		public string Msg { get; set; }
		/// <summary>
		/// 返回数据集合
		/// </summary>
		public List<T> Data { get; set; }
	}
}
