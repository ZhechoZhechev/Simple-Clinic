namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Common.Helpers;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;


[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]

public class AppointmentController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly EmailService emailService;
    private readonly IScheduleService scheduleService;
    private readonly IAppointmentService appointmentService;
    private readonly IConfiguration configuration;

    /// <summary>
    /// Controls appointments patients side
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="emailService"></param>
    /// <param name="configuration"></param>
    /// <param name="scheduleService"></param>
    /// <param name="appointmentService"></param>
    public AppointmentController(
        UserManager<ApplicationUser> userManager,
        EmailService emailService,
        IConfiguration configuration,
        IScheduleService scheduleService,
        IAppointmentService appointmentService)
    {
        this.userManager = userManager;
        this.emailService = emailService;
        this.configuration = configuration;
        this.scheduleService = scheduleService;
        this.appointmentService = appointmentService;
    }

    /// <summary>
    /// gets doctors ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult ChooseDate(string id)
    {
        ViewBag.DoctorId = id;

        return View();
    }

    /// <summary>
    /// gets service ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet]
    public IActionResult ChooseServiceDate(string id)
    {
        ViewBag.ServiceId = id;

        return View();
    }

    /// <summary>
    /// gets availabe dates for certain doctor
    /// </summary>
    /// <param name="doctorId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// gets availabe dates for certain service
    /// </summary>
    /// <param name="serviceId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// get doctors schedule for a certain date
    /// </summary>
    /// <param name="selectedDate"></param>
    /// <param name="doctorId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// get service schedule for a certain date
    /// </summary>
    /// <param name="selectedDate"></param>
    /// <param name="doctorId"></param>
    /// <returns></returns>
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

    /// <summary>
    /// makes doctors appointment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// makes service appointment
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// get all doctors appointments for a certain patient
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetDocBookings() 
    {
        var patient = await userManager.GetUserAsync(User);

        var model = await appointmentService.GetDoctorAppointmentsForPatient(patient.Id);

        return View(model);
    }

    /// <summary>
    /// get all service appointments for a certain patient
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> GetServiceBookings()
    {
        var patient = await userManager.GetUserAsync(User);

        var model = await appointmentService.GetServiceAppointmentsForPatient(patient.Id);

        return View(model);
    }

    /// <summary>
    /// cancel doctors booking
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
            emailService.SendMailWhenCancelBooking(doctorEmail, patientName, patientPhone, message);
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
            emailService.SendMailWhenCancelBooking(smtpUsername, patientName, patientPhone, message);
            TempData[SuccessMessage] = "Your appointment has been canceled successfully! Clinic has been notified.";
            return RedirectToAction("GetServiceBookings", "Appointment", new { area = RoleNames.PatientRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.PatientRoleName });
        }
    }

}