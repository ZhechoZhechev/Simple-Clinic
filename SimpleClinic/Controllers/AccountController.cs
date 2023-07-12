namespace SimpleClinic.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleClinic.Common;
using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure.Entities;
using System.Numerics;

/// <summary>
/// Account controller
/// </summary>
[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly string directoryPath;
    private readonly IWebHostEnvironment webHostEnvironment;

    public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration,
        IWebHostEnvironment webHostEnvironment)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.directoryPath = configuration["UpploadSettings:ImageDir"];
        this.webHostEnvironment = webHostEnvironment;
    }

    /// <summary>
    /// Register initial form
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        var model = new RegisterViewModel();

        return View(model);
    }

    /// <summary>
    /// Submits the form
    /// </summary>
    /// <param name="model">The form itself</param>
    /// <returns></returns>
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        if (model.SelectedRole == RoleNames.DoctorRoleName)
        {
            var doctorRegistrationModel = new RegisterViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                Password = model.Password,
                PasswordRepeat = model.PasswordRepeat,
                SelectedRole = model.SelectedRole
            };

            return RedirectToAction("RegisterDoctor", "Account", doctorRegistrationModel);
        }
        else if (model.SelectedRole == RoleNames.PatientRoleName)
        {
            var patientRegistrationModel = new RegisterViewModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Address = model.Address,
                Password = model.Password,
                PasswordRepeat = model.PasswordRepeat,
                SelectedRole = model.SelectedRole
            };

            return RedirectToAction("RegisterPatient", "Account", patientRegistrationModel);
        }

        return View(model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult RegisterPatient(RegisterViewModel model)
    {
        var patientModel = new PatientRegistrationViewModel()
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Address = model.Address,
            Password = model.Password,
            PasswordRepeat = model.PasswordRepeat,
            SelectedRole = model.SelectedRole
        };

        return View(patientModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterPatient(PatientRegistrationViewModel model)
    {
        if (ModelState.IsValid)
        {
            return View("RegisterPatient", model);
        }
        var patient = new Patient
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Address = model.Address,
            HasInsurance = model.HasInsurance,
            DateOfBirth = model.DateOfBirth
        };

        var result = await userManager.CreateAsync(patient, model.Password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(patient, RoleNames.DoctorRoleName);

            return RedirectToAction("Login");
        }

        foreach (var item in result.Errors)
        {
            ModelState.AddModelError("", item.Description);
        }

        return View("RegisterPatient", model);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult RegisterDoctor(RegisterViewModel model)
    {
        var doctorModel = new DoctorRegistrationViewModel() 
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Address = model.Address,
            Password = model.Password, 
            PasswordRepeat = model.PasswordRepeat,
            SelectedRole = model.SelectedRole
        };
        return View(doctorModel);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> RegisterDoctor(DoctorRegistrationViewModel model, List<IFormFile> files)
    {
        if (!ModelState.IsValid)
        {
            return View("RegisterDoctor", model);
        }
        var doctor = new Doctor
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            UserName = model.Email,
            Address = model.Address,
            LicenseNumber = model.LicenseNumber,
            Biography = model.Biography,
            OfficePhoneNumber = model.OfficePhoneNumber,
            PricePerAppointment = model.PricePerAppointment,
            ProfilePictureFilename = model.Files.FileName
        };

        var result = await userManager.CreateAsync(doctor, model.Password);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(doctor, RoleNames.DoctorRoleName);

            await ProcessFileUploadsAsync(files);
            
            return RedirectToAction("Login");
        }

        foreach (var item in result.Errors)
        {
            ModelState.AddModelError("", item.Description);
        }

        return View("RegisterDoctor", model);
    }


    /// <summary>
    /// Submits the form
    /// </summary>
    /// <param name="returnUrl">The return path you are sent when you authenticate</param>
    /// <returns></returns>
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

    /// <summary>
    /// Submits the login form
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
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

    /// <summary>
    /// Log outs the user
    /// </summary>
    /// <returns></returns>
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// Created roles
    /// </summary>
    /// <returns></returns>
    private async Task ProcessFileUploadsAsync(List<IFormFile> files)
    {
        foreach (var file in files)
        {
            if (file != null && file.Length > 0)
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, directoryPath, file.FileName);

                using var stream = System.IO.File.Create(filePath);
                await file.CopyToAsync(stream);
            }
        }
    }
}


