namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;

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
    public async Task<IActionResult> GetDoctorSchedule(DateTime selectedDate, string doctorId)
    {

        //selectedDate = TimeZoneInfo.ConvertTimeFromUtc(selectedDate, TimeZoneInfo.Local);

        try
        {
            // Fetch and generate the doctor's schedule based on the selectedDate
            var schedule = await scheduleService.GetDoctorScheduleAsync(selectedDate, doctorId);

            return View("GetDoctorSchedule", schedule);
        }
        catch (Exception ex)
        {
            // Handle errors and return an appropriate response
            return BadRequest("Error fetching doctor's schedule: " + ex.Message);
        }

    }
}