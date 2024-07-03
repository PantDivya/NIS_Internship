using CRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUD.Controllers
{
    public class ProductController : Controller
    {
        EmployeeEntities db = new EmployeeEntities();
        // GET: Product
        public ActionResult Index()
        {
            var category = "Books";
            var category1 = "Toy";
            var name = "xyz";
            var id = 10;
            //List<Product> products = db.Products.Where(m=>m.Category == category).ToList(); 
            //List<Product> products = db.Products.OrderBy(m => m.Price).ToList();
            //var products = db.Products.Select(m => m.Name).ToList();
            //List<Product> products = db.Products.Where(m=>m.Price > 100).ToList();
            //List<Product> products = db.Products.Where(m => m.Stock < 50).ToList();

            /* List<Product> products = db.Products.Where(m => m.Category == category).ToList();
             var count = products.Count();*/

            //var products = db.Products.Average(m=>m.Price);

            //List<Product> products = db.Products.OrderByDescending(p => p.Price).Take(5).ToList();

            Product products = db.Products.Where(m => m.Name == name).FirstOrDefault();

            //Product products = db.Products.Where(m => m.ProductId == id).First();

           //var isProductPresent = db.Products.Any(m=>m.Category == category1);


            return View(products);
        }
    }
}