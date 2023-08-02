namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Mvc;

public class AppointmentController : Controller
{
    public IActionResult ChooseDate(string id)
    {
        ViewBag.DoctorId = id;

        return View();
    }
}