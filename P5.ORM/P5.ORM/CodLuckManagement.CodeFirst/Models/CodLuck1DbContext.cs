using Microsoft.EntityFrameworkCore;

namespace CodLuckManagement.CodeFirst.Models
{
    public class CodLuck1DbContext:DbContext
    {
        public CodLuck1DbContext(DbContextOptions options):base(options) { }

        #region DbSet
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Department> Departments { get; set; }

        public DbSet<EmployeeDepartment> EmployeeDepartments { get; set; }

        #endregion

       
    }
}
