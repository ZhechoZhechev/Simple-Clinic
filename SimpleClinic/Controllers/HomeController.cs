namespace SimpleClinic.Controllers;

using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using MimeKit;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;
using static SimpleClinic.Common.ExceptionMessages.NotificationMessages;
using static SimpleClinic.Common.Constants.GeneralApplicationConstants;

/// <summary>
/// Responsible for home page for unregistrated users
/// </summary>
public class HomeController : Controller
{
    private readonly ISpecialityService specialityService;
    private readonly IDoctorService doctorService;
    private readonly IServiceService serviceService;
    private readonly IConfiguration configuration;
    private readonly IMemoryCache memoryCache;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="specialityService">dependancy</param>
    /// <param name="doctorService">dependancy</param>
    /// <param name="serviceService">dependancy</param>
    public HomeController(
        ISpecialityService specialityService,
        IDoctorService doctorService,
        IServiceService serviceService,
        IConfiguration configuration,
        IMemoryCache memoryCache)
    {
        this.specialityService = specialityService;
        this.doctorService = doctorService;
        this.serviceService = serviceService;
        this.configuration = configuration;
        this.memoryCache = memoryCache;
    }
    /// <summary>
    /// Home page
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    /// <summary>
    /// Departments page
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Departments()
    {
        return View();
    }

    /// <summary>
    /// Contacts page
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public IActionResult Contacts()
    {
        return View();
    }

    /// <summary>
    /// Available services
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Services()
    {
        try
        {
            var model = await serviceService.GetFirstThreeServices();
            return View(model);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home");
        }
    }

    /// <summary>
    /// Registered doctors
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Doctors()
    {
        try
        {
            var model = await doctorService.GetFirstThreeDoctors();
            return View(model);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home");
        }

    }
    /// <summary>
    /// Get doctor details
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> DoctorDetails(string Id)
    {
        var result = await doctorService.DoctorExistsById(Id);

        if (!result)
        {
            TempData[ErrorMessage] = "Docotor with such ID does not exist!";
            return RedirectToAction("Doctors", "Home");
        }

        try
        {
            var model = memoryCache.Get<DoctorDetailsViewModel>(DoctorDetailsCacheKey);

            if (model == null)
            {
                model = await doctorService.DoctorDetails(Id);
                
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(DocDetailsCacheExpTime));

                memoryCache.Set(DoctorDetailsCacheKey, model, cacheOptions);
            }

            return View(model);

        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home");
        }

    }


    /// <summary>
    /// Available departments
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> AllDepartments()
    {
        try
        {
            var model = memoryCache.Get<IEnumerable<SpecialityViewModel>>(AllDepsMemoryCacheKey);

            if (model == null)
            {
                model = await specialityService.GetAllSpecialitiesWithDoctorsCount();

                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromMinutes(AllDepsMemoryCacheExpTime));

                memoryCache.Set(AllDepsMemoryCacheKey, model, cacheOptions);
            }

            model = await specialityService.GetAllSpecialitiesWithDoctorsCount();
            return View(model);
        }
        catch (Exception)
        {
            TempData[ErrorMessage] = "Something went wrong!";
            return RedirectToAction("Error", "Home");
        }

    }

    /// <summary>
    /// Send email for endpoint
    /// </summary>
    [HttpPost]
    public IActionResult SendEmail(string email, string name, string phone, string message)
    {
        try
        {
            var smtpConfig = configuration.GetSection("Smtp");
            var smtpHost = smtpConfig["Host"];
            var smtpPort = int.Parse(smtpConfig["Port"]);
            var smtpUsername = smtpConfig["Username"];
            var smtpPassword = smtpConfig["Password"];

            var messageBody = $"Email: {email}\nName: {name}\nPhone: {phone}\nMessage: {message}";

            var messageToSend = new MimeMessage();
            messageToSend.From.Add(new MailboxAddress("", email));
            messageToSend.To.Add(new MailboxAddress("", smtpUsername));
            messageToSend.Subject = $"New message from user {name}";
            messageToSend.Body = new TextPart("plain")
            {
                Text = messageBody
            };

            using (var smtpClient = new SmtpClient())
            {
                smtpClient.Connect(smtpHost, smtpPort, SecureSocketOptions.Auto);
                smtpClient.Authenticate(smtpUsername, smtpPassword);
                smtpClient.Send(messageToSend);
                smtpClient.Disconnect(true);
            }

            TempData[SuccessMessage] = "You email, has been sent successfully. We will get back you soon!";
            return RedirectToAction("Contacts", "Home");
        }
        catch (Exception ex)
        {
            TempData[ErrorMessage] = ex.ToString();
            return RedirectToAction("Error", "Home");
        }
    }

    /// <summary>
    /// Error handler
    /// </summary>
    /// <param name="statusCode">status code</param>
    /// <returns></returns>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(int statusCode)
    {
        if (statusCode == 400 || statusCode == 404)
        {
            return this.View("Error404");
        }

        if (statusCode == 401)
        {
            return this.View("Error401");
        }

        return this.View();
    }
}