using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZDSoft.TES.Domain
{
    /// <summary>
    /// 实体类：基类
    /// Creator：zhr
    /// Date：2018/1/5
    /// </summary>
    public class BaseEntity<T> : ActiveRecordBase
        where T : class
    {
        /// <summary>
        /// ID
        /// </summary>
        [PrimaryKey(PrimaryKeyType.Native)]
        public virtual int ID { get; set; }
    }
}
