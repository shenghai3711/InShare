using InShare.Common;
using InShare.IService;
using InShare.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Unity.Attributes;

namespace InShare.Web.Areas.Manage.Controllers
{
    public class PostController : Controller
    {
        [Dependency]
        public IPostService PostService { get; set; }

        // GET: Manage/User
        public ActionResult List(int pageIndex = 1)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount = PostService.GetPostCount();
            var list = PostService.GetPostPagerList(8, pageIndex).Select(p => new PostInfo(p)).ToArray();
            return View(list);
        }

        #region 修改

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var post = PostService.GetPostInfo(id);
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(long id, string caption, string displayUrl, string location)
        {
            var post = PostService.GetPostInfo(id);
            if (PostService.Edit(id, caption, displayUrl, location))
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "修改失败" });
        }

        #endregion

        #region 删除

        /// <summary>
        /// 删除单个数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(long id)
        {
            if (PostService.MarkDel(id))
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "删除失败" });
        }

        /// <summary>
        /// 批量删除数据
        /// </summary>
        /// <param name="selectedIds"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult BatchDelete(long[] selectedIds)
        {
            if (PostService.MarkBatchDel(selectedIds))
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "删除失败" });
        }

        #endregion

        public ActionResult Count()
        {
            return View();
        }

        /// <summary>
        /// 回收站
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <returns></returns>
        public ActionResult DelList(int pageIndex = 1)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount = PostService.GetPostCount(flag: true);
            var list = PostService.GetPostPagerList(8, pageIndex, true).Select(p => new PostInfo(p)).ToArray();
            return View(list);
        }

        #region 恢复数据

        /// <summary>
        /// 批量恢复数据
        /// </summary>
        /// <param name="selectedIds"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReBatchDelete(long[] selectedIds)
        {
            if (PostService.ReMarkBatch(selectedIds))
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "恢复失败" });
        }

        /// <summary>
        /// 恢复单个
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ReDelete(long id)
        {
            if (PostService.ReMark(id))
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "恢复失败" });
        }

        #endregion
    }
}