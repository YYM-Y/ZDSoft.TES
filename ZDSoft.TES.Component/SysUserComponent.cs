using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZDSoft.TES.Domain;
using ZDSoft.TES.Manager;
using ZDSoft.TES.Service;

namespace ZDSoft.TES.Component
{
    public class SysUserComponent : BaseComponent<SysUser, SysUserManager>, ISysUserService
    {
        public SysUser Login(string account, string password)
        {
            IList<ICriterion> criterionList = new List<ICriterion>();
            criterionList.Add(Expression.Eq("Account", account));
            criterionList.Add(Expression.Eq("Password", password));

            SysUser user = manager.Query(criterionList).FirstOrDefault();
            return user;
        }
    }
}
