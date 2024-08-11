using OneToManyToMany.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OneToManyToMany.Controllers
{
    public class HomeController : Controller
    {
        MainEntities db = new MainEntities();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(List<DepartmentViewModel> modelList)
        { 
            if (ModelState.IsValid)
            {
                foreach (var model in modelList)
                {
                    var department = db.tbl_Department.FirstOrDefault(x => x.DepartmentName == model.Name);

                    if (department == null)
                    {
                        department = new tbl_Department
                        {
                            DepartmentName = model.Name,
                            tbl_Employee = model.Employees.Select(e => new tbl_Employee
                            {
                                Name = e.Name,
                                tbl_Project = e.Projects.Select(p => new tbl_Project
                                {
                                    ProjectName = p.ProjectName
                                }).ToList()
                            }).ToList()
                        };
                        db.tbl_Department.Add(department);
                    }
                    else
                    {
                        // Update the existing department's employees
                        department.tbl_Employee = model.Employees.Select(e => new tbl_Employee
                        {
                            Name = e.Name,
                            tbl_Project = e.Projects.Select(p => new tbl_Project
                            {
                                ProjectName = p.ProjectName
                            }).ToList()
                        }).ToList();

                        db.Entry(department).State = System.Data.Entity.EntityState.Modified;
                    }
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Error");
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}