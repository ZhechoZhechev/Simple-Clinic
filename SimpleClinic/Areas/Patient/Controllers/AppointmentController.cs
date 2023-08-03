namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]

public class AppointmentController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IScheduleService scheduleService;
    private readonly IAppointmentService appointmentService;

    public AppointmentController(
        UserManager<ApplicationUser> userManager,
        IScheduleService scheduleService,
        IAppointmentService appointmentService)
    {
        this.userManager = userManager;
        this.scheduleService = scheduleService;
        this.appointmentService = appointmentService;
    }

    [HttpGet]
    public IActionResult ChooseDate(string id)
    {
        ViewBag.DoctorId = id;

        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetAvailableDates(string doctorId)
    {
        try
        {
            var availableDates = await scheduleService.GetAvailableDates(doctorId);

            return Json(availableDates);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.PatientRoleName });
        }
    }


    [HttpGet]
    public async Task<IActionResult> GetDoctorSchedule(DateTime selectedDate, string doctorId)
    {
        try
        {
            var schedule = await scheduleService.GetDoctorScheduleAsync(selectedDate, doctorId);
            return PartialView("_GetDoctorSchedule", schedule);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.PatientRoleName });
        }

    }

    public async Task<IActionResult> MakeAppointment (string id)
    {

        var patient = await userManager.GetUserAsync(User);

        try
        {
            await appointmentService.CreateAppointment(id, patient.Id);
            TempData[SuccessMessage] = "Doctors appointment created successfully!";
            return RedirectToAction("All", "Doctor", new { area = RoleNames.PatientRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.PatientRoleName });
        }
    }
}