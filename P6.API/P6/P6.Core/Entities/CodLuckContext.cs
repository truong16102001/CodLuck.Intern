using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace P6.Core.Entities;

public partial class CodLuckContext : DbContext
{
    public CodLuckContext()
    {
    }

    public CodLuckContext(DbContextOptions<CodLuckContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<EmployeesDepartment> EmployeesDepartments { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server =(local);database=CodLuck;uid=ndt;pwd=16102001;Trusted_Connection=True;Encrypt=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DeptId).HasName("PK__Departme__B2079BED7E5D7C84");

            entity.Property(e => e.DeptName).HasMaxLength(100);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmpId).HasName("PK__Employee__AF2DBB99DF69CAC0");

            entity.ToTable(tb => tb.HasTrigger("DeleteEmployeeTrigger"));

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
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC0750F95BE3");

            entity.ToTable("Employees_Departments");

            entity.HasOne(d => d.Dept).WithMany(p => p.EmployeesDepartments)
                .HasForeignKey(d => d.DeptId)
                .HasConstraintName("FK__Employees__Depar__29572725");

            entity.HasOne(d => d.Emp).WithMany(p => p.EmployeesDepartments)
                .HasForeignKey(d => d.EmpId)
                .HasConstraintName("FK__Employees__EmpId__286302EC");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__User__1788CC4CD407D71A");

            entity.ToTable("User");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.ExpiredToken).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(2000);
            entity.Property(e => e.RefreshToken).HasMaxLength(1000);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
