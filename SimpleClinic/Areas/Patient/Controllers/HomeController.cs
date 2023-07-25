﻿namespace SimpleClinic.Core.Services;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Controllers;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;

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

    [HttpPost]
    public async Task<IActionResult> Index(NextOfKinViewModel model)
    { 
        var userId =  userManager.GetUserId(User);

        try
        {
            await accountService.AddNextOfKin(model, userId);
            return RedirectToAction("AddMedicalHistory", "Home");
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home");
        }
    }

    [HttpGet]
    public IActionResult AddMedicalHistory() 
    {
        var model = new MedicalHistoryViewModel();

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> AddMedicalHistory(MedicalHistoryViewModel model) 
    {
        var userId = userManager.GetUserId(User);

        try
        {
            await accountService.AddMedicalHistory(model, userId);
            TempData["Formcompleted"] = "yes";
            return RedirectToAction("Index", "Home");
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home");
        }
    }
}
