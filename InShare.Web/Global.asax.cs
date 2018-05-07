using Microsoft.Practices.Unity.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace InShare.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            log4net.Config.XmlConfigurator.Configure();//启动log4net
            GlobalFilters.Filters.Add(new App_Start.ExceptionFilter());//启动异常Filter
        }

    }
}
