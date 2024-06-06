using System;
using System.Collections.Generic;

namespace CodeLuckManagement.DbFirst.Models
{
    public partial class Department
    {
        public Department()
        {
            EmployeesDepartments = new HashSet<EmployeesDepartment>();
        }

        public int DeptId { get; set; }
        public string? DeptName { get; set; }

        public virtual ICollection<EmployeesDepartment> EmployeesDepartments { get; set; }
    }
}
