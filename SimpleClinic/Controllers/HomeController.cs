using Microsoft.AspNetCore.Mvc;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;
using System.Diagnostics;

namespace SimpleClinic.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly ISpecialityService specialityService;

        public HomeController(ILogger<HomeController> logger,
            ISpecialityService specialityService)
        {
            this.logger = logger;
            this.specialityService = specialityService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Departments()
        {
            return View();
        }

        public async Task<IActionResult> AllDepartments() 
        {
            var model = await specialityService.GetAllSpecialitiesWithDoctorsCount();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int statusCode)
        {
            if (statusCode == 400 || statusCode == 404)
            {
                return this.View("Error404");
            }

            if (statusCode == 401)
            {
                return this.View("Error401");
            }

            return this.View();
        }
    }
}