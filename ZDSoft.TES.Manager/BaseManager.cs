using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate.Criterion;
using NHibernate;
using ZDSoft.TES.Domain;

namespace ZDSoft.TES.Manager
{
    /// <summary>
    /// 数据访问基类
    /// Creator:张浩然
    /// Date:2019.9.10
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseManager<T> : ActiveRecordBase<T>
        where T : class
    {
        #region 增
        /// <summary>
        /// 新建实体
        /// </summary>
        public virtual void Add(T t)
        {
            ActiveRecordBase.Create(t);
        }
        #endregion

        #region 删
        /// <summary>
        /// 删除实体
        /// </summary>
        public new void Delete(T t)
        {
            ActiveRecordBase.Delete(t);
        }
        /// <summary>
        /// 根据主键删除实体
        /// </summary>
        public void Delete(int id)
        {
            T t = Get(id);//根据id获取对象
            if (t != null)//如果对象存在
            {
                Delete(t);//删除它
            }
        }
        /// <summary>
        /// 删除全部
        /// </summary>
        public void DelAll()
        {
            ActiveRecordBase.DeleteAll(typeof(T));
        }

        public void DelByWhere(string strWhere)
        {
            ActiveRecordBase.DeleteAll(typeof(T), strWhere);
        }
        #endregion

        #region 改
        /// <summary>
        /// 更新实体
        /// </summary>
        public new void Update(T t)
        {
            ActiveRecordBase.Update(t);
        }
        #endregion

        #region 查
        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        public T Get(int ID)
        {
            return FindByPrimaryKey(typeof(T), ID) as T;
        }
        /// <summary>
        /// 获取所有实体
        /// </summary>
        public IList<T> GetAll()
        {
            return FindAll(typeof(T)) as IList<T>;
        }
        /// <summary>
        /// 根据查询条件查询满足条件的实体
        /// </summary>
        public IList<T> Query(IList<ICriterion> queryConditions)
        {
            Array arr = ActiveRecordBase.FindAll(typeof(T), queryConditions.ToArray());
            return arr as IList<T>;
        }

        /// <summary>
        /// 根据SQL语句查询
        /// </summary>
        /// <param name="querySql"></param>
        /// <returns></returns>
        public IList<T> FindBySQL(string querySql)
        {
            ISession session = ActiveRecordBase.holder.CreateSession(typeof(T));//获取管理   的session对象
            IQuery query = session.CreateSQLQuery(querySql).AddEntity("arr", typeof(T));//获取满足条件
            IList<T> arr = query.List<T>();
            session.Close();
            return arr;
        }

        /// <summary>
        /// 分页获取满足条件的实体
        /// </summary>
        /// <param name="queryConditions">查询条件</param>
        /// <param name="orderList">排序属性列表</param>
        /// <param name="pageIndex">页码,从1开始</param>
        /// <param name="pageSize">每页实体数</param>
        /// <param name="count">满足条件的实体总数</param>
        /// <returns></returns>
        public IList<T> Query(IList<ICriterion> queryConditions, IList<Order> orderList, int pageIndex, int pageSize, out int count)
        {
            if (queryConditions == null)//如果为null则赋值为一个总数为0的集合
            {
                queryConditions = new List<ICriterion>();
            }
            if (orderList == null)//如果为null则赋值为一个总数为0的集合
            {
                orderList = new List<Order>();
            }
            count = Count(typeof(T), queryConditions.ToArray());//根据查询条件获取满足条件的对象总数
            Array arr = SlicedFindAll(typeof(T), (pageIndex - 1) * pageSize, pageSize, orderList.ToArray(), queryConditions.ToArray());//根据查询条件分页获取对象集合
            return arr as IList<T>;
        }

        /// <summary>
        /// 根据SQL语句获取数据集；
        /// </summary>
        ///<param name="strSql"></param>
        ///<returns></returns>
        public IList<T> GetListBySQL(string strSql)
        {
            return FindBySQL(strSql);
        }

        ////分页区和取对象集合
        //public IList<T> GetPaged(IList<ICriterion> queryConditions, IList<Order> orderList, int pageIndex, int pageSize, out int count)
        //{
        //    if (queryConditions == null)//如果为null则赋值为一个总数为0的集合
        //    {
        //        queryConditions = new List<ICriterion>();
        //    }
        //    if (orderList == null)//如果为null则赋值为一个总数为0的集合
        //    {
        //        orderList = new List<Order>();
        //    }
        //    count = Count(typeof(T), queryConditions.ToArray());//根据查询条件获取满足条件的对象总数
        //    Array arr = SlicedFindAll(typeof(T), (pageIndex - 1) * pageSize, pageSize, orderList.ToArray(), queryConditions.ToArray());//根据查询条件分页获取对象集合
        //    return arr as IList<T>;
        //}

        /// <summary>
        /// 根据查询条件分页获取实体
        /// </summary>
        /// <param name="queryConditions">查询条件集合</param>
        /// <param name="pageIndex">当前页码，从1开始</param>
        /// <param name="pageSize">页面大小</param>
        /// <param name="count">返回满足查询条件</param>
        /// <returns>返回满足查询条件的实体</returns>
        public IList<T> GetPaged(IList<QueryConditions> queryConditions, int pageIndex, int pageSize, out int count)
        {
            //实例化一个hql查询语句对象
            StringBuilder hql = new StringBuilder(@"from " + typeof(T).Name + " d");
            //根据查询条件构造hql查询语句
            for (int i = 0; i < queryConditions.Count; i++)
            {
                QueryConditions qc = queryConditions[i];//获取当前序号对应的条件
                if (qc.Value != null && qc.Value.ToString() != string.Empty)
                {
                    AddHqlSatements(hql);//增加where或and语句
                    hql.Append(string.Format("d.{0} {1} :q_{2}", qc.PropertyName, qc.Operator, i));
                }
            }

            ISession session = ActiveRecordBase.holder.CreateSession(typeof(T));//获取管理T的session对象
            IQuery query = session.CreateQuery(hql.ToString());//获取满足条件的数据
            IQuery queryScalar = session.CreateQuery("select count(ID) " + hql.ToString());//获取满足条件的数据的总数
            for (int i = 0; i < queryConditions.Count; i++)
            {
                QueryConditions qc = queryConditions[i];//获取当前序号对应的条件
                if (qc.Value != null && qc.Value.ToString() != "")
                {
                    //如果是like语句，则修改值的表达方式
                    if (qc.Operator.ToUpper() == "LIKE")
                    {
                        qc.Value = "%" + qc.Value + "%";
                    }

                    //用查询条件的值去填充hql，如d.Transportor.Name="michael"
                    queryScalar.SetParameter("q_" + i, qc.Value);
                    query.SetParameter("q_" + i, qc.Value);
                }
            }

            IList<object> result = queryScalar.List<object>();//执行查询条件总数的查询对象，返回一个集合（有一点怪异）
            int.TryParse(result[0].ToString(), out count);//尝试将返回值的第一个值转换为整形，并将转换成功的值赋值给count，如果转换失败,count=0
            query.SetFirstResult((pageIndex - 1) * pageSize);//设置获取满足条件实体的起点
            query.SetMaxResults(pageSize);//设置获取满足条件实体的终点
            IList<T> arr = query.List<T>();//返回当前页的数据
            //session.Close(); //此不要要显示关闭，因为在系统的HttpModle中已经统一处理
            return arr;
        }

        /// <summary>
        /// 根据查询条件分页获取实体
        /// </summary>
        /// <param name="queryConditions">查询条件集合</param>
        /// <returns>返回满足查询条件的实体</returns>
        /// <summary>
        /// 根据查询条件分页获取实体
        /// </summary>
        /// <param name="queryConditions">查询条件集合</param>
        /// <returns>返回满足查询条件的实体</returns>
        public IList<T> GetPaged(IList<QueryConditions> queryConditions)
        {
            //实例化一个hql查询语句对象
            StringBuilder hql = new StringBuilder(@"from " + typeof(T).Name + " d");
            //根据查询条件构造hql查询语句
            for (int i = 0; i < queryConditions.Count; i++)
            {
                QueryConditions qc = queryConditions[i];//获取当前序号对应的条件
                if (qc.Value != null)
                {
                    AddHqlSatements(hql);//增加where或and语句
                    hql.Append(string.Format("d.{0} {1} :q_{2}", qc.PropertyName, qc.Operator, i));
                }
            }

            ISession session = ActiveRecordBase.holder.CreateSession(typeof(T));//获取管理T的session对象
            IQuery query = session.CreateQuery(hql.ToString());//获取满足条件的数据
            //IQuery queryScalar = session.CreateQuery("select count(ID) " + hql.ToString());//获取满足条件的数据的总数
            for (int i = 0; i < queryConditions.Count; i++)
            {
                QueryConditions qc = queryConditions[i];//获取当前序号对应的条件
                if (qc.Value != null)
                {
                    //如果是like语句，则修改值的表达方式
                    if (qc.Operator.ToUpper() == "LIKE")
                    {
                        qc.Value = "%" + qc.Value + "%";
                    }

                    //用查询条件的值去填充hql，如d.Transportor.Name="michael"
                    //queryScalar.SetParameter("q_" + i, qc.Value);
                    query.SetParameter("q_" + i, qc.Value);
                }
            }

            //ISession session = ActiveRecordBase.holder.CreateSession(typeof(T));//获取管理T的session对象
            //IQuery query = session.CreateQuery(hql.ToString());//获取满足条件的数据

            IList<T> arr = query.List<T>();//返回当前页的数据
            //session.Close(); //此不要要显示关闭，因为在系统的HttpModle中已经统一处理
            return arr;
        }
        //public IList<T> GetPaged(IList<QueryConditions> queryConditions)
        //{
        //    //实例化一个hql查询语句对象
        //    StringBuilder hql = new StringBuilder(@"from " + typeof(T).Name + " d");
        //    //根据查询条件构造hql查询语句
        //    for (int i = 0; i < queryConditions.Count; i++)
        //    {
        //        QueryConditions qc = queryConditions[i];//获取当前序号对应的条件
        //        if (qc.Value != null)
        //        {
        //            AddHqlSatements(hql);//增加where或and语句
        //            hql.Append(string.Format("d.{0} {1} :q_{2}", qc.PropertyName, qc.Operator, i));
        //        }
        //    }

        //    ISession session = ActiveRecordBase.holder.CreateSession(typeof(T));//获取管理T的session对象
        //    IQuery query = session.CreateQuery(hql.ToString());//获取满足条件的数据

        //    IList<T> arr = query.List<T>();//返回当前页的数据
        //    //session.Close(); //此不要要显示关闭，因为在系统的HttpModle中已经统一处理
        //    return arr;
        //}

        protected void AddHqlSatements(StringBuilder hql)
        {
            if (!hql.ToString().Contains("where"))//查询语句的开始条件是where
            {
                hql.Append(" where ");
            }
            else//当hql中有了一个where后再添加查询条件时就应该使用and了
            {
                hql.Append(" and ");
            }
        }

        /// <summary>
        /// 根据条件查找数据，返回集合
        /// </summary>
        /// <param name="queryConditions"></param>
        /// <returns></returns>
        public IList<T> Find(IList<ICriterion> queryConditions)
        {
            Array arr = ActiveRecordBase.FindAll(typeof(T), queryConditions.ToArray());
            return arr as IList<T>;
        }
        #endregion


    }

}
