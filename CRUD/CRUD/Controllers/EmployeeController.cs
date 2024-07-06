using CRUD.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeEntities db = new EmployeeEntities();

        public ActionResult Index()
        {
            var department = db.tblDepartments.ToList();
            ViewBag.tblDepartments = new SelectList(department, "Id", "DepartmentName");
            List<tblEmployee> employee = db.tblEmployees.ToList();
            return View(employee);
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
        public ActionResult UpdateEmployee(int id)
        {
            tblEmployee employee = db.tblEmployees.Find(id);

            var department = db.tblDepartments.ToList();
            ViewBag.tblDepartments = new SelectList(department, "Id", "DepartmentName", employee.DepartmentId);
            return View("Edit", employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditEmployee(int id,tblEmployee employee)
        {
            if (ModelState.IsValid)
            {
            var oldEmployee = db.tblEmployees.Find(id);
            oldEmployee.Name = employee.Name;
            oldEmployee.Address = employee.Address;
            oldEmployee.Email = employee.Email;
            oldEmployee.Contact = employee.Contact;
            oldEmployee.DepartmentId = employee.DepartmentId;
            db.Entry(oldEmployee).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult Create()
        {
            var department = db.tblDepartments.ToList();

            ViewBag.tblDepartments = new SelectList(department, "Id", "DepartmentName");
            return View();
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
            employee = db.tblEmployees.Find(id);
            db.tblEmployees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
