using System.Collections.Generic;
using Castle.ActiveRecord;
using System.ComponentModel.DataAnnotations;

namespace ZDSoft.TES.Domain
{
    /// <summary>
    /// 实体类：用户
    /// Creator:张浩然
    /// Date:2018/1/6
    /// </summary>
    [ActiveRecord("SysUser")]
    public class SysUser : BaseEntity<SysUser>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Display(Name = "用户名")]
        [Property(Length = 30, NotNull = true)]
        public string Name { get; set; }

        /// <summary>
        /// 登录账户
        /// </summary>
        [Display(Name = "登录账户")]
        [Property(Length = 50, NotNull = true)]
        public string Account { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Display(Name = "登录密码")]
        [Property(Length = 50, NotNull = true)]
        public string Password { get; set; }

        /// <summary>
        /// 用户状态
        ///     1：正常
        ///     0：禁用
        /// </summary>
        [Display(Name = "用户状态")]
        [Property(NotNull = true)]
        public int Status { get; set; }

        /// <summary>
        /// 所属角色
        /// </summary>
        [HasAndBelongsToMany(typeof(SysRole), Table = "SysUser_SysRole", ColumnKey = "SysUserId", ColumnRef = "SysRoleId", Cascade = ManyRelationCascadeEnum.None, Lazy = false)]
        [Display(Name = "所属角色")]
        public IList<SysRole> SysRoleList { get; set; }
    }
}
