using CodLuckManagement.CodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CodLuckManagement.CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        CodLuck1DbContext _codLuck1DbContext;

        public HomeController(ILogger<HomeController> logger, CodLuck1DbContext codLuck1DbContext)
        {
            _logger = logger;
            _codLuck1DbContext = codLuck1DbContext;
        }

        public IActionResult Index()
        {
            var emps = _codLuck1DbContext.Employees.ToList();
            return View(emps);
        }

        public IActionResult Privacy()
        {
            return View();
        }
       
    }
}
