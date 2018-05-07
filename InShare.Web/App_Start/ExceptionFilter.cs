using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InShare.Web.App_Start
{

    /// <summary>
    /// 异常处理Filter
    /// </summary>
    public class ExceptionFilter : IExceptionFilter
    {
        private static ILog log = LogManager.GetLogger(typeof(ExceptionFilter));

        /// <summary>
        /// 出现异常时执行
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnException(ExceptionContext filterContext)
        {
            //记录异常日志,方便以后修复问题
            log.Error("出现未处理异常(╯°口°)╯(┴—┴ \r\n", filterContext.Exception);
            ////重定向友好错误页面
            //filterContext.Result = new RedirectResult("~/error.html");
        }
    }
}