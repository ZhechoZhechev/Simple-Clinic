namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;

[Authorize(Roles = RoleNames.DoctorRoleName)]
[Area("Doctor")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
