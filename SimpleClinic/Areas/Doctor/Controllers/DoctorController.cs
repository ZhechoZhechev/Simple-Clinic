namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

[Authorize(Roles = RoleNames.DoctorRoleName)]
[Area("Doctor")]
public class DoctorController : Controller
{
    private readonly IScheduleService scheduleService;
    private readonly UserManager<ApplicationUser> userManager;

    public DoctorController(IScheduleService scheduleService,
        UserManager<ApplicationUser> userManager)
    {
        this.scheduleService = scheduleService;
        this.userManager = userManager;
    }

    [HttpGet]
    public IActionResult AddOrUpdateSchedule()
    {
        var model = new DoctorScheduleViewModel();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> CheckSchedule() 
    {
        var doctor = await userManager.GetUserAsync(User);

        if (doctor == null)
        {
            TempData[ErrorMessage] = "Docotor with such ID does not exist!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });

        }

        var model = await scheduleService.CheckSchedule(doctor.Id);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrUpdateSchedule(DoctorScheduleViewModel viewModel)
    {
        if (viewModel.Day.HasValue)
        {
            var day = viewModel.Day.Value;
            var doctor = await userManager.GetUserAsync(User);
            var scheduleExists = await scheduleService.IfDayScheduleExists(day, doctor.Id);

            if (doctor == null)
            {
                TempData[ErrorMessage] = "Docotor with such ID does not exist!";
                return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });

            }

            if (scheduleExists)
            {
                TempData[ErrorMessage] = "Schedule for this day exists. Please, select different day.";
                return RedirectToAction("AddOrUpdateSchedule", "Doctor", new { area = RoleNames.DoctorRoleName });
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                await scheduleService.AddOrUpdateDoctorScheduleAsync(doctor.Id, day, viewModel.TimeSlots);
                return RedirectToAction("AddOrUpdateSchedule", "Doctor", new { area = RoleNames.DoctorRoleName });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });
            }
        }

        TempData[ErrorMessage] = "Please, select a different day.";
        return RedirectToAction("AddOrUpdateSchedule", "Doctor", new { area = RoleNames.DoctorRoleName });

    }
}
