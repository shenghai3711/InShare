using InShare.Common;
using InShare.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace InShare.Web.Areas.Manage.Controllers
{
    public class MainController : Controller
    {
        [Dependency]
        public IAdminUserService AdminUserService { get; set; }
        // GET: Manage/Main
        public ActionResult Index()
        {
            if (Session["AdminName"] == null)
            {
                Session.Clear();
                return Redirect("/Manage/Main/Login");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string passWord, string verifyCode)
        {
            string cacheVerifyCode = TempData["verifyManageCode"].ToString();
            if (verifyCode != cacheVerifyCode)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "验证码错误" });
            }
            if (AdminUserService.Login(userName, passWord))
            {
                Session["AdminName"] = userName;
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "用户名或密码错误" });
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return Redirect("/Manage/Main/Login");
        }

        public ActionResult Welcome()
        {
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult CreateVerifyCode()
        {
            string checkCode = RandomHelper.CreateImageCode();
            TempData["verifyManageCode"] = checkCode;//将验证码随机数放入临时缓存
            return File(ImageHelper.CodeImage(checkCode), @"image/Gif");
        }
    }
}