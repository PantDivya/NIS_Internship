using RoleBasedWithCrud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace RoleBasedWithCrud.Controllers
{
    public class AccountController : Controller
    {
        MainEntities _db = new MainEntities();
        // GET: Account
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
                var roleMapping = _db.UserRoleMappings.SingleOrDefault(r=>r.UserId == user.Id);
                var id = roleMapping.RoleId;
                var userRole = _db.Roles.Find(id);
                if (user != null && userRole.RoleName == "Manager" || userRole.RoleName == "Admin")
                {
                    FormsAuthentication.SetAuthCookie(user.Username, true);
                    Session["Username"] = user.Username;
                    Session["Username"] = user.Username;
                    return RedirectToAction("Index", "Users");
                }
                else
                {
                    // Authentication failed
                    ViewBag.ErrorMessage = "Invalid username or password.";
                    return View("Index", "Home");
                }
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            ViewBag.Roles = _db.Roles.ToList();
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            // Validate input
            if (string.IsNullOrEmpty(model.User.Username) || string.IsNullOrEmpty(model.User.Password) || model.SelectedRoleId == 0)
            {
                ViewBag.ErrorMessage = "All fields are required.";
                ViewBag.Roles = _db.Roles.ToList();
                return View();
            }

            // Check if user already exists
            if (_db.Users.Any(u => u.Username == model.User.Username))
            {
                ViewBag.ErrorMessage = "Username already exists.";
                ViewBag.Roles = _db.Roles.ToList();
                return View();
            }

            // Create new user
            var newUser = new User
            {
                Username = model.User.Username,
                Password = model.User.Password
            };
            _db.Users.Add(newUser);
            _db.SaveChanges();

            // Assign role to the user
            var userRoleMapping = new UserRoleMapping
            {
                UserId = newUser.Id,
                RoleId = model.SelectedRoleId
            };
            _db.UserRoleMappings.Add(userRoleMapping);
            _db.SaveChanges();

            // Set authentication cookie
            FormsAuthentication.SetAuthCookie(newUser.Username, false);
            Session["Username"] = newUser.Username;

            return RedirectToAction("Index", "Home");
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