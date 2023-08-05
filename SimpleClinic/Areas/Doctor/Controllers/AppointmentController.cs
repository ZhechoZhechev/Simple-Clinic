namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure.Entities;

public class AppointmentController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IAppointmentService appointmentService;

    public AppointmentController(
        UserManager<ApplicationUser> userManager,
        IAppointmentService appointmentService)
    {
        this.userManager = userManager;
        this.appointmentService = appointmentService;
    }

    public async Task<IActionResult> GetPatientAppointments(string id)
    {
        var doctor = await userManager.GetUserAsync(User);

        var model = await appointmentService.GetDoctorAppointmentsForPatient(doctor.Id);

        return View(model);
    }
}
