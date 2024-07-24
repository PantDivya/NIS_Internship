using DynamicAdd.Models;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicAdd.Controllers
{
    public class HomeController : Controller
    {
        ProductEntities db = new ProductEntities();
        public ActionResult Index()
        {
            List<tblProduct> viewModel = new List<tblProduct>();
            var productList = db.tblProducts.ToList();
            viewModel.AddRange(productList);
            return View(viewModel);
        }
        //Add product
        public ActionResult Add()
        {
            var model = new tblProduct();
            return View(model);
        }

        public ActionResult AddProduct(tblProduct product)
        {
            db.tblProducts.Add(product);
            var sizes = product.Sizes;
            foreach (var size in sizes)
            {
                size.Product_Id = product.Id;
                
            }
            db.tblSizes.AddRange(sizes);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Update Product
        public ActionResult Update(int id)
        { 
            var product = db.tblProducts.Find(id);
            product.Sizes = product.tblSizes.ToList();
            return View(product);
        }

        public ActionResult UpdateProduct(tblProduct product, int? id)
        {
            var oldProduct = db.tblProducts.Find(id);
            var oldSizes = db.tblSizes.Where(m => m.Product_Id == id).ToList();
            
            oldProduct.Name = product.Name;
            oldProduct.Description = product.Description;
            db.Entry(oldProduct).State = EntityState.Modified;

            
            foreach (var size in product.Sizes)
            {
                var oldSize = oldSizes.FirstOrDefault(s => s.Id == size.Id);

                if (oldSize != null)
                {
                    oldSize.Size_Name = size.Size_Name;
                    oldSize.price = size.price;
                    db.Entry(oldSize).State = EntityState.Modified;
                }
                else
                {
                    size.Product_Id = id;
                    db.tblSizes.Add(size);
                }
            }
            foreach (var oldSize in oldSizes)
            {
                if (!product.Sizes.Any(s => s.Id == oldSize.Id))
                {
                    db.tblSizes.Remove(oldSize);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        //Delete product
        public ActionResult Delete(tblProduct product)
        {
            int Id = product.Id;
            var oldProduct = db.tblProducts.Find(Id);
            var oldSizes = db.tblSizes.Where(m => m.Product_Id == Id).ToList();
            db.tblSizes.RemoveRange(oldSizes);
            db.tblProducts.Remove(oldProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}