namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Common.Helpers;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

[Authorize(Roles = RoleNames.DoctorRoleName)]
[Area("Doctor")]
public class AppointmentController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IAppointmentService appointmentService;
    private readonly EmailService emailService;

    public AppointmentController(
        UserManager<ApplicationUser> userManager,
        EmailService emailService,
        IAppointmentService appointmentService)
    {
        this.userManager = userManager;
        this.emailService = emailService;
        this.appointmentService = appointmentService;
    }

    public async Task<IActionResult> GetPatientAppointments()
    {
        var doctor = await userManager.GetUserAsync(User);

        var model = await appointmentService.GetPatientAppointmentsForDoctor(doctor.Id);

        return View(model);
    }

    public async Task<IActionResult> CancelPatientAppointment(string id)
    {
        var appointment = await appointmentService.GetAppointmentById(id);
        var email = appointment.Patient.Email;
        var doctorName = $"{appointment.Doctor.FirstName} {appointment.Doctor.LastName}";
        var doctorPhone = appointment.Doctor.OfficePhoneNumber;
        var message = $"Appointment for {appointment.BookingDateTime.ToString("d.M.yyyy")} at {appointment.TimeSlot.StartTime.TimeOfDay} has been canceled.";

        try
        {
            emailService.SendMailWhenCancelBooking(email, doctorName, doctorPhone, message);
            await appointmentService.CancelPatientAppointment(id);
            TempData[SuccessMessage] = "You have canceled the appointment, please let the patient know via email.";
            return RedirectToAction("GetPatientAppointments", "Appointment", new { area = RoleNames.DoctorRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });
        }
    }
}
