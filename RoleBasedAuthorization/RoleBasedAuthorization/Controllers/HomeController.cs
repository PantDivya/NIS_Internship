using RoleBasedAuthorization.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoleBasedAuthorization.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = MyConstant.RoleUser)]
        public ActionResult Employee()
        {           
            return View();
        }
        [Authorize(Roles = MyConstant.RoleAdmin)]
        public ActionResult EditEmployee()
        {
            return View();
        }
    }
}