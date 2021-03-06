﻿using InShare.Common;
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
    public class UserController : Controller
    {
        [Dependency]
        public IUserService UserService { get; set; }

        // GET: Manage/User
        public ActionResult List(int pageIndex = 1)
        {
            ViewBag.pageIndex = pageIndex;
            ViewBag.totalCount = UserService.GetAllUserCount();
            var userList = UserService.GetUserPagerList(8, pageIndex).Select(u => new UserInfo
            {
                Id = u.Id,
                FullName = u.FullName,
                UserName = u.UserName,
                CreateDateTime = string.Format("{0:R}", u.CreateDateTime)
            }).ToArray();
            return View(userList);
        }

        #region 修改

        [HttpGet]
        public ActionResult Edit(long id)
        {
            var user = UserService.GetUserById(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(long id, string userName, string fullName, string email)
        {
            var user = UserService.GetUserById(id);
            if (UserService.Edit(id, userName, fullName, user.Biography, user.IsPrivate, user.Profile.Gender, email, user.Profile.PhoneNum))
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
            if (UserService.MarkDel(id))
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
            if (UserService.MarkBatchDel(selectedIds))
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
            ViewBag.totalCount = UserService.GetAllUserCount(flag: true);
            var userList = UserService.GetUserPagerList(8, pageIndex, true).Select(u => new UserInfo
            {
                Id = u.Id,
                FullName = u.FullName,
                UserName = u.UserName,
                CreateDateTime = string.Format("{0:R}", u.CreateDateTime)
            }).ToArray();
            return View(userList);
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
            if (UserService.ReMarkBatch(selectedIds))
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
            if (UserService.ReMark(id))
            {
                return Json(new AjaxResult { Status = "OK" });
            }
            return Json(new AjaxResult { Status = "Error", ErrorMsg = "恢复失败" });
        }

        #endregion

    }
}