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
        [Dependency]
        public IVerifyService VerifyService { get; set; }
        /// <summary>
        /// 帖子每页数量
        /// </summary>
        public int PageSize = 6;

        /// <summary>
        /// 获取当前登录账号编号
        /// </summary>
        public long AccountId
        {
            get
            {
                if (string.IsNullOrEmpty(SessionHelper.Get("userId")))
                {
                    return 0;
                }
                return Convert.ToInt64(SessionHelper.Get("userId"));
            }
        }

        #region 首页加载

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

        /// <summary>
        /// 用户详情页下拉加载数据
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Load(long userId, int pageIndex)
        {
            var returList = PostService.GetPostPagerList(userId, PageSize, pageIndex).Select(
                p => new PostInfo(p));
            return Json(new AjaxResult { Status = "OK", Data = returList });
        }

        #endregion

        #region 加载关注者或被关注者

        [HttpPost]
        public ActionResult LoadFollower(long userId, int pageIndex = 1)
        {
            long accountId = Convert.ToInt64(Session["userId"]);
            List<UserInfo> userList = new List<UserInfo>();
            var followerList = FollowService.GetFollowerPagerList(userId, 12, pageIndex);
            foreach (var followerId in followerList)
            {
                var user = UserService.GetUserById(followerId);
                userList.Add(new UserInfo
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    IsFollowing = FollowService.IsFollowing(accountId, followerId),
                    Biography = user.Biography.Length > 50 ? user.Biography.Substring(0, 50) + "..." : user.Biography,
                    ProfilePic = user.ProfilePic
                });
            }
            return Json(new AjaxResult { Status = "OK", Data = userList });
        }

        [HttpPost]
        public ActionResult LoadFollowing(long userId, int pageIndex = 1)
        {
            long accountId = Convert.ToInt64(Session["userId"]);
            List<UserInfo> userList = new List<UserInfo>();
            var followingList = FollowService.GetFollowingPagerList(userId, 12, pageIndex);
            foreach (var followingId in followingList)
            {
                var user = UserService.GetUserById(followingId);
                userList.Add(new UserInfo
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    UserName = user.UserName,
                    IsFollowing = FollowService.IsFollowing(accountId, followingId),
                    Biography = user.Biography.Length > 50 ? user.Biography.Substring(0, 50) + "..." : user.Biography,
                    ProfilePic = user.ProfilePic
                });
            }
            return Json(new AjaxResult { Status = "OK", Data = userList });
        }

        #endregion

        #region 修改资料

        [HttpGet]
        public ActionResult Edit()
        {
            if (AccountId == 0)
            {
                Redirect("/User/Login");
            }
            var user = UserService.GetUserById(AccountId);
            user.Profile.PhoneNum = user.Profile.PhoneNum ?? "";
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(string biography, string fullName, string email, string phoneNum)
        {
            if (AccountId == 0)
            {
                Redirect("/User/Login");
            }
            if (string.IsNullOrEmpty(fullName))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "fullname 不能为空" });
            }
            if (string.IsNullOrEmpty(email))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "email 不能为空" });
            }
            var user = UserService.GetUserById(AccountId);
            if (user != null)
            {
                if (UserService.Edit(AccountId, user.UserName, fullName, biography, user.IsPrivate, user.Profile.Gender, email, phoneNum))
                {
                    return Json(new AjaxResult { Status = "OK", Data = "/User/" + AccountId });
                }
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "修改失败" });
        }

        [HttpPost]
        public ActionResult UploadUserIcon(string base64Data)
        {
            string url = Common.ImageHelper.UploadStream(Common.ImageHelper.ImgBase64ToStream(base64Data));
            if (string.IsNullOrEmpty(url))
            {
                return Json(new AjaxResult { Status = "Failed", ErrorMsg = "上传图片失败" });
            }
            if (UserService.EditProfilePic(AccountId, url))
            {
                return Json(new AjaxResult { Status = "OK", Data = url });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "修改头像失败" });
        }

        #endregion

        #region 修改密码

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePassword(string oldPwd, string newPwd)
        {
            if (string.IsNullOrEmpty(oldPwd) || string.IsNullOrEmpty(newPwd))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "密码不能为空" });
            }
            if (oldPwd == newPwd)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "新密码不能与旧密码重复" });
            }
            try
            {
                if (UserService.UpdatePwd(AccountId, newPwd, oldPwd))
                {
                    return Json(new AjaxResult { Status = "OK" });
                }
            }
            catch (Exception ex)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = ex.Message });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "修改密码失败，请稍后重试" });
        }

        #endregion

        #region 用户日志 未完成

        [HttpGet]
        public ActionResult Log()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Log(int pageIndex)
        {
            return Json(new AjaxResult { });
        }

        #endregion

        #region Follow操作

        /// <summary>
        /// ajax post处理follo用户请求
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
                    LogService.Add(accountId, 3, string.Format("{0}成功关注{1}", accountId, userId));
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
                    LogService.Add(accountId, 4, string.Format("{0}成功取消关注{1}", accountId, userId));
                    return Json(new AjaxResult { Status = "OK" });
                }
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "Unfollow 失败" });
        }

        /// <summary>
        /// 点击关注或取消关注后重新加载数据
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult LoadUserInfo(long userId)
        {
            if (userId == 0)
            {
                return View();
            }
            var user = UserService.GetUserById(userId);
            if (user == null)
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
                    IsFollowing = FollowService.IsFollowing(Convert.ToInt64(Session["userId"]), userId)
                }
            });
        }
        #endregion

        #region 登陆

        /// <summary>
        /// 进入登陆页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// Ajax post登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string userName, string passWord, string ip)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord) || string.IsNullOrEmpty(ip))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "不能为空" });
            }
            var user = UserService.GetUserByUserName(userName);
            if (user != null)
            {
                string city = "重庆";//todo:根据ip获取当地位置
                if (UserService.CheckLogin(userName, passWord))
                {
                    SessionHelper.Add("userId", user.Id.ToString(), 60);
                    SessionHelper.Add("userName", user.UserName, 60);
                    SessionHelper.Add("profilePic", user.ProfilePic, 60);
                    SessionHelper.Add("IP", ip, 60);
                    SessionHelper.Add("CityName", city, 60);
                    LogService.Add(user.Id, 0, string.Format("在{0}登陆成功", city), ip);//日志记录用户登录
                    return Json(new AjaxResult { Status = "OK", Data = user.Id });
                }
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "用户名或密码错误" });
        }

        #endregion

        #region 注册

        /// <summary>
        /// 加载注册页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// ajax post 注册
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="fullName"></param>
        /// <param name="passWord"></param>
        /// <param name="verifyCode"></param>
        /// <param name="ip"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Register(string userName, string fullName, string passWord, string verifyCode, string ip)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(passWord) || string.IsNullOrEmpty(verifyCode) || string.IsNullOrEmpty(ip))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "信息不能为空" });
            }
            string cacheVerifyCode = TempData["verifyCode"].ToString();
            if (string.IsNullOrEmpty(verifyCode) || verifyCode != cacheVerifyCode)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "验证码错误，请重新填写" });
            }
            string city = "重庆";//todo:根据ip获取当地位置
            long userId = UserService.Add(userName, fullName, passWord);
            LogService.Add(userId, 1, string.Format("在{0}注册账号成功", city), ip);
            return Json(new AjaxResult { Status = "OK" });
        }

        #endregion

        #region 退出

        public ActionResult Logout()
        {
            if (!string.IsNullOrEmpty(SessionHelper.Get("userId")))
            {
                LogService.Add(Convert.ToInt64(SessionHelper.Get("userId")), 1, string.Format("在{0}退出登录", SessionHelper.Get("CityName")), SessionHelper.Get("IP"));
            }
            Session.Clear();
            return Redirect("/User/Login");
        }

        #endregion

        #region 忘记密码
        /// <summary>
        /// 忘记密码页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult ForgotPwd()
        {
            return View();
        }

        /// <summary>
        /// ajax post 忘记密码发送邮件
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ForgotPwd(string email, string userName)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(userName))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "email或用户名不能为空" });
            }
            if (!RegexHelper.IsMatch(email, @"\w[-\w.+]*@([A-Za-z0-9][-A-Za-z0-9]+\.)+[A-Za-z]{2,14}"))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "email格式不正确" });
            }
            var user = UserService.GetUserByUserName(userName);
            if (user == null)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "用户不存在" });
            }
            var verify = VerifyService.Add(user.Id);
            string content = string.Format("http://localhost:31726/User/ResetPassword?userId={0}&verifyCode={1}", user.Id, verify.VerifyCode);
            bool b = EmailHelper.SendMail(new Email
            {
                DisplayName = "InShare运营团队",
                Subject = "找回账号密码",
                Body = content,//这里应该是链接，点击后请求修改密码页面
            }, email);
            if (b)
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "请稍后再试" });
        }

        #endregion

        #region 重置密码

        [HttpGet]
        public ActionResult ResetPassword(long? userId, string verifyCode)
        {
            if (userId == null || string.IsNullOrEmpty(verifyCode))
            {
                return Redirect("/SiteStatus/NotFound");
            }
            ViewBag.UserId = userId;
            ViewBag.Code = verifyCode;
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(long userId, string verifyCode, string userName, string pwd)
        {
            if (userId == 0 || string.IsNullOrEmpty(verifyCode) || string.IsNullOrEmpty(pwd))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "信息不完整" });
            }
            //查询验证码是否有效
            var verify = VerifyService.GetVerify(userId);
            if (verify == null)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "验证码无效" });
            }
            //重置密码
            var user = UserService.GetUserById(userId);
            if (user.UserName != userName)
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "用户名错误" });
            }
            if (UserService.UpdatePwd(userId, pwd, user.Profile.Password))
            {
                //修改验证码状态
                VerifyService.UpdateValid(userId);
                LogService.Add(userId, 7, "重置密码成功");
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "修改密码失败，请稍后重试" });
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