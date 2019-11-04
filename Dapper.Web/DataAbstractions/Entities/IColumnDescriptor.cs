using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions.Entities
{
	public interface IColumnDescriptor
	{
		/// <summary>
		/// 列名
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 实体属性信息
		/// </summary>
		PropertyInfo PropertyInfo { get; }

		/// <summary>
		/// 是否主键
		/// </summary>
		bool IsPrimaryKey { get; }
	}
}
