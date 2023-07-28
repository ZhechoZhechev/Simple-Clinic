namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure.Entities;

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

    // GET: /Doctor/AddOrUpdateSchedule
    [HttpGet]
    public IActionResult AddOrUpdateSchedule()
    {
        var viewModel = new DoctorScheduleViewModel
        {
            StartDate = DateTime.Today.AddDays(1), // Set the default start date to one day ahead
            EndDate = DateTime.Today.AddDays(7), // Set the default end date to one week ahead
            Days = new List<DayScheduleViewModel>()
        };

        for (var date = viewModel.StartDate.Date; date <= viewModel.EndDate.Date; date = date.AddDays(1))
        {
            viewModel.Days.Add(new DayScheduleViewModel
            {
                Day = date,
                TimeSlots = new List<TimeSlotViewModel>
                {
                    new TimeSlotViewModel // Add one default time slot initially
                    {
                        StartTime = date.AddHours(8),
                    }
                }
            });
        }

        return View(viewModel);
    }

    // POST: /Doctor/AddOrUpdateSchedule
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddOrUpdateSchedule(DoctorScheduleViewModel viewModel)
    {
        var doctor = await userManager.GetUserAsync(User);

        if (ModelState.IsValid)
        {
            await scheduleService.AddOrUpdateDoctorSchedule(doctor.Id, viewModel.StartDate, viewModel.EndDate, viewModel.Days);
            return RedirectToAction("Index", "Home"); // Redirect to a success page or show a success message.
        }

        // If the model is not valid, return the view with validation errors.
        return View(viewModel);
    }
}
