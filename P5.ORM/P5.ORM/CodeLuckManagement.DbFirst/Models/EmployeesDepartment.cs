﻿using System;
using System.Collections.Generic;

namespace CodeLuckManagement.DbFirst.Models
{
    public partial class EmployeesDepartment
    {
        public int Id { get; set; }
        public int? EmpId { get; set; }
        public int? DeptId { get; set; }

        public virtual Department? Dept { get; set; }
        public virtual Employee? Emp { get; set; }
    }
}
