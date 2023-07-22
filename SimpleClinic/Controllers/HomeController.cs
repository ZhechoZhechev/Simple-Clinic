namespace SimpleClinic.Controllers;

using Microsoft.AspNetCore.Mvc;
using SimpleClinic.Core.Contracts;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> logger;
    private readonly ISpecialityService specialityService;
    private readonly IDoctorService doctorService;

    public HomeController(ILogger<HomeController> logger,
        ISpecialityService specialityService,
        IDoctorService doctorService)
    {
        this.logger = logger;
        this.specialityService = specialityService;
        this.doctorService = doctorService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Departments()
    {
        return View();
    }

    public async Task<IActionResult> Doctors()
    {
        var model = await doctorService.GetFirstThreeDoctors();

        return View(model);
    }

    public async Task<IActionResult> DoctorDetails(string Id)
    {
        var model = doctorService.DoctorDetails(Id);

        return View(model);
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