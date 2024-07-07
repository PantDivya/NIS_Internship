using CRUDAjax.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CRUDAjax.Controllers
{
    public class HomeController : Controller
    {
        ProductEntity db = new ProductEntity();
        public ActionResult Index()
        {
            var data = db.Products.ToList();
            return View(data);
        }

       public ActionResult AddProduct()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Add( Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
            //return RedirectToAction("Index");
            return Json(new { status = true, JsonRequestBehavior.AllowGet });
        }
        public ActionResult EditProduct(int? id)
        {
            var product = db.Products.Find(id);
            return View("EditProduct", product);
        }
        public JsonResult Edit(Product product, int? id)
        {
            var oldProduct = db.Products.Find(product.ProductId);
            oldProduct.Name = product.Name;
            oldProduct.Category = product.Category;
            oldProduct.Price = product.Price;
            oldProduct.Stock = product.Stock;
            db.Entry(oldProduct).State = EntityState.Modified;
            db.SaveChanges();
            //return RedirectToAction("Index");
            return Json(new { status = true, JsonRequestBehavior.AllowGet });
        }
        public JsonResult Delete(int? id)
        {
            var product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return Json(new { status = true, JsonRequestBehavior.AllowGet });
        }
    }
}