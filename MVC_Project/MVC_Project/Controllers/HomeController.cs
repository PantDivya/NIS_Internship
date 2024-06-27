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
        MainEntities db = new MainEntities();

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

            ViewBag.tblHobby = db.tblHobbies.ToList();
            return View();

        }
        [HttpPost]

        public ActionResult New(tblEmployee employee)
        {
            var data = db.tblEmployees.Add(employee);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult CreateEmployee()
        {
            ViewBag.Enum = GetCity();
            return View();
        }


        [HttpPost]

        public ActionResult Create(Employee employee, List<UploadFile> uploadFile)
        {
            if (ModelState.IsValid)
            {
                /*WebImage photo = null;
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
                employee.Image = imagePath;*/
                
                var data = db.Employees.Add(employee);
                db.SaveChanges();
                //UploadImage(employee, uploadFile);
                return RedirectToAction("Index");
            }
            else
            {
                return View("CreateEmployee", employee);
            }

        }

        
        public void UploadImage(Employee employee, UploadFile uploadFile)
        {
            var photo = new tblPhoto();
            if(uploadFile.File != null) 
            {
                for (int i = 0; i < uploadFile.File.Count; i++)
                {
                    var file = uploadFile.File[i];

                    var newFileName = Guid.NewGuid().ToString() + "_" + Path.GetFileName(file.FileName);
                    var imagePath = @"images\" + newFileName;
                    var fullPath = Server.MapPath(@"~\Content\") + imagePath;

                    file.SaveAs(fullPath);
                    photo.Location = imagePath;
                    photo.Name = newFileName;
                    photo.EmployeeId = employee.Id;
                    var data = db.tblPhotoes.Add(photo);
                    db.SaveChanges();
                }
            }
        }
    }
}