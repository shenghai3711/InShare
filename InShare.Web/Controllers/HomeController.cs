using InShare.Common;
using InShare.IService;
using InShare.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace InShare.Web.Controllers
{
    public class HomeController : Controller
    {
        [Dependency]
        public IPostService PostService { get; set; }
        [Dependency]
        public IUserService UserService { get; set; }
        [Dependency]
        public IFollowService FollowService { get; set; }

        public int PageSize = 6;
        // GET: Home
        public ActionResult Index()
        {
            long userId = 0;
            if (Session["userId"] == null || !long.TryParse(Session["userId"].ToString(), out userId))
            {
                Session.Clear();
                return Redirect("/User/Login");
            }
            ViewBag.IsExistPost = true;
            var postList = PostService.GetHomePager(userId, PageSize, 1);
            ViewBag.PostList = postList.Select(p => new PostInfo(p)).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Load(int pageIndex)
        {
            long userId = 0;
            if (Session["userId"] == null || !long.TryParse(Session["userId"].ToString(), out userId))
            {
                Session.Clear();
                return Redirect("/User/Login");
            }
            var postList = PostService.GetHomePager(userId, PageSize, pageIndex).Select(p => new PostInfo(p)).ToList();
            return Json(new AjaxResult { Status = "OK", Data = postList });
        }

        /// <summary>
        /// 测试错误页面
        /// </summary>
        /// <returns></returns>
        public ActionResult TestError()
        {
            throw new Exception("测试错误页面");
        }

        public ActionResult About()
        {
            return View();
        }
    }
}