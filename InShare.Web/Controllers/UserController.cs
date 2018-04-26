using InShare.Common;
using InShare.IService;
using InShare.Model;
using InShare.Web.Models;
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
        [Dependency]
        public IFollowService FollowService { get; set; }
        [Dependency]
        public IPostService PostService { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize = 6;

        /// <summary>
        /// 加载用户详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index(long? id)
        {
            if (id == null)
            {
                return View();
            }
            ViewBag.FollowerCount = FollowService.GetFollowerCount(id.Value);
            ViewBag.FollowingCount = FollowService.GetFollowingCount(id.Value);
            if (Convert.ToInt64(Session["userId"]) != id.Value)
            {
                ViewBag.Following = FollowService.IsFollowing(Convert.ToInt64(Session["userId"]), id.Value);
            }
            ViewBag.PostCount = PostService.GetPostCount(id.Value);
            ViewBag.PostList = PostService.GetPostPagerList(id.Value, PageSize, 1).Select(
                p => new PostInfo(p));
            return View(UserService.GetUserById(id.Value));
        }

        [HttpPost]
        public ActionResult Load(long userId, int pageIndex)
        {
            var returList = PostService.GetPostPagerList(userId, PageSize, pageIndex).Select(
                p => new PostInfo(p));
            return Json(new AjaxResult { Status = "OK", Data = returList });
        }

        [HttpPost]
        public ActionResult LoadUserInfo(long userId)
        {
            if (userId == 0)
            {
                return View();
            }
            var user = UserService.GetUserById(userId);
            if (user==null)
            {
                return View();
            }
            return Json(new AjaxResult
            {
                Status = "OK",
                Data = new UserInfo
                {
                    Id = user.Id,
                    Biography = user.Biography,
                    FollowerCount = FollowService.GetFollowerCount(userId),
                    FollowingCount = FollowService.GetFollowingCount(userId),
                    FullName = user.FullName,
                    PostCount = PostService.GetPostCount(userId),
                    ProfilePic = user.ProfilePic,
                    UserName = user.UserName,
                    IsFollowing= FollowService.IsFollowing(Convert.ToInt64(Session["userId"]), userId)
                }
            });
        }

        #region 修改资料 未完成

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

        #region 修改密码 未完成

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

        #region Follow操作

        [HttpPost]
        public ActionResult Follow(long userId)
        {
            long accountId = 0;
            if (Session["userId"] == null || !long.TryParse(Session["userId"].ToString(), out accountId))
            {
                Session.Clear();
                return Redirect("/User/Login");
            }
            if (userId != accountId || userId == 0)
            {
                if (FollowService.Follow(accountId, userId))
                {
                    return Json(new AjaxResult { Status = "OK" });
                }
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "Follow 失败" });
        }

        [HttpPost]
        public ActionResult Unfollow(long userId)
        {
            long accountId = 0;
            if (Session["userId"] == null || !long.TryParse(Session["userId"].ToString(), out accountId))
            {
                Session.Clear();
                return Redirect("/User/Login");
            }
            if (userId != accountId || userId == 0)
            {
                if (FollowService.Unfollow(accountId, userId))
                {
                    return Json(new AjaxResult { Status = "OK" });
                }
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "Unfollow 失败" });
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
                Session["userName"] = user.UserName;
                Session["profilePic"] = user.ProfilePic;
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

        #region 验证

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

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        public ActionResult VerifyCode()
        {
            string checkCode = RandomHelper.CreateImageCode();
            TempData["verifyCode"] = checkCode;//将验证码随机数放入临时缓存
            return File(ImageHelper.CodeImage(checkCode), @"image/Gif");
        }

        #endregion

    }
}