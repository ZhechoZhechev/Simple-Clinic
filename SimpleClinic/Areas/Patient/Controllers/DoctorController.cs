namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;
using static SimpleClinic.Common.Constants.GeneralApplicationConstants;


/// <summary>
/// Services conected with doctors - patient side
/// </summary>
[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]
public class DoctorController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IDoctorService doctorService;
    private readonly ISpecialityService specialityService;
    private readonly IPrescriptionService prescriptionService;
    private readonly IMemoryCache memoryCache;

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="userManager"></param>
    /// <param name="doctorService"></param>
    /// <param name="specialityService"></param>
    /// <param name="prescriptionService"></param>
    public DoctorController(
        UserManager<ApplicationUser> userManager,
        IDoctorService doctorService,
        ISpecialityService specialityService,
        IPrescriptionService prescriptionService,
        IMemoryCache memoryCache)
    {
        this.userManager = userManager;
        this.doctorService = doctorService;
        this.specialityService = specialityService;
        this.prescriptionService = prescriptionService;
        this.memoryCache = memoryCache;
    }

    /// <summary>
    /// returning all registered doctors
    /// </summary>
    /// <param name="queryModel"></param>
    /// <returns></returns>
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
    /// <summary>
    /// details for a certain doctor
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

    /// <summary>
    /// returns all prescriptions for a certains patient
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetAllPrescrtiptionsForPatient() 
    {
        var patient = await userManager.GetUserAsync(User);

        try
        {
            var model = memoryCache.Get<List<PatientAllPrescriptionsViewModel>>(PatientPrescriptionsCacheKey);
            if (model == null)
            {
                model = await prescriptionService.GetAllPrescriptionsForPatient(patient.Id);

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(PatientPrescriptionsExpTime));

                memoryCache.Set(AllDepsMemoryCacheKey , model, cacheOptions);
            }
            model = await prescriptionService.GetAllPrescriptionsForPatient(patient.Id);
            return View(model);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home");
        }

    }
}
