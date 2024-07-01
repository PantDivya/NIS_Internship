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

                var employee = db.tblEmployees.Find(id);
                if (employee != null)
                {
                    return View(employee);

                }
                return HttpNotFound();
            
        }
        [HttpPost]
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
        public ActionResult Delete(tblEmployee employee, int? id)
        {
            db.Entry(employee).State = EntityState.Deleted;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
    }
}