using Microsoft.AspNetCore.Mvc;

namespace SimpleClinic.Areas.Patient.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult ChooseDate(string id)
        {
            ViewBag.DoctorId = id;

            return View();
        }
    }
}