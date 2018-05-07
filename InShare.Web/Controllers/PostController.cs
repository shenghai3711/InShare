using InShare.Common;
using InShare.IService;
using InShare.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace InShare.Web.Controllers
{
    public class PostController : Controller
    {
        [Dependency]
        public ILogService LogService { get; set; }
        [Dependency]
        public IPostService PostService { get; set; }
        [Dependency]
        public ICommentService CommentService { get; set; }
        [Dependency]
        public ITagService TagService { get; set; }
        [Dependency]
        public ILikeService LikeService { get; set; }

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

        [HttpGet]
        public ActionResult Index(string shortCode)
        {
            //throw new Exception("测试404页面");
            if (string.IsNullOrEmpty(shortCode))
            {
                return Redirect("/Home/Index");
            }
            var post = PostService.GetPostInfo(shortCode);
            if (post == null)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.IsLiked = LikeService.IsLiked(AccountId, post.Id);
            ViewBag.Post = new PostInfo(post);
            return View();
        }

        #region 搜索

        [HttpGet]
        public ActionResult Search(string keyword)
        {
            keyword = keyword ?? Request.RequestContext.RouteData.Values["id"].ToString();
            bool isAjax = Request.IsAjaxRequest();
            if (string.IsNullOrEmpty(keyword))
            {
                if (isAjax)
                {
                    return Json(new AjaxResult { Status = "Error", ErrorMsg = "关键词不能为空" }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return View();
                }
            }
            var tag = TagService.GetTagByName(keyword);
            if (isAjax)
            {
                if (tag == null)
                {
                    return Json(new AjaxResult { Status = "Failed", ErrorMsg = "没有这个标签，赶快去发表一个包含这个标签的帖子吧" }, JsonRequestBehavior.AllowGet);
                }
                return Json(new AjaxResult { Status = "OK" }, JsonRequestBehavior.AllowGet);
            }
            var post = PostService.GetSearchPager(tag.Id, PageSize, 1);
            ViewBag.TagName = tag.Name;
            ViewBag.PostCount = PostService.GetPostCountByTag(tag.Id);
            ViewBag.TagId = tag.Id;
            ViewBag.UserCount = PostService.GetUserCountByTag(tag.Id);
            ViewBag.PostList = post.Select(p => new PostInfo(p));
            return View();
        }

        [HttpPost]
        public ActionResult Search(int tagId, int pageIndex = 1)
        {
            var postList = PostService.GetSearchPager(tagId, PageSize, pageIndex).Select(p => new PostInfo(p));
            return Json(new AjaxResult { Status = "OK", Data = postList });
        }

        #endregion

        #region Like操作

        [HttpPost]
        public ActionResult Like(long postId)
        {
            if (LikeService.Like(AccountId, postId))
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "Like Failed" });
        }

        [HttpPost]
        public ActionResult Unlike(long postId)
        {
            if (LikeService.UnLike(AccountId, postId))
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "UnLike Failed" });
        }

        #endregion

        #region Comment操作

        [HttpPost]
        public ActionResult Comment(long postId, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "The content of the comment is empty" });
            }
            long userId = 0;
            if (!long.TryParse(Session["userId"].ToString(), out userId))
            {
                Session.Clear();
                return Redirect("/User/Login");
            }
            CommentService.Add(userId, postId, content);
            return Json(new AjaxResult { Status = "OK" });
        }

        [HttpPost]
        public ActionResult LoadComment(long postId, int pageIndex = 1)
        {
            var commentList = CommentService.GetCommentPagerList(postId, PageSize, pageIndex).Select(c => new CommentInfo(c)).ToList();
            return Json(new AjaxResult { Status = "OK", Data = commentList });
        }

        #endregion

        #region 发帖

        [HttpGet]
        public ActionResult Posting()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Posting(string content, string base64Data, string ip, string city)
        {
            if (Session["userId"] == null)
            {
                Session.Clear();
                //return Json(new AjaxResult { Status = "Error", ErrorMsg = "未登录用户" });
                return Redirect("/User/Login");
            }
            //上传到文件服务器（七牛云）
            string url = Common.ImageHelper.UploadStream(Common.ImageHelper.ImgBase64ToStream(base64Data));
            if (string.IsNullOrEmpty(url))
            {
                return Json(new AjaxResult { Status = "Failed", ErrorMsg = "Failed to upload picture" });
            }
            PostService.Add(Convert.ToInt64(Session["userId"]), content, url, city);
            LogService.Add(Convert.ToInt64(Session["userId"]), 2, string.Format("{0}在{1}发帖成功", Session["userName"], city), ip);
            return Json(new AjaxResult { Status = "OK" });
        }

        #endregion

        #region 转发

        [HttpPost]
        public ActionResult RePost(long rePostId, string content, string ip, string city)
        {
            if (Session["userId"] == null)
            {
                return Json(new AjaxResult { Status = "NoLogin", ErrorMsg = "Not logged in user" });
            }
            var post = PostService.GetPostInfo(rePostId);
            if (post == null)
            {
                return Json(new AjaxResult { Status = "Failed", ErrorMsg = "Please refresh and try again" });
            }
            PostService.Add(Convert.ToInt64(Session["userId"]), content, post.DisplayUrl, city);
            LogService.Add(Convert.ToInt64(Session["userId"]), 2, string.Format("{0}在{1}转发帖子成功", Session["userName"], city), ip);
            return Json(new AjaxResult { Status = "OK" });
        }

        #endregion
    }
}