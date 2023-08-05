namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;


[Area("Doctor")]
[Authorize(Policy = "DoctorAdmin")]
public class ServiceController : Controller
{
    private readonly IServiceService serviceService;

    public ServiceController(IServiceService serviceService)
    {
        this.serviceService = serviceService;
    }

    [HttpGet]
    public async Task<IActionResult> AllServicesForSchedule() 
    {
        var model = await serviceService.GetAllServicesForSchedule();
        

        return View(model);
    }

    [HttpGet]
    public IActionResult AddSchedule(string serviceName)
    {
        var model = new DoctorScheduleViewModel();
        TempData["CurrServiceName"] = serviceName;

        return View(model);
    }
}
