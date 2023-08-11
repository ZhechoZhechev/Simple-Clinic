namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;

using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;


[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]
public class AppointmentController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IConfiguration configuration;
    private readonly IScheduleService scheduleService;
    private readonly IAppointmentService appointmentService;

    public AppointmentController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        IScheduleService scheduleService,
        IAppointmentService appointmentService)
    {
        this.userManager = userManager;
        this.configuration = configuration;
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
    public IActionResult ChooseServiceDate(string id)
    {
        ViewBag.ServiceId = id;

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
    public async Task<IActionResult> GetAvailableDatesService(string serviceId)
    {
        try
        {
            var availableDates = await scheduleService.GetAvailableDatesService(serviceId);

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

    [HttpGet]
    public async Task<IActionResult> GetServiceSchedule(DateTime selectedDate, string serviceId)
    {
        try
        {
            var schedule = await scheduleService.GetServiceScheduleAsync(selectedDate, serviceId);
            return PartialView("_GetServiceSchedule", schedule);
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

    public async Task<IActionResult> MakeServiceAppointment(string id)
    {

        var patient = await userManager.GetUserAsync(User);

        try
        {
            await appointmentService.CreateServiceAppointment(id, patient.Id);
            TempData[SuccessMessage] = "Service appointment created successfully!";
            return RedirectToAction("All", "Service", new { area = RoleNames.PatientRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.PatientRoleName });
        }
    }

    public async Task<IActionResult> GetDocBookings() 
    {
        var patient = await userManager.GetUserAsync(User);

        var model = await appointmentService.GetDoctorAppointmentsForPatient(patient.Id);

        return View(model);
    }
    public async Task<IActionResult> GetServiceBookings()
    {
        var patient = await userManager.GetUserAsync(User);

        var model = await appointmentService.GetServiceAppointmentsForPatient(patient.Id);

        return View(model);
    }

    public async Task<IActionResult> CancelDocBooking(string id) 
    {
        var apppointment = await appointmentService.GetAppointmentById(id);
        var doctorEmail = apppointment.Doctor.Email;
        var patientName = $"{apppointment.Patient.FirstName} {apppointment.Patient.LastName}";
        var patientPhone = apppointment.Patient.PhoneNumber;
        var message = $"Appointment for {apppointment.BookingDateTime.ToString("d.M.yyyy")} at {apppointment.TimeSlot.StartTime.TimeOfDay} has been canceled.";

        try
        {
            await appointmentService.CancelDocAppointment(id);
            SendMailWhenCancelBooking(doctorEmail, patientName, patientPhone, message);
            TempData[SuccessMessage] = "Your appointment has been canceled successfully! Doctor has been notified.";
            return RedirectToAction("GetDocBookings", "Appointment", new { area = RoleNames.PatientRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.PatientRoleName });
        }
    }

    public async Task<IActionResult> CancelServiceBooking(string id)
    {
        var smtpConfig = configuration.GetSection("Smtp");
        var apppointment = await appointmentService.GetAppointmentById(id);
        var smtpUsername = smtpConfig["Username"];
        var patientName = $"{apppointment.Patient.FirstName} {apppointment.Patient.LastName}";
        var patientPhone = apppointment.Patient.PhoneNumber;
        var message = $"Appointment {apppointment.Service.Name} on {apppointment.BookingDateTime.ToString("d.M.yyyy")} at {apppointment.TimeSlot.StartTime.TimeOfDay} has been canceled.";

        try
        {
            await appointmentService.CancelServiceAppointment(id);
            SendMailWhenCancelBooking(smtpUsername, patientName, patientPhone, message);
            TempData[SuccessMessage] = "Your appointment has been canceled successfully! Clinic has been notified.";
            return RedirectToAction("GetServiceBookings", "Appointment", new { area = RoleNames.PatientRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.PatientRoleName });
        }
    }

    private void SendMailWhenCancelBooking(string email, string name, string phone, string message) 
    {
        try
        {
            var smtpConfig = configuration.GetSection("Smtp");
            var smtpHost = smtpConfig["Host"];
            var smtpPort = int.Parse(smtpConfig["Port"]);
            var smtpUsername = smtpConfig["Username"];
            var smtpPassword = smtpConfig["Password"];

            var messageBody = $"Email: {email}\nName: {name}\nPhone: {phone}\nMessage: {message}";

            var messageToSend = new MimeMessage();
            messageToSend.From.Add(new MailboxAddress("", smtpUsername));
            messageToSend.To.Add(new MailboxAddress("", email));
            messageToSend.Subject = $"Appointment with patient {name} has been canceled";
            messageToSend.Body = new TextPart("plain")
            {
                Text = messageBody
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(smtpHost, smtpPort, SecureSocketOptions.Auto);
                smtpClient.Authenticate(smtpUsername, smtpPassword);
                smtpClient.Send(messageToSend);
                smtpClient.Disconnect(true);
            }

        }
        catch (Exception ex)
        {
            TempData[ErrorMessage] = ex.ToString();
        }
    }
}