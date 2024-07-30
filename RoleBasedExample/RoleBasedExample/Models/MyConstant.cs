using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoleBasedExample.Models
{
    public static class MyConstant
    {
        public static string RoleAdmin { get; set; } = "Admin";
        public static string RoleUser { get; set; } = "User";
        public static string RoleManager { get; set; } = "Manager";
    }
}