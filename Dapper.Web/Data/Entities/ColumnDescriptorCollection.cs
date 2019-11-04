using Dapper.Web.Core;
using Dapper.Web.DataAbstractions.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.Data.Entities
{
    /// <summary>
    /// 列信息集合
    /// </summary>
    public class ColumnDescriptorCollection : CollectionAbstract<IColumnDescriptor>, IColumnDescriptorCollection
    {
    }
}
