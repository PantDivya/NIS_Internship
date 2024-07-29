using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnArea.Areas.Teachers.Controllers
{
    public class IndexController : Controller
    {
        // GET: Teachers/Index
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TeacherList()
        {
            return View();
        }
    }
}