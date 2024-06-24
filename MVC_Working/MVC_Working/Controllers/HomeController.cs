using MVC_Working.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC_Working.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Employee employee = GetEmployee();
            return View(employee);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public Employee GetEmployee()
        {
            Employee employee = new Employee();
            employee.ID = 1;
            employee.Name = "Divya";
            employee.Address = "Kanchanpur";
            employee.Email = "divya@gmail.com";
            employee.Phone = "1234567890";
            return employee;
        }
    }
}