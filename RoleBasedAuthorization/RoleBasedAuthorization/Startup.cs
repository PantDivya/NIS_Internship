using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using RoleBasedAuthorization.Models;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(RoleBasedAuthorization.Startup))]
namespace RoleBasedAuthorization
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            PopulateUserAndRoles();
        }
        public void PopulateUserAndRoles()
        {
            var db = new ApplicationDbContext();
            //populating roles
            if (!db.Roles.Any(x => x.Name == MyConstant.RoleAdmin))
            {
                db.Roles.Add(new IdentityRole(MyConstant.RoleAdmin));
                db.SaveChanges();
            }

            if (!db.Roles.Any(x => x.Name == MyConstant.RoleUser))
            {
                db.Roles.Add(new IdentityRole(MyConstant.RoleUser));
                db.SaveChanges();
            }
            //populating user
            if (!db.Users.Any(x => x.UserName == "myAdmin"))
            {
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new ApplicationUserManager(userStore);

                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var newUser = new ApplicationUser
                {
                    Email = "myAdmin@test.com",
                    UserName = "myAdmin"
                };
                userManager.Create(newUser, "admin123");
                userManager.AddToRole(newUser.Id, MyConstant.RoleAdmin);
                db.SaveChanges();
            }

            if (!db.Users.Any(x => x.UserName == "myUser"))
            {
                var userStore = new UserStore<ApplicationUser>(db);
                var userManager = new ApplicationUserManager(userStore);

                var roleStore = new RoleStore<IdentityRole>(db);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var newUser = new ApplicationUser
                {
                    Email = "myUser@test.com",
                    UserName = "myUser"
                };
                userManager.Create(newUser, "user123");
                userManager.AddToRole(newUser.Id, MyConstant.RoleUser);
                db.SaveChanges();
            }
        }
    }
}
