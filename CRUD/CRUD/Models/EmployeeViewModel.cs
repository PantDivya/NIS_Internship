using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CRUD.Models
{
    public class EmployeeViewModel
    {
        public List<tblEmployee> EmployeeList { get; set; }
        public tblEmployee Employee { get; set; }

    }
}