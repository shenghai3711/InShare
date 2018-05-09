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
    public class UserController : Controller
    {
        [Dependency]
        public IUserService UserService { get; set; }

        // GET: Manage/User
        public ActionResult List(int pageIndex = 1)
        {
            var userList = UserService.GetUserPagerList(8, pageIndex).Select(u => new UserInfo
            {
                Id = u.Id,
                FullName = u.FullName,
                UserName = u.UserName,
                CreateDateTime = string.Format("{0:R}", u.CreateDateTime)
            }).ToArray();
            return View(userList);
        }

        [HttpGet]
        public ActionResult AddNew()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNew(string userName, string fullName)
        {
            return View();
        }

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

        [HttpPost]
        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult BatchDelete()
        {
            return View();
        }

        public ActionResult Count()
        {
            return View();
        }

        public ActionResult DelList()
        {
            return View();
        }
    }
}