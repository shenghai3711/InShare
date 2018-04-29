using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InShare.Web.Controllers
{
    public class SiteStatusController : Controller
    {
        // GET: SiteStatus
        public ActionResult Index()
        {
            return Redirect("/SiteStatus/NotFound");
        }
        
        public ActionResult NotFound()
        {
            return View();
        }
    }
}