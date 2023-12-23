namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;

/// <summary>
/// Gets the home page for doctor area
/// </summary>
[Authorize(Roles = RoleNames.DoctorRoleName)]
[Area("Doctor")]
public class HomeController : Controller
{
    /// <summary>
    /// Home page endpoint
    /// </summary>
    /// <returns></returns>
    public IActionResult Index()
    {
        return View();
    }
}
