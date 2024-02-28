namespace SimpleClinic.Areas.Patient.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

using SimpleClinic.Common;
using SimpleClinic.Controllers;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure.Entities;
using SimpleClinic.Core.Models.PatientModels;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

/// <summary>
/// Represents the home page for the patient area
/// </summary>
[Authorize(Roles = RoleNames.PatientRoleName)]
[Area("Patient")]
public class HomeController : BaseController
{
    private readonly IAccountService accountService;
    private readonly UserManager<ApplicationUser> userManager;

    public HomeController(
        IAccountService accountService,
        UserManager<ApplicationUser> userManager)
    {
        this.accountService = accountService;
        this.userManager = userManager;
    }

    /// <summary>
    /// Starting page
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Index()
    {

        var userId = GetCurrnetUserId(TempData);

        var model = new NextOfKinViewModel()
        {
            FormsCompleted = await accountService.GetIsFormFilled(userId)

        };



        return View(model);
    }

    /// <summary>
    /// Patients must fill Next of kin form
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Index(NextOfKinViewModel model)
    { 
        var userId =  userManager.GetUserId(User);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await accountService.AddNextOfKin(model, userId);
            return RedirectToAction("AddMedicalHistory", "Home");
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home", new { area = "" });
        }
    }

    [HttpGet]
    public IActionResult AddMedicalHistory() 
    {
        var model = new MedicalHistoryViewModel();

        return View(model);
    }

    /// <summary>
    /// Patients must fill medical histor form
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> AddMedicalHistory(MedicalHistoryViewModel model) 
    {
        var userId = userManager.GetUserId(User);

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        try
        {
            await accountService.AddMedicalHistory(model, userId);
            TempData["Formcompleted"] = "yes";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home", new {area = ""});
        }
    }
}
