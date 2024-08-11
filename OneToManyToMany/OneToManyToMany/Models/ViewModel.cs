using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OneToManyToMany.Models
{
    public class DepartmentViewModel
    {
        public string Name { get; set; }
        public List<EmployeeViewModel> Employees { get; set; }
    }

    public class EmployeeViewModel
    {
        public string Name { get; set; }
        public List<ProjectViewModel> Projects { get; set; }
    }

    public class ProjectViewModel
    {
        public string ProjectName { get; set; }
    }

}