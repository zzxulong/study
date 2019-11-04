using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions.Enums
{
	/// <summary>
	/// 连接类型
	/// </summary>
	public enum JoinType
	{
		UnKnown,
		Left,
		Inner,
		Right
	}
	/// <summary>
	/// 数据库类型
	/// </summary>
	public enum SqlDialect
	{
		/// <summary>
		/// SqlServer
		/// </summary>
		SqlServer,
		/// <summary>
		/// MySql
		/// </summary>
		MySql,
		/// <summary>
		/// SQLite
		/// </summary>
		SQLite,
		/// <summary>
		/// Oracle
		/// </summary>
		Oracle
	}
	/// <summary>
	/// 主键类型
	/// </summary>
	public enum PrimaryKeyType
	{
		/// <summary>
		/// 没有主键
		/// </summary>
		[Description("无")]
		NoPrimaryKey,
		/// <summary>
		/// 整型
		/// </summary>
		[Description("Int")]
		Int,
		/// <summary>
		/// 长整型
		/// </summary>
		[Description("Long")]
		Long,
		/// <summary>
		/// 全球唯一码
		/// </summary>
		[Description("Guid")]
		Guid
			,
		/// <summary>
		/// 字符型
		/// </summary>
		[Description("String")]
		String
	}
	/// <summary>
	/// 排序规则
	/// </summary>
	public enum SortType
	{
		/// <summary>
		/// 升序
		/// </summary>
		Asc,
		/// <summary>
		/// 降序
		/// </summary>
		Desc
	}
    /// <summary>
    /// 排序规则
    /// </summary>
    public class Sort
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderBy { get; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public SortType Type { get; }

        public Sort (string orderBy, SortType type = SortType.Asc)
        {
            OrderBy = orderBy;
            Type = type;
        }
    }
}
