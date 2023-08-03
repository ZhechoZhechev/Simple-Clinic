namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]

public class AppointmentController : Controller
{

    private readonly IScheduleService scheduleService;

    public AppointmentController(IScheduleService scheduleService)
    {
        this.scheduleService = scheduleService;
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
}