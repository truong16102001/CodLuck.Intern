using System;
using System.Collections.Generic;

namespace P6.DataAccess.Entities;

public partial class Department
{
    public int DeptId { get; set; }

    public string? DeptName { get; set; }

    public virtual ICollection<EmployeesDepartment> EmployeesDepartments { get; set; } = new List<EmployeesDepartment>();
}
