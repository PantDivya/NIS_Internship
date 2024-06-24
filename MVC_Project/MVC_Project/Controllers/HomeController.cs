using MVC_Project.Enums;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        EmployeeEntities db = new EmployeeEntities();
        public ActionResult Index()
        {
            List<Employee> employee = db.Employees.ToList();
            return View(employee);
        }
        public IEnumerable<SelectListItem> GetCity()
        {
            var city = new SelectList(Enum.GetValues(typeof(Emp.City)).OfType<Enum>()
              .Select(x =>
                    new SelectListItem
                    {
                        Text = Enum.GetName(typeof(Emp.City), x),
                        Value = (Convert.ToInt32(x)).ToString()
                    }), "Value", "Text");

            return city;
        }

        public ActionResult CreateEmployee()
        {
            ViewBag.Enum = GetCity();
            return View();
        }


        [HttpPost]
       public ActionResult Create(Employee employee)
        {

            var data = db.Employees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}