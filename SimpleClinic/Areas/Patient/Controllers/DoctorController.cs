namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]
public class DoctorController : Controller
{
    private readonly IDoctorService doctorService;
    private readonly ISpecialityService specialityService;


    public DoctorController(IDoctorService doctorService,
        ISpecialityService specialityService)
    {
        this.doctorService = doctorService;
        this.specialityService = specialityService;
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
}
