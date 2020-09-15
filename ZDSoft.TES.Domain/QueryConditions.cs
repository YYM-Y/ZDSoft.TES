using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDSoft.TES.Domain
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class QueryConditions
    {
        /// <summary>
        /// 属性名
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 操作符号：= > < >= <= != isEmpty like等 
        /// </summary>
        public string Operator { get; set; }

        /// <summary>
        /// 属性值
        /// </summary>
        public object Value { get; set; }
    }
}
