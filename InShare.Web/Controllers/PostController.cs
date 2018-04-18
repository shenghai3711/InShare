using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InShare.Web.Controllers
{
    public class PostController : Controller
    {
        public ActionResult Index(string shortCode)
        {
            return View();
        }
        
    }
}