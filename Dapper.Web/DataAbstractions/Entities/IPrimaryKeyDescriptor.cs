﻿using Dapper.Web.DataAbstractions.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions.Entities
{
	/// <summary>
	/// 主键信息
	/// </summary>
	public interface IPrimaryKeyDescriptor
	{
		/// <summary>
		/// 列名
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 主键类型
		/// </summary>
		PrimaryKeyType Type { get; }

		/// <summary>
		/// 属性信息
		/// </summary>
		PropertyInfo PropertyInfo { get; }

		/// <summary>
		/// 判断是否是指定类型
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		bool Is(PrimaryKeyType type);

		/// <summary>
		/// NoPrimaryKey
		/// </summary>
		/// <returns></returns>
		bool IsNo();

		/// <summary>
		/// Int
		/// </summary>
		/// <returns></returns>
		bool IsInt();

		/// <summary>
		/// Long
		/// </summary>
		/// <returns></returns>
		bool IsLong();

		/// <summary>
		/// Guid
		/// </summary>
		/// <returns></returns>
		bool IsGuid();

	}
}
