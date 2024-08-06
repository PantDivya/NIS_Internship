using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneToManyToMany.Models
{
    public partial class tbl_Department
    {
        public List<tbl_Employee> Employees { get; set; } = new List<tbl_Employee>();
    }
    public partial class tbl_Employee 
    {
        public List<tbl_Project> Projects { get; set; } = new List<tbl_Project>();
    }
}