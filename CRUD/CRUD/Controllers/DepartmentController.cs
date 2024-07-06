using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class DepartmentController : Controller
    {
        EmployeeEntities db = new EmployeeEntities();

        public ActionResult Index()
        {
            List<tblDepartment> departments = db.tblDepartments.ToList();
            return View(departments);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepartment(tblDepartment department)
        {
            if (ModelState.IsValid)
            {
                var data = db.tblDepartments.Add(department);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartment(int id, tblDepartment department)
        {
            if (ModelState.IsValid)
            {
                db.Entry(department).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(tblDepartment department, int? id)
        {
            department = db.tblDepartments.Find(id);
            db.tblDepartments.Remove(department);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        /*public ActionResult Department_Employee(int id)
        {
            var department = db.tblDepartments.FirstOrDefault(m => m.Id == id);

            ViewBag.tblDepartments = department;
            List<tblEmployee> employee = department.tblEmployees.ToList();
            return View();
        }*/
    }
}