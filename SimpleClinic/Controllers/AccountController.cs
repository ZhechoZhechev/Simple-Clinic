namespace SimpleClinic.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleClinic.Common;
using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure.Entities;

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

    public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager,
        IConfiguration configuration)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
        this.directoryPath = configuration["UpploadSettings:ImageDir"];
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

        //var result = await userManager.CreateAsync(user, model.Password);


        //if (result.Succeeded)
        //{
        //    await signInManager.SignInAsync(user, isPersistent: false);

        //    return RedirectToAction("Index", "Home");
        //}

        //foreach (var item in result.Errors)
        //{
        //    ModelState.AddModelError("", item.Description);
        //}


        return View(model);
    }

    //[HttpPost]
    //public IActionResult RegisterPatient(PatientRegistrationViewModel model)
    //{
    //    if (ModelState.IsValid)
    //    {
    //        // Create a new Patient object and map the properties from the registration model
    //        var patient = new Patient
    //        {
    //            FirstName = model.FirstName,
    //            LastName = model.LastName,
    //            Email = model.Email,
    //            Address = model.Address,
    //            Password = model.Password,
    //            HasInsurance = model.HasInsurance,
    //            DateOfBirth = model.DateOfBirth
    //        };

    //        // Save the patient to the database or perform other necessary operations

    //        return RedirectToAction("Login");
    //    }

    //    // If the model is not valid, return the patient registration view with the model
    //    return View("RegisterPatient", model);
    //}

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
            SelectedRole = model.SelectedRole,
            
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
        // Create a new Doctor object and map the properties from the registration model
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
            PricePerAppointment = model.PricePerAppointment
        };

        var result = await userManager.CreateAsync(doctor, model.Password);
        // Save the doctor to the database or perform other necessary operations
        if (result.Succeeded)
        {
            // Assign the "Doctor" role to the newly created doctor user
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
    public async Task<IActionResult> CreateRoles()
    {
        await roleManager.CreateAsync(new IdentityRole(RoleNames.AdminRoleName));
        await roleManager.CreateAsync(new IdentityRole(RoleNames.DoctorRoleName));
        await roleManager.CreateAsync(new IdentityRole(RoleNames.PatientRoleName));

        return RedirectToAction("Index", "Home");
    }

    //public async Task<IActionResult> AddUsersToRoles()
    //{
    //    string email1 = "stamo.petkov@gmail.com";
    //    string email2 = "pesho@abv.bg";

    //    var user = await userManager.FindByEmailAsync(email1);
    //    var user2 = await userManager.FindByEmailAsync(email2);

    //    await userManager.AddToRoleAsync(user, RoleConstants.Manager);
    //    await userManager.AddToRolesAsync(user2, new string[] { RoleConstants.Supervisor, RoleConstants.Manager });

    //    return RedirectToAction("Index", "Home");
    //}

    private async Task ProcessFileUploadsAsync(List<IFormFile> files)
    {
        foreach (var file in files)
        {
            if (file != null && file.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(directoryPath, uniqueFileName);

                using var stream = System.IO.File.Create(filePath);
                await file.CopyToAsync(stream);
            }
        }
    }
}


