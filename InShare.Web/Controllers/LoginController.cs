using InShare.Common;
using InShare.IService;
using InShare.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace InShare.Web.Controllers
{
    public class LoginController : Controller
    {
        [Dependency]
        public ILogService LogService { get; set; }
        [Dependency]
        public IUserService UserService { get; set; }
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string userName, string passWord, string ip, string city)
        {
            var user = UserService.GetUserByUserName(userName);
            if (user == null)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "用户不存在" });
            }
            if (UserService.CheckLogin(userName, passWord))
            {
                Session["userId"] = user.Id;
                LogService.Add(user.Id, 0, string.Format("{0}在{1}登陆成功", user.FullName, city), ip);//日志记录用户登录
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "用户不存在" });
        }
    }
}