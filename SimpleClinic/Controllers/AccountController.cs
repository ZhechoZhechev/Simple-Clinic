namespace SimpleClinic.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure.Entities;


[Authorize]
public class AccountController : Controller
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly RoleManager<IdentityRole> roleManager;
    public AccountController(UserManager<ApplicationUser> userManager,
        SignInManager<ApplicationUser> signInManager,
        RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.signInManager = signInManager;
        this.roleManager = roleManager;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login()
    {
        if (!User?.Identity?.IsAuthenticated == false)
            return RedirectToAction("Index", "Home");

        var model = new LoginViewModel();

        if (TempData.ContainsKey("RegisterSuccess"))
            model.RegisterSuccess = Convert.ToBoolean(TempData["RegisterSuccess"]);

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Login(LoginViewModel user)
    {
        if (ModelState.IsValid)
        {
            var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, false, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Home");

            ModelState.AddModelError("LoginError", "Invalid login attempt");

        }
        return View(user);
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register()
    {
        if (!User?.Identity?.IsAuthenticated == false)
            return RedirectToAction("Index", "Home");

        var model = new RegisterViewModel();

        return View(model);
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser()
            {
                Email = model.Email,
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = true,
                Address = model.Address
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);

                return View(model);
            }


            TempData["registerSuccess"] = true;

            return RedirectToAction("Login", "Account");
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();

        return RedirectToAction("Login", "Account");
    }
}


