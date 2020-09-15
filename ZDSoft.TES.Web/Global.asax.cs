using Castle.ActiveRecord;
using Castle.ActiveRecord.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using ZDSoft.TES.Domain;

namespace ZDSoft.TES.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if (!ActiveRecordStarter.IsInitialized)
            {
                //如果ActiveRecordStarter框架没有初始化
                IConfigurationSource source = System.Configuration.ConfigurationManager.GetSection("activerecord") as IConfigurationSource;
               // ActiveRecordStarter.Initialize(typeof(CourseInfo).Assembly, source);
            }
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
