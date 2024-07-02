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
            //EmployeeViewModel viewModel = new EmployeeViewModel();

            List<tblEmployee> employee = db.tblEmployees.ToList();

          /*viewModel.EmployeeList = employee;
            if(id!= null)
            {
                tblEmployee employee1 = db.tblEmployees.Find(id);
                viewModel.Employee = employee1;
                EditEmployee(viewModel.Employee);
            }*/


            //return View(viewModel);


            return View(employee);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddEmployee(tblEmployee employee)
        {
            //string token = Request.Form["__RequestVerificationToken"];

            if (ModelState.IsValid)
            {
                var data = db.tblEmployees.Add(employee);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public ActionResult UpdateEmployee(int? id)
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
        public ActionResult EditEmployee(int id, tblEmployee employee) 
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        public ActionResult CreateEmployee()
        {
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
      public ActionResult hello()
        {
            IList<tblEmployee> employeeList = new List<tblEmployee>() {
        new tblEmployee() { Id = 1, Name = "John", Address = "Balkumari"} ,
        new tblEmployee() { Id = 2, Name = "Moin",  Address = "Imadol" } ,
        new tblEmployee() { Id = 3, Name = "Bill",  Address = "hello" } ,
        new tblEmployee() { Id = 4, Name = "Ram" , Address = "how are"} ,
        new tblEmployee() { Id = 5, Name = "Ram" , Address = "Khana khake jana" }
    };

            // LINQ Query Syntax 
            var emp = employeeList.Where(x => x.Address == "hello" && x.Name =="Bill").ToList<tblEmployee>();
            return View(emp);
        } 
    }
}