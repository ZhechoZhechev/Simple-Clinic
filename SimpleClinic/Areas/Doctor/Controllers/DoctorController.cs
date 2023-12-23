namespace SimpleClinic.Areas.Doctor.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
    private readonly IPrescriptionService prescriptionService;
    private readonly IMedicamentService medicamentService;
    private readonly UserManager<ApplicationUser> userManager;

    /// <summary>
    /// Dealing with logic around a doctor role
    /// </summary>
    /// <param name="scheduleService"></param>
    /// <param name="patientService"></param>
    /// <param name="prescriptionService"></param>
    /// <param name="medicamentService"></param>
    /// <param name="userManager"></param>
    public DoctorController(
        IScheduleService scheduleService,
        IPatientService patientService,
        IPrescriptionService prescriptionService,
        IMedicamentService medicamentService,
        UserManager<ApplicationUser> userManager)
    {
        this.scheduleService = scheduleService;
        this.patientService = patientService;
        this.prescriptionService = prescriptionService;
        this.medicamentService = medicamentService;
        this.userManager = userManager;
    }

    /// <summary>
    /// Adding a schedule
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult AddSchedule()
    {
        var model = new DoctorScheduleViewModel();
        return View(model);
    }

    /// <summary>
    /// Retreives currents schedule
    /// </summary>
    /// <returns></returns>

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

    /// <summary>
    /// Adds a new medicament
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    public IActionResult AddMedicament() 
    {
        var model = new MedicamentViewModel();

        return View(model);
    }

    /// <summary>
    /// Adds a new medicament
    /// </summary>
    /// <returns></returns>

    [HttpPost]
    public async Task<IActionResult> AddMedicament(MedicamentViewModel viewModel) 
    {
        if (!ModelState.IsValid) 
        {
            return View(viewModel);
        }

        try
        {
            await medicamentService.AddMedicamentAsync(viewModel);
            TempData[SuccessMessage] = "Medicament added successfully!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });
        }
    }

    /// <summary>
    /// Creating a schedule
    /// </summary>
    /// <param name="viewModel"></param>
    /// <returns></returns>
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
            return RedirectToAction("AddSchedule", "Doctor", new { area = RoleNames.DoctorRoleName });
        }

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            await scheduleService.AddDoctorScheduleAsync(doctor.Id, viewModel.Day, viewModel.TimeSlots);
            TempData[SuccessMessage] = "Schedule added successfully!";
            return RedirectToAction("AddSchedule", "Doctor", new { area = RoleNames.DoctorRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });
        }

    }

    /// <summary>
    /// Write a new prescription
    /// </summary>
    /// <returns></returns>

    [HttpGet]
    public async Task<IActionResult> WritePrescription()
    {
        return View();
    }

    /// <summary>
    /// Write a new prescription
    /// </summary>
    /// <returns></returns>

    [HttpPost]
    public async Task<IActionResult> WritePrescription(PrescriptionViewModel viewModel)
    {
        var doctor = await userManager.GetUserAsync(User);

        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        try
        {
            await prescriptionService.SavePrescription(viewModel, doctor.Id);
            TempData[SuccessMessage] = "Prescription created successfully";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Index", "Home", new { area = RoleNames.DoctorRoleName });

        }
    }

    /// <summary>
    /// Get JSON with the patients on a given search term
    /// </summary>
    /// <param name="searchTerm">search term</param>
    /// <returns></returns>

    public async Task<IActionResult> GetPatientsForSelect2(string searchTerm)
    {
        var patients = await patientService.GetAllPatients(searchTerm);

        var patientData = patients.Select(patient => new { id = patient.Id, text = $"{patient.FullName}" });

        return Json(patientData);
    }

    /// <summary>
    /// Get JSON with the medicaments on a given search term
    /// </summary>
    /// <param name="searchTerm">search term</param>
    /// <returns></returns>
    public async Task<IActionResult> GetMedicamentsForSelect2(string searchTerm)
    {
        var medicaments = await medicamentService.GetAllMedicaments(searchTerm);

        var patientData = medicaments.Select(patient => new { id = patient.Id, text = $"{patient.Name}" });

        return Json(patientData);
    }
}
