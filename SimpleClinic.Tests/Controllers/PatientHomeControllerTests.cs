namespace SimpleClinic.Tests.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;
using NUnit.Framework;

using SimpleClinic.Areas.Patient.Controllers;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Infrastructure.Entities;
using System.Security.Claims;

public class PatientHomeControllerTests
{
    private HomeController controller;
    private Mock<UserManager<ApplicationUser>> mockUserManager;
    private  Mock<IAccountService> mockAccountService;

    [SetUp]
    public void Setup() 
    {
        mockAccountService = new Mock<IAccountService>();

        mockUserManager = new Mock<UserManager<ApplicationUser>>(
            new Mock<IUserStore<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<ApplicationUser>>().Object,
            new IUserValidator<ApplicationUser>[0],
            new IPasswordValidator<ApplicationUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<ApplicationUser>>>().Object);

        mockUserManager
            .Setup(userManager => userManager.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
            .Returns(Task.FromResult(IdentityResult.Success));
        mockUserManager
            .Setup(userManager => userManager.AddToRoleAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()));

        controller = new HomeController(mockAccountService.Object, mockUserManager.Object);

        controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Test]
    public async Task Index_ValidModel_RedirectsToAction()
    {
        var userId = "testUserId";
        var nextOfKinViewModel = new NextOfKinViewModel
        {
            FormsCompleted = true,
            Name = "someguysname",
            PhoneNumber = "4232145423",
            Address = "someguysaddress",
            PatientId = userId
        };
        mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
            .Returns(userId);
        mockAccountService.Setup(service => service.AddNextOfKin(It.IsAny<NextOfKinViewModel>(), userId))
            .Returns(Task.CompletedTask);


        var result = await controller.Index(nextOfKinViewModel) as RedirectToActionResult;

        Assert.IsInstanceOf<RedirectToActionResult>(result);
        Assert.AreEqual("AddMedicalHistory", result.ActionName);
        Assert.AreEqual("Home", result.ControllerName);
    }

    [Test]
    public async Task Index_InvalidModel_ReturnsViewWithModel()
    {
        var userId = "testUserId";
        var nextOfKinViewModel = new NextOfKinViewModel
        {
            FormsCompleted = true,
            Name = "someguysname",
            PhoneNumber = "4232145423",
            Address = "someguysaddress",
            PatientId = userId
        };

        mockUserManager.Setup(um => um.GetUserId(It.IsAny<ClaimsPrincipal>()))
            .Returns(userId);

        controller.ModelState.AddModelError("key", "error message");

        var result = await controller.Index(nextOfKinViewModel) as ViewResult;

        Assert.IsInstanceOf<ViewResult>(result);
        Assert.AreEqual(nextOfKinViewModel, result.Model);
        Assert.AreEqual(null, result.ViewName);
    }

}
