namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;

[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]
public class AppointmentController : Controller
{
    public IActionResult ChooseDate(string id)
    {
        ViewBag.DoctorId = id;

        return View();
    }
}