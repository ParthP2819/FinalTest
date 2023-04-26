using FinalTest.DataAccess;
using FinalTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FinalTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _dbContext;
        

        public HomeController(ILogger<HomeController> logger,ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM obj)
        {
            if (ModelState.IsValid)
            {
                var data = await _dbContext.admins.FirstOrDefaultAsync(i => i.AdminEmail == obj.Email);
                if (data != null)
                {
                    if (obj.Email == data.AdminEmail && obj.Password == data.AdminPassword)
                    {
                        TempData["success"] = "Login Successfully";
                        return RedirectToAction("Index", "Admin");
                    }
                }
                
            }
            TempData["error"] = "Something Went Wronge";
            return View();  
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}