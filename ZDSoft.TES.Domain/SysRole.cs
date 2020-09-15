using System.Collections.Generic;
using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;

namespace ZDSoft.TES.Domain
{
    /// <summary>
    /// 实体类：用户
    /// Creator:张浩然
    /// Date:2018/5/15
    /// </summary>
    [ActiveRecord("SysRole")]
    public class SysRole : BaseEntity<SysRole>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [Property(Length = 30, NotNull = true)]
        [Display(Name = "角色名")]
        public string Name { get; set; }

        /// <summary>
        /// 角色状态
        /// </summary>
        [Property(NotNull = true)]
        [Display(Name = "角色状态")]
        public int Status { get; set; }

        /// <summary>
        /// 所拥有用户
        /// </summary>
        [HasAndBelongsToMany(typeof(SysUser), Table = "SysUser_SysRole", ColumnRef = "SysUserId", ColumnKey = "SysRoleId", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "所拥有用户")]
        public IList<SysUser> SysUserList { get; set; }
    }
}
