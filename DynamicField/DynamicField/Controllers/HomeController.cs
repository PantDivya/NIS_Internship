using DynamicField.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DynamicField.Controllers
{
    public class HomeController : Controller
    {
        OrderEntities db = new OrderEntities();
        public ActionResult Index()
        {
            var products = db.Products.ToList();
            return View(products);
        }
        [HttpPost]
        public ActionResult Add(OrderViewModel orderViewModel)
        {
            return View("Index");
        }
    }
}