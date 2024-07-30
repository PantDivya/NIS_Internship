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
        MainEntities db = new MainEntities();
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
               /* var roleMapping = db.UserRoleMappings.Select(m=>m.UserId).Where(m => m.UserId == user.Id);
                var id = 
                var userRole = db.Roles.Find(id);*/
                if (user != null )
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    Session["Username"] = user.Username;
                    Session["Username"] = user.Username;
                    return RedirectToAction("Index", "Employees");
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