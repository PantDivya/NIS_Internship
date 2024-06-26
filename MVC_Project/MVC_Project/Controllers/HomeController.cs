using MVC_Project.Enums;
using MVC_Project.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MVC_Project.Controllers
{
    public class HomeController : Controller
    {
        EmployeeEntities db = new EmployeeEntities();
        Employees employees = new Employees();

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

        public ActionResult NewEmployee()
        {
            ViewBag.Enum = GetCity();

            ViewBag.tblHobby = employees.tblHobbies.ToList();
            return View();

        }
        [HttpPost]

        public ActionResult New(tblEmployee employee)
        {
            var data = employees.tblEmployees.Add(employee);
            employees.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult CreateEmployee()
        {
            ViewBag.Enum = GetCity();
            return View();
        }


        [HttpPost]

        public ActionResult Create(Employee employee)
        {

            if (ModelState.IsValid)
            {
                WebImage photo = null;
                var newFileName = "";
                var imagePath = "";

                photo = WebImage.GetImageFromRequest();
                if (photo != null)
                {
                    newFileName = Guid.NewGuid().ToString() + "_" +
                        Path.GetFileName(photo.FileName);
                    imagePath = @"images\" + newFileName;

                    photo.Save(@"~\Content\" + imagePath);
                }
                employee.Image = imagePath;
                var data = db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("CreateEmployee",employee);
            }
            
        }

        [HttpPost]
        public ActionResult UploadImage()
        {
           
            
            return RedirectToAction("Index");
        }
    }
}