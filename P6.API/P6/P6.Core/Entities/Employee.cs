using System;
using System.Collections.Generic;

namespace P6.Core.Entities;

public partial class Employee
{
    public int EmpId { get; set; }

    public string? EmpName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateTime? Dob { get; set; }

    public virtual ICollection<EmployeesDepartment> EmployeesDepartments { get; set; } = new List<EmployeesDepartment>();
}
