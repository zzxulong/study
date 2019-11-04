using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
	public static class CommunalExtesion
	{
		/// <summary>
		/// 判断字符串是否不为Null、空
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool NotNull(this string s)
		{
			return !string.IsNullOrWhiteSpace(s);
		}
		/// <summary>
		/// 判断字符串是否为Null、空
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsNull(this string s)
		{
			return string.IsNullOrWhiteSpace(s);
		}

	}
}
