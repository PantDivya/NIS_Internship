using RoleBasedExample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RoleBasedExample.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult LogIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(User loginUser)
        {
            // Validate input
            if (string.IsNullOrEmpty(loginUser.Username) || string.IsNullOrEmpty(loginUser.Password))
            {
                ViewBag.ErrorMessage = "Username and password are required.";
                return View("Index", "Home");
            }

            // Authenticate user
            using (var _db = new MainEntities()) 
            {
                var user = _db.Users.SingleOrDefault(u => u.Username == loginUser.Username && u.Password == loginUser.Password);

                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(user.Username, false);
                    Session["Username"] = user.Username;
                    Session["Username"] = user.Username; 
                    return RedirectToAction("Index", "Home"); 
                }
                else
                {
                    // Authentication failed
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("Index", "Home");
                }
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}