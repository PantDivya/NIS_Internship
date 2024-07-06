using CRUD.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class HomeController : Controller
    {
        EmployeeEntities db = new EmployeeEntities();

        /*[AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]*/
        public ActionResult Index()
        {
            List<tblEmployee> employee = db.tblEmployees.Include(x=>x.tblSalary).ToList();
            return View(employee);
        }
        public ActionResult CreateEmployee()
        {
           ViewBag.Salary = db.tblSalaries;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(tblEmployee employee)
        {
            if (ModelState.IsValid)
            {
                var data = db.tblEmployees.Add(employee);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult UpdateEmployee(int? id)
        {
            var salary = db.tblSalaries.Find(id);
            /*ViewBag.tblSalary.Salary = salary;*/
            var employee = db.tblEmployees.Find(id);
                if (employee != null)
                {
                    return View(employee);

                }
                return HttpNotFound();
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(int id, tblEmployee employee) 
        {
            if (ModelState.IsValid)
            {
                var oldEmployee = db.tblEmployees.Find(id);
                oldEmployee.Name = employee.Name;
                oldEmployee.Address = employee.Address;
                oldEmployee.Email = employee.Email;
                oldEmployee.Contact = employee.Contact;
                oldEmployee.tblSalary = employee.tblSalary;
                db.Entry(oldEmployee).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        
        public ActionResult DeleteEmployee(int? id)
        {
            var employee = db.tblEmployees.Find(id);
            if (employee != null)
            {
                return View(employee);

            }
            return HttpNotFound();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(tblEmployee employee, int? id)
        {
            var empSalary = db.tblSalaries.Find(id);
            employee = db.tblEmployees.Find(id);
            db.tblSalaries.Remove(empSalary);
            db.tblEmployees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
      
    }
}