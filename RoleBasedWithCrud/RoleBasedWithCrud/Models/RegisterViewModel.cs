using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoleBasedWithCrud.Models
{
    public class RegisterViewModel
    {
        public User User { get; set; }
        public int SelectedRoleId { get; set; }
    }
}