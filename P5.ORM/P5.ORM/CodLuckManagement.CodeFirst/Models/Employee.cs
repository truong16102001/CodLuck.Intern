using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodLuckManagement.CodeFirst.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpId {  get; set; }

        [Required]
        [StringLength(50)]
        public string? EmpName {  get; set; }

        [Required]
        [StringLength(100)]
        public string? Email {  get; set; }

        [Required]
        [StringLength(20)]
        public string? Phone {  get; set; }

        public DateTime Dob {  get; set; }

        public ICollection<EmployeeDepartment> EmployeeDepartments { get; set; }

    }
}
