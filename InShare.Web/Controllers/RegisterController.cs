using InShare.Common;
using InShare.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace InShare.Web.Controllers
{
    public class RegisterController : Controller
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
        public ActionResult Index(string userName, string fullName, string passWord, string verifyCode, string ip, string cityName)
        {
            string cacheVerifyCode = TempData["verifyCode"].ToString();
            if (string.IsNullOrEmpty(verifyCode) || verifyCode != cacheVerifyCode)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "验证码错误，请重新填写" });
            }
            long userId = UserService.Add(userName, fullName, passWord);
            LogService.Add(userId, 1, string.Format("{0}在{1}注册账号成功", userName, cityName), ip);
            return Json(new AjaxResult { Status = "OK" });
        }
        /// <summary>
        /// 校验账号是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VerifyName(string userName)
        {
            if (UserService.GetUserByUserName(userName) == null)
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "此账号已注册" });
        }
        public ActionResult VerifyCode()
        {
            string checkCode = RandomHelper.CreateImageCode();
            TempData["verifyCode"] = checkCode;//将验证码随机数放入临时缓存
            return File(ImageHelper.CodeImage(checkCode), @"image/Gif");
        }
    }
}