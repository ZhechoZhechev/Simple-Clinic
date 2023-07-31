namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

[Authorize(Roles = RoleNames.DoctorRoleName)]
[Area("Doctor")]
public class DoctorController : Controller
{
    private readonly IScheduleService scheduleService;
    private readonly IPatientService patientService;
    private readonly UserManager<ApplicationUser> userManager;

    public DoctorController(IScheduleService scheduleService,
        UserManager<ApplicationUser> userManager,
        IPatientService patientService)
    {
        this.scheduleService = scheduleService;
        this.userManager = userManager;
        this.patientService = patientService;
    }

    [HttpGet]
    public IActionResult AddSchedule()
    {
        var model = new DoctorScheduleViewModel();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> CheckSchedule() 
    {
        var doctor = await userManager.GetUserAsync(User);

        if (doctor == null)
        {
            TempData[ErrorMessage] = "Docotor with such ID does not exist!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });

        }

        var model = await scheduleService.CheckSchedule(doctor.Id);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddSchedule(DoctorScheduleViewModel viewModel)
    {
        
            var doctor = await userManager.GetUserAsync(User);
            var scheduleExists = await scheduleService.IfDayScheduleExists(viewModel.Day, doctor.Id);

            if (doctor == null)
            {
                TempData[ErrorMessage] = "Docotor with such ID does not exist!";
                return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });

            }

            if (scheduleExists)
            {
                TempData[ErrorMessage] = "Schedule for this day exists. Please, select different day.";
                return RedirectToAction("AddOrUpdateSchedule", "Doctor", new { area = RoleNames.DoctorRoleName });
            }

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            try
            {
                await scheduleService.AddDoctorScheduleAsync(doctor.Id, viewModel.Day, viewModel.TimeSlots);
                return RedirectToAction("AddOrUpdateSchedule", "Doctor", new { area = RoleNames.DoctorRoleName });
            }
            catch (Exception)
            {
                TempData[ErrorMessage] = "Something went wrong!";
                return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });
            }

    }

    [HttpGet]
    public async Task<IActionResult> WritePrescription () 
    {
        //var model = new PrescriptionViewModel();

        var patients = await patientService.GetAllPatients();

        ViewBag.PatientList = new SelectList(patients, "Id", "FullName");

        return View();
    }
}
