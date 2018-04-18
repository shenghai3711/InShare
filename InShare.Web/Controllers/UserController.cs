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
    public class UserController : Controller
    {
        [Dependency]
        public ILogService LogService { get; set; }
        [Dependency]
        public IUserService UserService { get; set; }

        /// <summary>
        /// 加载用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(long? id)
        {
            return View();
        }

        #region 修改资料

        [HttpGet]
        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(long id, string userName, string fullName)
        {
            return View();
        }

        #endregion

        #region 修改密码

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(long? id, string oldPwd, string newPwd)
        {
            return View();
        }

        #endregion

        #region 登陆

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string passWord, string ip, string city)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord) || string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(city))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "不能为空" });
            }
            var user = UserService.GetUserByUserName(userName);
            if (user == null)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "用户不存在" });
            }
            if (UserService.CheckLogin(userName, passWord))
            {
                Session["userId"] = user.Id;
                LogService.Add(user.Id, 0, string.Format("{0}在{1}登陆成功", user.FullName, city), ip);//日志记录用户登录
                return Json(new AjaxResult { Status = "OK", Data = user.Id });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "用户不存在" });
        }

        #endregion

        #region 注册

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(string userName, string fullName, string passWord, string verifyCode, string ip, string cityName)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(passWord) || string.IsNullOrEmpty(verifyCode) || string.IsNullOrEmpty(ip) || string.IsNullOrEmpty(cityName))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "信息不能为空" });
            }
            string cacheVerifyCode = TempData["verifyCode"].ToString();
            if (string.IsNullOrEmpty(verifyCode) || verifyCode != cacheVerifyCode)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "验证码错误，请重新填写" });
            }
            long userId = UserService.Add(userName, fullName, passWord);
            LogService.Add(userId, 1, string.Format("{0}在{1}注册账号成功", userName, cityName), ip);
            return Json(new AjaxResult { Status = "OK" });
        }

        #endregion

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