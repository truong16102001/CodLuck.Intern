using System;
using System.Collections.Generic;

namespace CodeLuckManagement.DbFirst.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeesDepartments = new HashSet<EmployeesDepartment>();
        }

        public int EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? Dob { get; set; }

        public virtual ICollection<EmployeesDepartment> EmployeesDepartments { get; set; }
    }
}
