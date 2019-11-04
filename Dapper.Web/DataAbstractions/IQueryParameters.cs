using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dapper.Web.DataAbstractions
{
    /// <summary>
    /// 参数集合
    /// </summary>
    public interface IQueryParameters
    {
        /// <summary>
        /// 添加参数
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        string Add (object value);

        /// <summary>
        /// 转换参数
        /// </summary>
        /// <returns></returns>
        DynamicParameters Parse ();
    }
}
