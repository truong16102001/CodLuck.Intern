using CodeLuckManagement.DbFirst.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodeLuckManagement.DbFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        CodLuckContext _context;
        public HomeController(ILogger<HomeController> logger, CodLuckContext codLuck)
        {
            _logger = logger;
            _context = codLuck;
        }

        public IActionResult Index()
        {
            //eager 
            var emps = _context.Employees.ToList();


            //lazy
            // Truy vấn danh sách EmployeesDepartments
            //  var employeesDepartments = _context.EmployeesDepartments.ToList();

            // Duyệt qua từng EmployeesDepartment và truy cập thuộc tính điều hướng để kích hoạt lazy loading
            //foreach (var empDept in employeesDepartments)
            //{
            //    var emp = empDept.Emp;  // Lazy loading sẽ tải dữ liệu Employee liên quan
            //    var dept = empDept.Dept;  // Lazy loading sẽ tải dữ liệu Department liên quan
            //}


            //explicit
            var e = emps.First();
            _context.Entry(e).Collection("EmployeesDepartments").Load();


            return View(emps);
        }

        public IActionResult Privacy()
        {
            return View();
        }

      
    }
}
