namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;

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
}
