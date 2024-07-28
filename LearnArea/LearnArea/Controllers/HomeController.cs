using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnArea.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Students() 
        {
            return RedirectToAction("Index", "Index", new { area = "Students" });
        }
        public ActionResult Teachers()
        {
            return RedirectToAction("Index", "Faculty", new { area = "Teachers" });
        }
    }
}