namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

/// <summary>
/// Controller for the services(IMR, PET scan etc.)
/// </summary>
[Area("Doctor")]
[Authorize(Policy = "DoctorAdmin")]
public class ServiceController : Controller
{
    private readonly IServiceService serviceService;
    private readonly IScheduleService scheduleService;
    
    /// <summary>
    /// Contructor with arguments
    /// </summary>
    /// <param name="serviceService"></param>
    /// <param name="scheduleService"></param>
    public ServiceController(
        IServiceService serviceService,
        IScheduleService scheduleService)
    {
        this.serviceService = serviceService;
        this.scheduleService = scheduleService;

    }

    /// <summary>
    /// Gets all service you can add a schedule for
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> AllServicesForSchedule() 
    {
        var model = await serviceService.GetAllServicesForSchedule();
        

        return View(model);
    }

    /// <summary>
    /// Create a schedule for a service
    /// </summary>
    /// <param name="serviceName"></param>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult AddSchedule(string serviceName, string id)
    {
        var model = new DoctorScheduleViewModel() 
        {
            ServiceId = id
        };
        TempData["CurrServiceName"] = serviceName;

        return View(model);
    }

    /// <summary>
    /// Create a schedule for a service
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddSchedule(DoctorScheduleViewModel viewModel) 
    {
        var scheduleExists = await scheduleService.IfDayServiceScheduleExists(viewModel.Day, viewModel.ServiceId!);


        if (scheduleExists)
        {
            TempData[ErrorMessage] = "Schedule for this day exists. Please, select different day.";
            return RedirectToAction("AddSchedule", "Service", new { area = RoleNames.DoctorRoleName });
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            await scheduleService.AddServiceScheduleAsync(viewModel.ServiceId!, viewModel.Day, viewModel.TimeSlots);
            TempData[SuccessMessage] = "Schedule added successfully!";
            return RedirectToAction("AddSchedule", "Service", new { area = RoleNames.DoctorRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });
        }

    }

    /// <summary>
    /// Gets already created schedules
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> CheckSchedule(string id) 
    {
        var model = await scheduleService.CheckServiceSchedule(id);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> AddService()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddService(ServiceViewModel serviceModel)
    {
        if (!ModelState.IsValid)
        {
            return View(serviceModel);
        }

        try
        {
            await serviceService.AddServiceAsync(serviceModel);
            TempData[SuccessMessage] = "Service added successfully!";
            return RedirectToAction("AllServicesForSchedule", "Service", new { area = RoleNames.DoctorRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });
        }
    }

    [HttpGet]
    public async Task<IActionResult> EditService()
    {
        return View();
    }
}
