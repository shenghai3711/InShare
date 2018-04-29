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

        public int PageSize = 6;

        [HttpGet]
        public ActionResult Index(string shortCode)
        {
            if (string.IsNullOrEmpty(shortCode))
            {
                return Redirect("/Home/Index");
            }
            var post = PostService.GetPostInfo(shortCode);
            if (post == null)
            {
                return Redirect("/Home/Index");
            }
            ViewBag.Post = new PostInfo(post);
            return View();
        }

        #region 搜索

        [HttpGet]
        public ActionResult Search(string tagName)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Search(int tagId, int pageIndex = 1)
        {
            return Json(new AjaxResult { });
        }

        #endregion

        #region Like操作 未完成

        [HttpPost]
        public ActionResult Like(long postId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Unlike(long postId)
        {
            return View();
        }

        #endregion

        #region Comment操作

        [HttpPost]
        public ActionResult Comment(long postId, string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "评论内容为空" });
            }
            long userId = 0;
            if (!long.TryParse(Session["userId"].ToString(), out userId))
            {
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
                return Json(new AjaxResult { Status = "Error", ErrorMsg = "未登录用户" });
            }
            string url = Common.ImageHelper.UploadStream(Common.ImageHelper.ImgBase64ToStream(base64Data));
            if (string.IsNullOrEmpty(url))
            {
                return Json(new AjaxResult { Status = "Failed", ErrorMsg = "上传图片失败" });
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
                return Json(new AjaxResult { Status = "NoLogin", ErrorMsg = "未登录用户" });
            }
            var post = PostService.GetPostInfo(rePostId);
            if (post == null)
            {
                return Json(new AjaxResult { Status = "Failed", ErrorMsg = "请刷新后重试" });
            }
            PostService.Add(Convert.ToInt64(Session["userId"]), content, post.DisplayUrl, city);
            LogService.Add(Convert.ToInt64(Session["userId"]), 2, string.Format("{0}在{1}转发帖子成功", Session["userName"], city), ip);
            return Json(new AjaxResult { Status = "OK" });
        }

        #endregion
    }
}