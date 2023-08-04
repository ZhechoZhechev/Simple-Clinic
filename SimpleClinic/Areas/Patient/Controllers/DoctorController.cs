namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]
public class DoctorController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IDoctorService doctorService;
    private readonly ISpecialityService specialityService;
    private readonly IPrescriptionService prescriptionService;


    public DoctorController(
        UserManager<ApplicationUser> userManager,
        IDoctorService doctorService,
        ISpecialityService specialityService,
        IPrescriptionService prescriptionService)
    {
        this.userManager = userManager;
        this.doctorService = doctorService;
        this.specialityService = specialityService;
        this.prescriptionService = prescriptionService;

    }

    [HttpGet]
    public async Task<IActionResult> All([FromQuery] AllDoctorsQueryModel queryModel)
    {
        var queryResult = await doctorService.All(
            queryModel.Specialty,
            queryModel.SearchTerm,
            queryModel.CurrentPage,
            queryModel.DoctorsPerPage);

        queryModel.TotalDoctorsCount = queryResult.TotalDoctorsCount;
        queryModel.Doctors = queryResult.Doctors;

        var doctorSpecialities = await specialityService.GetAllSpecialityNames();
        queryModel.Specialities = doctorSpecialities;

        return View(queryModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(string id)
    {
        var result = await doctorService.DoctorExistsById(id);

        if (!result)
        {
            TempData[ErrorMessage] = "Docotor with such ID does not exist!";
            return RedirectToAction("All", "Doctor", new { area = RoleNames.PatientRoleName });
        }

        try
        {
            var model = await doctorService.DetailsForPatient(id);
            return View(model);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPrescrtiptionsForPatient() 
    {
        var patient = await userManager.GetUserAsync(User);

        var model = prescriptionService.GetAllPrescriptionsForPatient(patient.Id);

        return View(model);
    }
}
