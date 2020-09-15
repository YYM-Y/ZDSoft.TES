using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ZDSoft.TES.Core;
using ZDSoft.TES.Domain;
using ZDSoft.TES.Service;
using ZDSoft.TES.Web.Apps;

namespace ZDSoft.TES.Web
{
    public partial class InitDataBase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCreateDB_Click(object sender, EventArgs e)
        {
            string ret = "<br />";

            if (!ActiveRecordStarter.IsInitialized)
            {
                //如果ActiveRecordStarter框架没有初始化
                IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
                ActiveRecordStarter.Initialize(typeof(SysUser).Assembly, source);
            }
            ret += "正在创建数据库...<br />";
            ActiveRecordStarter.CreateSchema();
            ret += "创建数据库完成<br />";
            ret += "正在初始化数据...<br/>";

            InitSysRole();
            ret += "初始化角色完毕！<br/>";

            InitSysUser();
            ret += "初始化用户完毕！<br/>";

            Response.Write(ret);
        }

        
    

        /// <summary>
        /// 初始化用户
        /// </summary>
        private void InitSysUser()
        {
            //id=1
            Container.Instance.Resolve<ISysUserService>().Add(new SysUser()
            {
                Name = "周龙福",
                Account = "00001",
                Password = AppHelper.EncodeMd5("123"),
                Status = 1,
                SysRoleList = Container.Instance.Resolve<ISysRoleService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 2 }) })
            });
            //id=2
            Container.Instance.Resolve<ISysUserService>().Add(new SysUser()
            {
                Name = "高峰",
                Account = "04201",
                Password = AppHelper.EncodeMd5("123"),
                Status = 1,
                SysRoleList = Container.Instance.Resolve<ISysRoleService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 1,2 }) })
            });
            //id=3
            Container.Instance.Resolve<ISysUserService>().Add(new SysUser()
            {
                Name = "张三",
                Account = "19002",
                Password = AppHelper.EncodeMd5("123"),
                Status = 1,
                SysRoleList = Container.Instance.Resolve<ISysRoleService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 3 }) })
            });

        }

        /// <summary>
        /// 初始化角色
        /// </summary>
        private void InitSysRole()
        {
            Container.Instance.Resolve<ISysRoleService>().Add(new SysRole()
            {
                Name = "系统管理员",
                Status = 1,
              
            });

            Container.Instance.Resolve<ISysRoleService>().Add(new SysRole()
            {
                Name = "教师",
                Status = 1,
                //SystemFunctionList = Container.Instance.Resolve<ISystemFunctionService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 12, 13, 14, 15, 22 }) })
            });

            Container.Instance.Resolve<ISysRoleService>().Add(new SysRole()
            {
                Name = "学生",
                Status = 1,
                //SystemFunctionList = Container.Instance.Resolve<ISystemFunctionService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 12, 16 }) })
            });


        }
        /*
        /// <summary>
        /// 初始化学生
        /// </summary>
        private void InitStudent()
        {
            #region 学生
            //id=1
            Container.Instance.Resolve<IStudentService>().Add(new Student()
            {
                StudentCode = "169002411",
                StudentName = "蒋林峰",
                Sex = 0,
                IsActive = 1,
                ClassInfo = Container.Instance.Resolve<IClassInfoService>().GetEntity(1),
                Major = Container.Instance.Resolve<IDictionaryService>().GetEntity(1),
                Remark = ""
                //SysUser = Container.Instance.Resolve<ISysUserService>().GetEntity(7)
            });
            //id=2
            Container.Instance.Resolve<IStudentService>().Add(new Student()
            {
                StudentCode = "169001003",
                StudentName = "蒙诗",
                Sex = 0,
                IsActive = 1,
                ClassInfo = Container.Instance.Resolve<IClassInfoService>().GetEntity(1),
                Major = Container.Instance.Resolve<IDictionaryService>().GetEntity(1),
                Remark = ""
                //SysUser = Container.Instance.Resolve<ISysUserService>().GetEntity(8)
            });
            #endregion
        }

        /// <summary>
        /// 初始化教师
        /// </summary>
        private void InitTeacher()
        {
            #region 教师
            //id=1
            Container.Instance.Resolve<ITeacherService>().Add(new Teacher()
            {
                TeacherCode = "00211",
                TeacherName = "周龙福",
                IsActive = 1,
                Remark = "",
                Position = Container.Instance.Resolve<IDictionaryService>().GetEntity(5)
                //SysUser = Container.Instance.Resolve<ISysUserService>().GetEntity(2)
                //NormTaskList = Container.Instance.Resolve<INormTaskService>().GetAll()
            });

            //id=2
            Container.Instance.Resolve<ITeacherService>().Add(new Teacher()
            {
                TeacherCode = "00209",
                TeacherName = "袁开友",
                IsActive = 1,
                Remark = "",
                Position = Container.Instance.Resolve<IDictionaryService>().GetEntity(5)
                //SysUser = Container.Instance.Resolve<ISysUserService>().GetEntity(2)
                //NormTaskList = Container.Instance.Resolve<INormTaskService>().GetAll()
            });

            //id=3
            Container.Instance.Resolve<ITeacherService>().Add(new Teacher()
            {
                TeacherCode = "00208",
                TeacherName = "冷亚洪",
                IsActive = 1,
                Remark = "",
                Position = Container.Instance.Resolve<IDictionaryService>().GetEntity(6)
                //SysUser = Container.Instance.Resolve<ISysUserService>().GetEntity(3)
                //NormTaskList = Container.Instance.Resolve<INormTaskService>().GetAll()
            });

            //id=4
            Container.Instance.Resolve<ITeacherService>().Add(new Teacher()
            {
                TeacherCode = "00130",
                TeacherName = "秦红梅",
                IsActive = 1,
                Remark = "",
                Position = Container.Instance.Resolve<IDictionaryService>().GetEntity(5)
                //SysUser = Container.Instance.Resolve<ISysUserService>().GetEntity(2)
                //NormTaskList = Container.Instance.Resolve<INormTaskService>().GetAll()
            });

            //id=5
            Container.Instance.Resolve<ITeacherService>().Add(new Teacher()
            {
                TeacherCode = "04201",
                TeacherName = "高峰",
                IsActive = 1,
                Remark = ""
                //SysUser = Container.Instance.Resolve<ISysUserService>().GetEntity(5)
            });

            //id=6
            Container.Instance.Resolve<ITeacherService>().Add(new Teacher()
            {
                TeacherCode = "02201",
                TeacherName = "付祥明",
                IsActive = 1,
                Remark = ""
                //SysUser = Container.Instance.Resolve<ISysUserService>().GetEntity(5)
            });
            //id=7
            Container.Instance.Resolve<ITeacherService>().Add(new Teacher()
            {
                TeacherCode = "00201",
                TeacherName = "张浩然",
                IsActive = 1,
                Remark = "",
                Organization = Container.Instance.Resolve<IOrganizationService>().GetEntity(7)
                //SysUser = Container.Instance.Resolve<ISysUserService>().GetEntity(1)
            });

            #endregion
        }

        #region 初始化基础数据
        public void InitData()
        {
            #region 初始化组织机构

            //id=1
            Core.Container.Instance.Resolve<IOrganizationService>().Add(new Organization()
            {
                OrgCode = "100000",
                OrgName = "重庆工程学院",
                ParentOrg = null
            });
            Organization TopOrg = Container.Instance.Resolve<IOrganizationService>().GetEntity(1);
            //id=2
            Core.Container.Instance.Resolve<IOrganizationService>().Add(new Organization()
            {
                OrgCode = "101000",
                OrgName = "软件学院",
                ParentOrg = TopOrg
            });
            //id=3
            Core.Container.Instance.Resolve<IOrganizationService>().Add(new Organization()
            {
                OrgCode = "102000",
                OrgName = "大数据与人工智能学院",
                ParentOrg = TopOrg
            });
            //id=4
            Core.Container.Instance.Resolve<IOrganizationService>().Add(new Organization()
            {
                OrgCode = "103000",
                OrgName = "计算机与物联网学院",
                ParentOrg = TopOrg
            });
            //id=5
            Core.Container.Instance.Resolve<IOrganizationService>().Add(new Organization()
            {
                OrgCode = "104000",
                OrgName = "管理学院",
                ParentOrg = TopOrg
            });
            Organization SoftOrg = Container.Instance.Resolve<IOrganizationService>().GetEntity(2);
            //id=6
            Core.Container.Instance.Resolve<IOrganizationService>().Add(new Organization()
            {
                OrgCode = "101010",
                OrgName = "软件工程系",
                ParentOrg = SoftOrg
            });
            //id=7
            Core.Container.Instance.Resolve<IOrganizationService>().Add(new Organization()
            {
                OrgCode = "101020",
                OrgName = "数字媒体艺术系",
                ParentOrg = SoftOrg
            });
            //id=8
            Core.Container.Instance.Resolve<IOrganizationService>().Add(new Organization()
            {
                OrgCode = "101020",
                OrgName = "人工智能与大数据系",
                ParentOrg = SoftOrg
            });
            #endregion

            #region 课程
            //id=1
            Container.Instance.Resolve<ICourseInfoService>().Add(new CourseInfo()
            {
                CourseCode = "0001",
                CourseName = "C#程序设计基础",
                Remark = ""
            });
            //id=2
            Container.Instance.Resolve<ICourseInfoService>().Add(new CourseInfo()
            {
                CourseCode = "0002",
                CourseName = "C#面向对象编程",
                Remark = ""
            });
            //id=3
            Container.Instance.Resolve<ICourseInfoService>().Add(new CourseInfo()
            {
                CourseCode = "0003",
                CourseName = "dotNet框架程序设计",
                Remark = ""
            });
            //id=4
            Core.Container.Instance.Resolve<ICourseInfoService>().Add(new CourseInfo()
            {
                CourseCode = "201900001",
                CourseName = "专业综合实训",
                Remark = "这门课程好啊！"
            });
            #endregion

            #region 班级
            //id=1
            Container.Instance.Resolve<IClassInfoService>().Add(new ClassInfo()
            {
                ClassCode = "1690011",
                ClassName = "1690011班",
                Organization = Container.Instance.Resolve<IOrganizationService>().GetEntity(6),
                //TeacherList = Container.Instance.Resolve<ITeacherService>().Query(new List<ICriterion>() { Expression.In("ID",new int[]{1,3})}),
                //StudentList = Container.Instance.Resolve<IStudentService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 3,5}) }),
                Remark = ""
            });
            Container.Instance.Resolve<IClassInfoService>().Add(new ClassInfo()
            {
                ClassCode = "1690010",
                ClassName = "1690010班",
                Organization = Container.Instance.Resolve<IOrganizationService>().GetEntity(6),
                //TeacherList = Container.Instance.Resolve<ITeacherService>().Query(new List<ICriterion>() { Expression.In("ID",new int[]{1,3})}),
                //StudentList = Container.Instance.Resolve<IStudentService>().Query(new List<ICriterion>() { Expression.In("ID", new int[] { 3,5}) }),
                Remark = ""
            });
            #endregion
        }
        #endregion


        #region 初始化评价批次
        private void InitEvalBatch()
        {
            Container.Instance.Resolve<IEvalBatchService>().Add(new EvalBatch()
            {
                Name = "2018-2019学年第2学期",
                StartDate = DateTime.Parse("2019-4-20"),
                EndDate = DateTime.Parse("2018-7-20"),
                Remark = "2018-2019学年第2学期",
                IsActive = 1,
                TeacherList = Container.Instance.Resolve<ITeacherService>().Query(new List<ICriterion>() { Expression.Le("ID", 7) })
            });
            Container.Instance.Resolve<IEvalBatchService>().Add(new EvalBatch()
            {
                Name = "2019-2020学年第1学期",
                StartDate = DateTime.Parse("2019-9-1"),
                EndDate = DateTime.Parse("2020-1-25"),
                Remark = "2019-2020学年第1学期",
                IsActive = 1,
                TeacherList = Container.Instance.Resolve<ITeacherService>().Query(new List<ICriterion>() { Expression.Le("ID", 7) })
            });

        }
        #endregion

        #region 初始化指标
        public void InitNorm()
        {
            //id=1
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0100000000",
                NormName = "学生方面",
                Score = 0.3m,
                IsBottom = false,
            });
            //id=2
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0200000000",
                NormName = "系(部)方 面",
                Score = 0.25m,
                IsBottom = false,
            });
            //id=3
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0300000000",
                NormName = "教研室方面",
                Score = 0.2m,
                IsBottom = false,
            });
            //id=4
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0400000000",
                NormName = "同行方面(领导、督导)",
                Score = 0.15m,
                IsBottom = false,
            });
            //id=5
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0500000000",
                NormName = "教师个人方面",
                Score = 0.1m,
                IsBottom = false,
            });
            //id=6
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0101000000",
                NormName = "概念的讲解",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=7
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0102000000",
                NormName = "重点和难点",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=8
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0103000000",
                NormName = "逻辑性和条理性",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=9
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0104000000",
                NormName = "趣味性和生动性",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=10
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0105000000",
                NormName = "板书",
                Score = 0.05m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=11
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0106000000",
                NormName = "辅导（阅读指导）",
                Score = 0.08m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=12
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0107000000",
                NormName = "作业与批改",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=13
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0108000000",
                NormName = "能力培养",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=14
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0109000000",
                NormName = "教书育人",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=15
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0110000000",
                NormName = "为人师表",
                Score = 0.07m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(1),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(1)
            });
            //id=16
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0201000000",
                NormName = "量考核",
                Score = 0.3m,
                IsBottom = false,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(2)
            });
            //id=17
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202000000",
                NormName = "质考核",
                Score = 0.7m,
                IsBottom = false,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(2)
            });
            //id=18
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0201010000",
                NormName = "教学工作量",
                Score = 0.75m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(16)
            });
            //id=19
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0201020000",
                NormName = "社会工作量",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(16)
            });
            //id=20
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0201030000",
                NormName = "任课班级",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(16)
            });
            //id=21
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010000",
                NormName = "工作态度",
                Score = 0.4m,
                IsBottom = false,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(17)
            });
            //id=22
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202020000",
                NormName = "学术与研究水平",
                Score = 0.15m,
                IsBottom = false,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(17)
            });
            //id=23
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202030000",
                NormName = "完成任务情况",
                Score = 0.05m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(17)
            });
            //id=24
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202040000",
                NormName = "教学水平变化",
                Score = 0.05m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(17)
            });
            //id=25
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202050000",
                NormName = "教学反映",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(17)
            });
            //id=26
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202060000",
                NormName = "能力培养",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(17)
            });
            //id=27
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202070000",
                NormName = "汲取新信息新技术",
                Score = 0.05m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(17)
            });
            //id=28
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202080000",
                NormName = "考试命题",
                Score = 0.05m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(17)
            });
            //id=29
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010100",
                NormName = "接受任务态度",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(21)
            });
            //id=30
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010200",
                NormName = "教学常规",
                Score = 0.9m,
                IsBottom = false,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(21)
            });
            //id=31
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010201",
                NormName = "授课计划的制定",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(30)
            });
            //id=32
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010202",
                NormName = "教案首页",
                Score = 0.2m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(30)
            });
            //id=33
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010203",
                NormName = "备课余量",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(30)
            });
            //id=34
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010204",
                NormName = "教学日志手册的填写",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(30)
            });
            //id=35
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010205",
                NormName = "教学表格的填写",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(30)
            });
            //id=36
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010206",
                NormName = "辅导",
                Score = 0.2m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(30)
            });
            //id=37
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202010207",
                NormName = "教学秩序的掌握",
                Score = 0.2m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(30)
            });
            //id=38
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202020100",
                NormName = "职称",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(22)
            });
            //id=39
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202020200",
                NormName = "运用新知识、新技术能力",
                Score = 0.4m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(22)
            });
            //id=40
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0202020300",
                NormName = "论文撰写、教材编写能力",
                Score = 0.5m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(2),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(22)
            });
            //id=41
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301000000",
                NormName = "教学环节",
                Score = 0.6m,
                IsBottom = false,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(3)
            });
            //id=42
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0302000000",
                NormName = "接受任务的态度",
                Score = 0.05m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(3)
            });
            //id=43
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0303000000",
                NormName = "汲取新技术",
                Score = 0.05m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(3)
            });
            //id=44
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0304000000",
                NormName = "学术与研究水平",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(3)
            });
            //id=45
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0305000000",
                NormName = "参加教研活动",
                Score = 0.2m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(3)
            });
            //id=46
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301010000",
                NormName = "概念的讲解",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(41)
            });
            //id=47
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301020000",
                NormName = "重点和难点",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(41)
            });
            //id=48
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301030000",
                NormName = "逻辑性、条理性",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(41)
            });
            //id=49
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301040000",
                NormName = "趣味性、生动性",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(41)
            });
            //id=50
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301050000",
                NormName = "板书",
                Score = 0.05m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(41)
            });
            //id=51
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301060000",
                NormName = "能力培养",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(41)
            });
            //id=52
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301070000",
                NormName = "理论联系实际",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(41)
            });
            //id=53
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301080000",
                NormName = "辅导（阅读指导）",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(41)
            });
            //id=54
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0301090000",
                NormName = "作业与批改",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(3),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(41)
            });
            //id=55
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0401000000",
                NormName = "组织教学",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(4),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(4)
            });
            //id=56
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0402000000",
                NormName = "教学内容与教学要求",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(4),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(4)
            });
            //id=57
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0403000000",
                NormName = "概念讲解",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(4),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(4)
            });
            //id=58
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0404000000",
                NormName = "重点和难点",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(4),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(4)
            });
            //id=59
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0405000000",
                NormName = "趣味性与生动性",
                Score = 0.08m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(4),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(4)
            });
            //id=60
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0406000000",
                NormName = "直观教学与板书",
                Score = 0.07m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(4),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(4)
            });
            //id=61
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0407000000",
                NormName = "智力能力的培养",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(4),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(4)
            });
            //id=62
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0408000000",
                NormName = "理论联系实际",
                Score = 0.1m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(4),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(4)
            });
            //id=63
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0409000000",
                NormName = "教材处理",
                Score = 0.15m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(4),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(4)
            });
            //id=64
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0501000000",
                NormName = "自我评价",
                Score = 0.5m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(5),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(5)
            });
            //id=65
            Container.Instance.Resolve<INormService>().Add(new Norm()
            {
                NormCode = "0502000000",
                NormName = "自我评价的工作",
                Score = 0.5m,
                IsBottom = true,
                TopNorm = Container.Instance.Resolve<INormService>().GetEntity(5),
                Parent = Container.Instance.Resolve<INormService>().GetEntity(5)
            });



        }
        #endregion

        #region 创建NormOption
        public void InitNormOption()
        {
            #region 学生方面
            //id=1
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "语言精练，深入浅出，讲解准确",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(6)
            });
            //id=2
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解清晰，容易接受",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(6)
            });
            //id=3
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解基本准确，但不易接受",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(6)
            });
            //id=4
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "概念紊乱，时有差错",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(6)
            });
            //id=5
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "重点突出，讲清难点，举一反三",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(7)
            });
            //id=6
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "能把握重点、难点，但讲解不够明确",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(7)
            });
            //id=7
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "重点不明显，难点讲不透",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(7)
            });

            //id=8
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "重点一言而过，难点草率了事",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(7)
            });
            //id=9
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "层次分明，融会贯通",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(8)
            });
            //id=10
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "条目较清楚，有分析归纳",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(8)
            });
            //id=11
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "平淡叙述，缺乏连贯性",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(8)
            });
            //id=12
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "杂乱无章，前后矛盾",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(8)
            });
            //id=13
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解方法新颖，举例生动，有吸引力",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(9)
            });
            //id=14
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解较熟练，语言通俗",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(9)
            });
            //id=15
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解平淡，语言单调",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(9)
            });
            //id=16
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解生疏，远离课题，语言枯燥",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(9)
            });
            //id=17
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "简繁适度，清楚醒目",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(10)
            });
            //id=18
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "条目明白，书写整洁",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(10)
            });
            //id=19
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "布局较差，详略失当",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(10)
            });
            //id=20
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "次序凌乱，书写潦草",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(10)
            });
            //id=21
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "辅导及时、并指导课外阅读",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(11)
            });
            //id=22
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "定期辅导，并布置课外阅读",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(11)
            });
            //id=23
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "辅导较少",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(11)
            });
            //id=24
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "没有辅导",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(11)
            });
            //id=25
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "选题得当，批改及时，注意讲评",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(12)
            });
            //id=26
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "作业适量，批改及时",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(12)
            });
            //id=27
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "作业量时轻时重，批改不够及时",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(12)
            });
            //id=28
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "选题随便，批改马虎",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(12)
            });
            //id=29
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "思路开阔，鼓励创新，注意能力培养、效果明显",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(13)
            });
            //id=30
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "注意学生能力培养，并在教学中有所体现",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(13)
            });
            //id=31
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "能提出能力培养的要求，但缺乏具体的办法",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(13)
            });
            //id=32
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "忽视能力培养，单纯灌输书本知识",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(13)
            });
            //id=33
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "全面关心学生，经常接触学生，亲切、严格",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(14)
            });
            //id=34
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "关心学生的学业，引导学生学好本门课程",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(14)
            });
            //id=35
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "单纯完成上课任务，与同学接触较少",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(14)
            });
            //id=36
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "对学生漠不关心，放任自流",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(14)
            });
            //id=37
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "严于律己，以身作则，堪称楷模",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(15)
            });
            //id=38
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "举止文明，待人热情",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(15)
            });
            //id=39
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "注意礼貌，待人和气",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(15)
            });
            //id=40
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "要求不严，言谈失当",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(15)
            });
            #endregion
            #region 系（部）方面
            //id=41
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "超工作量",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(18)
            });
            //id=42
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "满工作量",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(18)
            });
            //id=43
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "接近完成（70%）",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(18)
            });
            //id=44
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "差距较大",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(18)
            });
            //id=45
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "担任教研室主任",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(19)
            });
            //id=46
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "担任办公室、工作室主任",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(19)
            });
            //id=47
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "担任专业班主任（辅导员）等其他工作",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(19)
            });
            //id=48
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "未承担",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(19)
            });
            //id=49
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "任4个班级以上，或双进度",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(20)
            });
            //id=50
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "任3个班级",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(20)
            });
            //id=51
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "任2个班级",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(20)
            });
            //id=52
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "任1个班级",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(20)
            });
            //id=53
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "勇挑重担",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(29)
            });
            //id=54
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "主动承担",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(29)
            });
            //id=55
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一    般",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(29)
            });
            //id=56
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "较    差",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(29)
            });
            //id=57
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "勇挑重担",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(29)
            });
            //id=58
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "主动承担",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(29)
            });
            //id=59
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一    般",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(29)
            });
            //id=60
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "较    差",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(29)
            });
            //id=61
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "清晰",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(31)
            });
            //id=62
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "完整",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(31)
            });
            //id=63
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一般",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(31)
            });
            //id=64
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "潦草",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(31)
            });
            //id=65
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "完整",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(32)
            });
            //id=66
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "缺一项",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(32)
            });
            //id=67
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "缺二项",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(32)
            });
            //id=68
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "缺二项以上",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(32)
            });
            //id=69
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一周以上",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(33)
            });
            //id=70
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一周",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(33)
            });
            //id=71
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "接近一周",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(33)
            });
            //id=72
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "没有余量",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(33)
            });
            //id=73
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "清楚、准确",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(34)
            });
            //id=74
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "正确、及时",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(34)
            });
            //id=75
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "填写缺项",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(34)
            });
            //id=76
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "填写马虎",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(34)
            });
            //id=77
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "认真且有见解",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(35)
            });
            //id=78
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "详尽、整洁",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(35)
            });
            //id=79
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "正确",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(35)
            });
            //id=80
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "潦草、拖拉",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(35)
            });
            //id=81
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "每周有辅导",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(36)
            });
            //id=82
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "辅导较经常",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(36)
            });
            //id=83
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "辅导较少",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(36)
            });
            //id=84
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "不辅导",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(36)
            });
            //id=85
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "严格",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(37)
            });
            //id=86
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "较严格",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(37)
            });
            //id=87
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一般",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(37)
            });
            //id=88
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "出现教学事故",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(37)
            });
            //id=89
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "副教授",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(38)
            });
            //id=90
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲师",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(38)
            });
            //id=91
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "助教",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(38)
            });
            //id=92
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "未评职称",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(38)
            });
            //id=93
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "开出有一定水平的选修课、讲座、院级公开课或指导兴趣小组有成效",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(39)
            });
            //id=94
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "开出选修课、讲座、科内公开课或指导兴趣小组活动",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(39)
            });
            //id=95
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "承担室内研究课、协助开出讲座或配合指导学生课外活动",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(39)
            });
            //id=96
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "无",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(39)
            });
            //id=97
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "在核心刊物上发表、教材正式出版（三年内）",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(40)
            });
            //id=98
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "在公开刊物上发表，教材兄弟院校使用（二年内）",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(40)
            });
            //id=99
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "在内部刊上发表，教材在校内使用（一年内）",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(40)
            });
            //id=100
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "无",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(40)
            });
            //id=101
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "高质量完成",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(23)
            });
            //id=102
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "及时完成",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(23)
            });
            //id=103
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "基本完成",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(23)
            });
            //id=104
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "完不成",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(23)
            });
            //id=105
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "显著提高",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(24)
            });
            //id=106
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "有所提高",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(24)
            });
            //id=107
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "变化很小",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(24)
            });
            //id=108
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "下降",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(24)
            });
            //id=109
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "优秀",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(25)
            });
            //id=110
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "良好",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(25)
            });
            //id=111
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一般",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(25)
            });
            //id=112
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "较差",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(25)
            });
            //id=113
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "思路开阔，鼓励创新，注意能力培养、效果明显",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(26)
            });
            //id=114
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "注意学生能力培养，并在教学中有所体现",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(26)
            });
            //id=115
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "能提出能力培养的要求，但缺乏具体的办法",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(26)
            });
            //id=116
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "忽视能力培养，单纯灌输书本知识",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(26)
            });
            //id=117
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "及时在教学中体现",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(27)
            });
            //id=118
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "教学中注意联系新信息新技术",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(27)
            });
            //id=119
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "教学中联系新信息新技术不够",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(27)
            });
            //id=120
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "教学中根本不联系新信息新技术",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(27)
            });
            //id=121
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "试题的水平、题型、题量、分布、覆盖面符合教学目标的要求；难度适中，区分度适当；表述准确、严密、简洁。",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(28)
            });
            //id=122
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "有2项不符合要求",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(28)
            });
            //id=123
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "有3项不符合要求",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(28)
            });
            //id=124
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "有3项以上（不含3项）不符合要求",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(28)
            });
            #endregion
            #region 教研室
            //id=125
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "语言精练，深入浅出，讲解准确",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(46)
            });
            //id=126
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解清晰，容易接受",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(46)
            });
            //id=127
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解基本准确，但不易接受",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(46)
            });
            //id=128
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "概念紊乱，时有差错",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(46)
            });
            //id=129
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "重点突出，讲清难点，举一反三",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(47)
            });
            //id=130
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "能把握重点、难点，但讲解不够明确",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(47)
            });
            //id=131
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "重点不明显，难点讲不透",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(47)
            });
            //id=132
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "重点一言而过，难点草率了事",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(47)
            });
            //id=133
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "层次分明，融会贯通",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(48)
            });
            //id=134
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "条目较清楚，有分析归纳",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(48)
            });
            //id=135
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "平淡叙述，缺乏连贯性",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(48)
            });
            //id=136
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "杂乱无章，前后矛盾",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(48)
            });
            //id=137
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解方法新颖，举例生动，有吸引力",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(49)
            });
            //id=138
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解较熟练，语言通俗",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(49)
            });
            //id=139
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解平淡，语言单调",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(49)
            });
            //id=140
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解生疏，远离课题，语言枯燥",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(49)
            });
            //id=141
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "简繁适度，清楚醒目",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(50)
            });
            //id=142
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "条目明白，书写整洁",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(50)
            });
            //id=143
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "布局较差，详略适当",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(50)
            });
            //id=144
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "次序凌乱，书写潦草",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(50)
            });
            //id=145
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "思路开阔，鼓励创新，注意能力培养、效果明显",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(51)
            });
            //id=146
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "注意学生能力培养，并在教学中有所体现",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(51)
            });
            //id=147
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "能提出能力培养的要求，但缺乏具体的办法",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(51)
            });
            //id=148
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "忽视能力培养，单纯灌输书本知识",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(51)
            });
            //id=149
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "理论紧密联系当前实际",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(52)
            });
            //id=150
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "理论能联系具体事例",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(52)
            });
            //id=151
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "联系实际较勉强",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(52)
            });
            //id=152
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "只有理论没有实际",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(52)
            });
            //id=153
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "辅导及时、并指导课外阅读",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(53)
            });
            //id=154
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "定期辅导，并布置课外阅读",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(53)
            });
            //id=155
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "辅导较少",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(53)
            });
            //id=156
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "没有辅导",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(53)
            });
            //id=157
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "选题得当，批改及时，注意讲评",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(54)
            });
            //id=158
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "作业适量，批改及时",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(54)
            });
            //id=159
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "作业量时轻时重，批改不够及时",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(54)
            });
            //id=160
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "选题随便，批改马虎",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(54)
            });
            //id=161
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "选题得当，批改及时，注意讲评",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(54)
            });
            //id=162
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "作业适量，批改及时",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(54)
            });
            //id=163
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "作业量时轻时重，批改不够及时",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(54)
            });
            //id=164
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "选题随便，批改马虎",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(54)
            });
            //id=165
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "勇挑重担",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(42)
            });
            //id=166
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "主动承担",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(42)
            });
            //id=167
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一    般",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(42)
            });
            //id=168
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "较    差",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(42)
            });
            //id=169
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "及时在教学中体现",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(43)
            });
            //id=170
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "教学中注意联系新信息新技术",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(43)
            });
            //id=171
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "教学中联系新信息新技术不够",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(43)
            });
            //id=172
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "教学中根本不联系新信息新技术",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(43)
            });
            //id=173
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "开出有一定水平的选修课、讲座、院级公开课或指导兴趣小组有成效",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(44)
            });
            //id=174
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "开出选修课、讲座、系内公开课或指导兴趣小组活动",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(44)
            });
            //id=175
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "承担室内研究课、协助开出讲座或配合指导学生课外活动",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(44)
            });
            //id=176
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "无",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(44)
            });
            //id=177
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "出主意、提建议、协助室主任搞好教研活动",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(45)
            });
            //id=178
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "积极参加讨论",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(45)
            });
            //id=179
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "能按时参加活动",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(45)
            });
            //id=180
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "参加活动不正常",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(45)
            });
            #endregion
            #region 同行方面（领导、督导）
            //id=181
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "教学组织安排得当，气氛活跃，纪律良好",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(55)
            });
            //id=182
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "注意学生动态，教学有条不紊",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(55)
            });
            //id=183
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "忽视教学步骤，师生双边活动较差",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(55)
            });
            //id=184
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "只顾自己讲，不管学生情况",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(55)
            });
            //id=185
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "切合教学大纲要求与实际，内容组织科学严密",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(56)
            });
            //id=186
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "符合教学大纲要求，内容正确",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(56)
            });
            //id=187
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "基本达到教学大纲要求，内容偶有差错",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(56)
            });
            //id=188
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "降低教学标准，内容时有差错",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(56)
            });
            //id=189
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "语言精练，深入浅出，讲解准确",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(57)
            });
            //id=190
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解清晰，容易接受",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(57)
            });
            //id=191
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解基本准确，但不易接受",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(57)
            });
            //id=192
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "概念紊乱，时有差错",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(57)
            });
            //id=193
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "重点突出，讲清难点，举一反三",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(58)
            });
            //id=194
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "能把握重点、难点，但讲解不够明确",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(58)
            });
            //id=195
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "重点不明显，难点讲不透",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(58)
            });
            //id=196
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "重点一言而过，难点草率了事",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(58)
            });
            //id=197
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解方法新颖，举例生动，有吸引力",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(59)
            });
            //id=198
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解较熟练，语言通俗",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(59)
            });
            //id=199
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解平淡，语言单调",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(59)
            });
            //id=200
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "讲解生疏，远离课题，语言枯燥",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(59)
            });
            //id=201
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "教具使用合理，板书清晰，示教形象、直观",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(60)
            });
            //id=202
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "注意直观教学，板书条目明白、整洁",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(60)
            });
            //id=203
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "教具使用失当，板书布局较差",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(60)
            });
            //id=204
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "忽视直观教学，板书凌乱",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(60)
            });
            //id=205
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "运用各种方法，调动学生积极思维，注重能力培养",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(61)
            });
            //id=206
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "注意调动学生思维和能力培养，方法和效果欠佳",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(61)
            });
            //id=207
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "缺乏启发式方法和能力培养手段",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(61)
            });
            //id=208
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "照本宣科，不搞启发式教学",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(61)
            });
            //id=209
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "理论与实例、实验、实际密切结合",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(62)
            });
            //id=210
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "理论能结合实际进行教学",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(62)
            });
            //id=211
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "理论与实际结合不理想",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(62)
            });
            //id=212
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "理论与实际严重脱节",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(62)
            });
            //id=213
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "科学的处理教材，繁简增删适当，收事半功倍之效",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(63)
            });
            //id=214
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "对教材的处理，有助于学生理解和掌握内在联系",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(63)
            });
            //id=215
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "基本按照教材讲课，没有给学生什么新东西",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(63)
            });
            //id=216
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "对教材毫无处理，完全重复课本内容",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(63)
            });
            #endregion
            #region 教师个人方面
            //id=217
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "优秀",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(64)
            });
            //id=218
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "良好",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(64)
            });
            //id=219
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一般",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(64)
            });
            //id=220
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "较差",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(64)
            });
            //id=221
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "优秀",
                Score = 1,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(65)
            });
            //id=222
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "良好",
                Score = 0.85m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(65)
            });
            //id=223
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "一般",
                Score = 0.65m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(65)
            });
            //id=224
            Container.Instance.Resolve<INormOptionService>().Add(new NormOption()
            {
                OptionContent = "较差",
                Score = 0.45m,
                Norm = Container.Instance.Resolve<INormService>().GetEntity(65)
            });
            #endregion
        }
        #endregion
        */

    }
}