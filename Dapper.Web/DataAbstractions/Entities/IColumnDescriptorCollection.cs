using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions.Entities
{
	/// <summary>
	/// 列信息集合
	/// </summary>
	public interface IColumnDescriptorCollection : IList<IColumnDescriptor>
	{
	}
}
