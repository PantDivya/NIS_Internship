using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace RoleBasedWithCrud.Models
{
    public class UserRoleProvider : RoleProvider
    {
        private MainEntities _db = new MainEntities(); // Your DbContext or data access class

        public override string ApplicationName { get; set; }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            // Add users to roles
            foreach (var username in usernames)
            {
                var user = _db.Users.SingleOrDefault(u => u.Username == username);
                if (user == null) continue;

                foreach (var roleName in roleNames)
                {
                    var role = _db.Roles.SingleOrDefault(r => r.RoleName == roleName);
                    if (role == null) continue;

                    if (!_db.UserRoleMappings.Any(urm => urm.UserId == user.Id && urm.RoleId == role.Id))
                    {
                        _db.UserRoleMappings.Add(new UserRoleMapping
                        {
                            UserId = user.Id,
                            RoleId = role.Id
                        });
                    }
                }
            }
            _db.SaveChanges();
        }

        public override void CreateRole(string roleName)
        {
            if (!_db.Roles.Any(r => r.RoleName == roleName))
            {
                _db.Roles.Add(new Role { RoleName = roleName });
                _db.SaveChanges();
            }
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            var role = _db.Roles.SingleOrDefault(r => r.RoleName == roleName);
            if (role == null) return false;

            if (throwOnPopulatedRole && _db.UserRoleMappings.Any(urm => urm.RoleId == role.Id))
            {
                throw new InvalidOperationException("Role has users assigned and cannot be deleted.");
            }

            _db.Roles.Remove(role);
            _db.SaveChanges();
            return true;
        }

        public override bool RoleExists(string roleName)
        {
            return _db.Roles.Any(r => r.RoleName == roleName);
        }

        public override string[] GetAllRoles()
        {
            return _db.Roles.Select(r => r.RoleName).ToArray();
        }

        public override string[] GetRolesForUser(string username)
        {
            var user = _db.Users.SingleOrDefault(u => u.Username == username);
            if (user == null) return new string[0];

            return (from urm in _db.UserRoleMappings
                    join r in _db.Roles on urm.RoleId equals r.Id
                    where urm.UserId == user.Id
                    select r.RoleName).ToArray();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var role = _db.Roles.SingleOrDefault(r => r.RoleName == roleName);
            if (role == null) return new string[0];

            return (from urm in _db.UserRoleMappings
                    join u in _db.Users on urm.UserId equals u.Id
                    where urm.RoleId == role.Id
                    select u.Username).ToArray();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            var user = _db.Users.SingleOrDefault(u => u.Username == username);
            if (user == null) return false;

            var role = _db.Roles.SingleOrDefault(r => r.RoleName == roleName);
            if (role == null) return false;

            return _db.UserRoleMappings.Any(urm => urm.UserId == user.Id && urm.RoleId == role.Id);
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            foreach (var username in usernames)
            {
                var user = _db.Users.SingleOrDefault(u => u.Username == username);
                if (user == null) continue;

                foreach (var roleName in roleNames)
                {
                    var role = _db.Roles.SingleOrDefault(r => r.RoleName == roleName);
                    if (role == null) continue;

                    var userRoleMapping = _db.UserRoleMappings.SingleOrDefault(urm => urm.UserId == user.Id && urm.RoleId == role.Id);
                    if (userRoleMapping != null)
                    {
                        _db.UserRoleMappings.Remove(userRoleMapping);
                    }
                }
            }
            _db.SaveChanges();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
    }
}