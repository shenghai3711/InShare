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
    public class TagController : Controller
    {
        [Dependency]
        public ITagService TagService { get; set; }

        // GET: Manage/User
        public ActionResult List(int pageIndex = 1)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount = TagService.GetTagCount();
            var list = TagService.GetTagPagerList(8, pageIndex).ToArray();
            return View(list);
        }

        [HttpGet]
        public ActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNew(string name)
        {
            if (TagService.Add(name))
                return Json(new AjaxResult { Status = "OK" });
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "添加失败" });
        }

        #region 修改

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var post = TagService.GetTagById(id);
            return View(post);
        }

        [HttpPost]
        public ActionResult Edit(long id, string name)
        {
            var post = TagService.GetTagById(id);
            if (TagService.Edit(id, name))
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
            if (TagService.MarkDel(id))
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
            if (TagService.MarkBatchDel(selectedIds))
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
            ViewBag.totalCount = TagService.GetTagCount(flag: true);
            var list = TagService.GetTagPagerList(8, pageIndex, true).ToArray();
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
            if (TagService.ReMarkBatch(selectedIds))
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
            if (TagService.ReMark(id))
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "恢复失败" });
        }

        #endregion
    }
}