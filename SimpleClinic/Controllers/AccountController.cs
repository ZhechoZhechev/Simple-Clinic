﻿namespace SimpleClinic.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Common;
using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure.Entities;



public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public AccountController(
        UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        var model = new RegisterViewModel();

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = new ApplicationUser()
        {
            Email = model.Email,
            FirstName = model.FirstName,
            EmailConfirmed = true,
            LastName = model.LastName,
            UserName = model.Email
        };

        var result = await userManager.CreateAsync(user, model.Password);
        //await userManager
        //        .AddClaimAsync(user, new System.Security.Claims.Claim(ClaimTypeConstants.FirsName, user.FirstName ?? user.Email));


        if (result.Succeeded)
        {
            await signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home");
        }

        foreach (var item in result.Errors)
        {
            ModelState.AddModelError("", item.Description);
        }


        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login(string? returnUrl = null)
    {
        var model = new LoginViewModel()
        {
            ReturnUrl = returnUrl
        };

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var user = await userManager.FindByEmailAsync(model.Email);

        if (user != null)
        {

            var result = await signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
            {
                if (model.ReturnUrl != null)
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
        }

        ModelState.AddModelError("", "Invalid login");

        return View(model);
    }

    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> CreateRoles()
    {
        await roleManager.CreateAsync(new IdentityRole(RoleNames.AdminRoleName));
        await roleManager.CreateAsync(new IdentityRole(RoleNames.DoctorRoleName));
        await roleManager.CreateAsync(new IdentityRole(RoleNames.PatientRoleName));

        return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> AddUsersToRoles()
    {
        //string email1 = "stamo.petkov@gmail.com";
        //string email2 = "pesho@abv.bg";

        //var user = await userManager.FindByEmailAsync(email1);
        //var user2 = await userManager.FindByEmailAsync(email2);

        //await userManager.AddToRoleAsync(user, RoleConstants.Manager);
        //await userManager.AddToRolesAsync(user2, new string[] { RoleConstants.Supervisor, RoleConstants.Manager });

        return RedirectToAction("Index", "Home");
    }
}

