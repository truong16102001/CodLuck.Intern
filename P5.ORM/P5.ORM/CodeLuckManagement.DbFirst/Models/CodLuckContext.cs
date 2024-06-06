using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeLuckManagement.DbFirst.Models
{
    public partial class CodLuckContext : DbContext
    {
        public CodLuckContext()
        {
        }

        public CodLuckContext(DbContextOptions<CodLuckContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<EmployeesDepartment> EmployeesDepartments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("CodLuck");
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>(entity =>
            {
                entity.HasKey(e => e.DeptId)
                    .HasName("PK__Departme__B2079BED7E5D7C84");

                entity.Property(e => e.DeptName).HasMaxLength(100);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmpId)
                    .HasName("PK__Employee__AF2DBB99DF69CAC0");

                entity.Property(e => e.Dob).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EmpName).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<EmployeesDepartment>(entity =>
            {
                entity.ToTable("Employees_Departments");

                entity.HasOne(d => d.Dept)
                    .WithMany(p => p.EmployeesDepartments)
                    .HasForeignKey(d => d.DeptId)
                    .HasConstraintName("FK__Employees__Depar__29572725");

                entity.HasOne(d => d.Emp)
                    .WithMany(p => p.EmployeesDepartments)
                    .HasForeignKey(d => d.EmpId)
                    .HasConstraintName("FK__Employees__EmpId__286302EC");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
