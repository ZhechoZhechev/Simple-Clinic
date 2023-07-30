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
        return View();
    }

    //// POST: /Doctor/AddOrUpdateSchedule
    //[HttpPost]
    //public async Task<IActionResult> AddOrUpdateSchedule(DoctorScheduleViewModel viewModel)
    //{
    //    var doctor = await userManager.GetUserAsync(User);

    //    if (ModelState.IsValid)
    //    {
    //        await scheduleService.AddOrUpdateDoctorSchedule(doctor.Id, viewModel.StartDate, viewModel.EndDate, viewModel.Days);
    //        return RedirectToAction("Index", "Home"); // Redirect to a success page or show a success message.
    //    }

    //    // If the model is not valid, return the view with validation errors.
    //    return View(viewModel);
    //}
}
