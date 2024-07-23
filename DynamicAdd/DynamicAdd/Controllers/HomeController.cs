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
            AllProductsViewModel viewModel = new AllProductsViewModel();
            var productList = db.tblProducts.ToList();
            foreach (var product in productList)
            {
                var sizes = db.tblSizes.Where(s => s.Product_Id == product.Id).ToList();
                var productViewModel = new ProductViewModel
                {
                    Product = product,
                    Sizes = sizes
                };
                viewModel.AllProducts.Add(productViewModel);
            }
            return View(viewModel);
        }
        //Add product
        public ActionResult Add()
        {
            ProductViewModel model = new ProductViewModel();
            return View(model);
        }

        public ActionResult AddProduct(ProductViewModel productViewModel)
        {
            var product = productViewModel.Product;
            db.tblProducts.Add(product);
            var sizes = productViewModel.Sizes;
            foreach (var size in sizes)
            {
                size.Product_Id = productViewModel.Product.Id;
                db.tblSizes.Add(size);
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        //Update Product
        public ActionResult Update(int id)
        {
            var product = db.tblProducts.Find(id);
            var sizes = db.tblSizes.Where(m => m.Product_Id == id).ToList();
            ProductViewModel model = new ProductViewModel();
            model.Product = product;
            model.Sizes = sizes;
            return View(model);
        }

        public ActionResult UpdateProduct(ProductViewModel productViewModel)
        {
            int id = productViewModel.Product.Id;
            var oldProduct = db.tblProducts.Find(id);
            var oldSizes = db.tblSizes.Where(m => m.Product_Id == id).ToList();
            
            oldProduct.Name = productViewModel.Product.Name;
            oldProduct.Description = productViewModel.Product.Description;
            db.Entry(oldProduct).State = EntityState.Modified;

            
            foreach (var size in productViewModel.Sizes)
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
                if (!productViewModel.Sizes.Any(s => s.Id == oldSize.Id))
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
            foreach (var oldSize in oldSizes)
            {
                db.tblSizes.Remove(oldSize);
            }
            db.tblProducts.Remove(oldProduct);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}