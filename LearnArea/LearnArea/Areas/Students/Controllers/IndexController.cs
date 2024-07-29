using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnArea.Areas.Students.Controllers
{
    public class IndexController : Controller
    {
        // GET: Students/Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult StudentList()
        {
            return View();
        }
    }
}