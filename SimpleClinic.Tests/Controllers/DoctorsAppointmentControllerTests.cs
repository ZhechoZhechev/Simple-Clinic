namespace SimpleClinic.Tests.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using Moq;
using NUnit.Framework;

using SimpleClinic.Areas.Doctor.Controllers;
using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure.Entities;
using System.Security.Claims;

[TestFixture]
internal class DoctorsAppointmentControllerTests
{
    private AppointmentController controller;
    private Mock<IAppointmentService> mockAppointmentService;
    private Mock<UserManager<ApplicationUser>> mockUserManager;

    [SetUp]
    public void Setup()
    {
        mockAppointmentService = new Mock<IAppointmentService>();

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

        controller = new AppointmentController(mockUserManager.Object,
            mockAppointmentService.Object);

        controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Test]
    public async Task GetPatientAppointments_Returns_View_With_Model() 
    {
        var doctorId = "testdocid";
        var doctorUser = new ApplicationUser { Id = doctorId };
        var expectedModel = new List<PatientAppointmentViewModel>()
        {
            new PatientAppointmentViewModel
            {
                Id = "someappid",
                PatientFirstName = "testname",
                PatientLastName = "testname",
                PatientEmail = "testemail",
                BookingDate = new DateTime(2023, 11, 8, 0, 0, 0),
                StartTime = new DateTime(2023, 11, 8, 8, 0, 0),
                EndTime = new DateTime(2023, 11, 8, 9, 0, 0)
            },
            new PatientAppointmentViewModel
            {
                Id = "someappid1",
                PatientFirstName = "testname1",
                PatientLastName = "testname1",
                PatientEmail = "testemail1",
                BookingDate = new DateTime(2023, 12, 8, 0, 0, 0),
                StartTime = new DateTime(2023, 11, 8, 8, 0, 0),
                EndTime = new DateTime(2023, 11, 8, 9, 0, 0)
            },
        };
        mockAppointmentService.Setup(s => s.GetPatientAppointmentsForDoctor(doctorId))
            .ReturnsAsync(expectedModel);
        mockUserManager.Setup(u => u.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                  .ReturnsAsync(doctorUser);

        var result = await controller.GetPatientAppointments() as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ViewResult>(result);

        var model = result.Model as List<PatientAppointmentViewModel>;
        Assert.AreEqual(expectedModel, model);
    }

    [Test]
    public async Task CancelPatientAppointment_Returns_RedirectToAction() 
    {
        var appId = "someapid";
        mockAppointmentService.Setup(a => a.CancelPatientAppointment(appId))
            .Returns(Task.CompletedTask);


        var result = await controller.CancelPatientAppointment(appId) as RedirectToActionResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("GetPatientAppointments", result.ActionName);
        Assert.AreEqual("Appointment", result.ControllerName);
        Assert.AreEqual(RoleNames.DoctorRoleName, result.RouteValues["area"]);
    }


    [Test]
    public async Task CancelPatientAppointment_Error_Returns_RedirectToAction() 
    {
        var appId = "someapid";
        mockAppointmentService.Setup(a => a.CancelPatientAppointment(appId))
            .Throws(new Exception());

        var result = await controller.CancelPatientAppointment(appId) as RedirectToActionResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("Index", result.ActionName);
        Assert.AreEqual("Home", result.ControllerName);
        Assert.AreEqual(RoleNames.DoctorRoleName, result.RouteValues["area"]);

    }
}
