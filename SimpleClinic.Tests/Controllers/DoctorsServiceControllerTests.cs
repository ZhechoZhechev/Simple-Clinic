namespace SimpleClinic.Tests.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

using Moq;
using NUnit.Framework;

using SimpleClinic.Areas.Doctor.Controllers;
using SimpleClinic.Common;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;

[TestFixture]
internal class DoctorsServiceControllerTests
{
    private ServiceController controller;
    private Mock<IServiceService> mockServiceService;
    private Mock<IScheduleService> mockScheduleService;
    private DoctorScheduleViewModel doctorScheduleViewModel;

    [SetUp]
    public void Setup()
    {
        doctorScheduleViewModel = new DoctorScheduleViewModel
        {
            Day = new DateTime(2024, 10, 23),
            ServiceId = "testServiceId",
            TimeSlots = new List<TimeSlotViewModel>()
        };

        mockScheduleService = new Mock<IScheduleService>();
        mockServiceService = new Mock<IServiceService>();

        controller = new ServiceController(
            mockServiceService.Object,
            mockScheduleService.Object);

        controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Test]
    public async Task AllServicesForSchedule_Returns_View_With_Model()
    {
        var expectedModel = new List<AllServicesForScheduleViewModel>()
        {
            new AllServicesForScheduleViewModel()
            {
                Id = "serviceid1",
                ServiceName = "testservicename1"
            },
            new AllServicesForScheduleViewModel()
            {
                Id = "serviceid2",
                ServiceName = "testservicename2"
            }
        };
        mockServiceService.Setup(s => s.GetAllServicesForSchedule())
            .ReturnsAsync(expectedModel);

        var result = await controller.AllServicesForSchedule() as ViewResult;
        var model = result.Model as List<AllServicesForScheduleViewModel>;


        Assert.That(result, Is.Not.EqualTo(null));
        Assert.That(result, Is.InstanceOf<ViewResult>());
        Assert.That(expectedModel, Is.EqualTo(model));
    }

    [Test]
    public void AddSchedule_Returns_View_With_Model_And_TempData()
    {
        var serviceName = "TestService";
        var id = "1";
        var expectedModel = new DoctorScheduleViewModel
        {
            ServiceId = id
        };

        var result = controller.AddSchedule(serviceName, id) as ViewResult;
        var model = result!.Model as DoctorScheduleViewModel;
        var tempDataValue = controller.TempData["CurrServiceName"] as string;


        Assert.That(result, Is.Not.EqualTo(null));
        Assert.That(result, Is.InstanceOf<ViewResult>());
        Assert.That(expectedModel.ServiceId, Is.EqualTo(model!.ServiceId));
        Assert.That(serviceName, Is.EqualTo(tempDataValue));
    }

    [Test]
    public async Task AddSchedule_Returns_RedirectToAction_When_Schedule_Exists()
    {

        mockScheduleService.Setup(s => s.IfDayServiceScheduleExists(doctorScheduleViewModel.Day, doctorScheduleViewModel.ServiceId!))
            .ReturnsAsync(true);

        var result = await controller.AddSchedule(doctorScheduleViewModel) as RedirectToActionResult;

        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        Assert.That("AddSchedule", Is.EqualTo(result!.ActionName));
        Assert.That("Service", Is.EqualTo(result.ControllerName));
        Assert.That(RoleNames.DoctorRoleName, Is.EqualTo(result.RouteValues!["area"]));
        Assert.That("Schedule for this day exists. Please, select different day.", Is.EqualTo(controller.TempData["ErrorMessage"]));
    }

    [Test]
    public async Task AddSchedule_Returns_View_Model_When_ModelState_Is_Invalid()
    {

        controller.ModelState.AddModelError("Day", "Day is required");

        var result = await controller.AddSchedule(doctorScheduleViewModel) as ViewResult;

        Assert.That(doctorScheduleViewModel, Is.EqualTo(result!.Model));
    }

    [Test]
    public async Task AddSchedule_Returns_RedirectToAction_When_Schedule_NonExistant_With_Success_Message()
    {
        mockScheduleService.Setup(s => s.IfDayServiceScheduleExists(doctorScheduleViewModel.Day, doctorScheduleViewModel.ServiceId!))
            .ReturnsAsync(false);

        var result = await controller.AddSchedule(doctorScheduleViewModel) as RedirectToActionResult;

        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        Assert.That("AddSchedule", Is.EqualTo(result!.ActionName));
        Assert.That("Service", Is.EqualTo(result.ControllerName));
        Assert.That(RoleNames.DoctorRoleName, Is.EqualTo(result.RouteValues!["area"]));
        Assert.That("Schedule added successfully!", Is.EqualTo(controller.TempData["SuccessMessage"]));
    }
    [Test]
    public async Task AddSchedule_Returns_RedirectToAction_When_Error_With_Success_Message() 
    {
        mockScheduleService.Setup(s => s.IfDayServiceScheduleExists(doctorScheduleViewModel.Day, doctorScheduleViewModel.ServiceId!))
           .ReturnsAsync(false);
        mockScheduleService.Setup(s => s.AddServiceScheduleAsync(doctorScheduleViewModel.ServiceId!, doctorScheduleViewModel.Day, doctorScheduleViewModel.TimeSlots))
            .Throws(new Exception());

        var result = await controller.AddSchedule(doctorScheduleViewModel) as RedirectToActionResult;

        Assert.That(result, Is.InstanceOf<RedirectToActionResult>());
        Assert.That("Index", Is.EqualTo(result!.ActionName));
        Assert.That("Home", Is.EqualTo(result.ControllerName));
        Assert.That(RoleNames.DoctorRoleName, Is.EqualTo(result.RouteValues!["area"]));
        Assert.That("Something went wrong!", Is.EqualTo(controller.TempData["ErrorMessage"]));
    }

    [Test]
    public async Task CheckSchedule_Returns_View_With_Model() 
    {
        var serviceId = "someserviceid";
        var expectedModel = new List<DayScheduleViewModel>()
        {
            new DayScheduleViewModel
            {
                Id = "someid1",
                Day = new DateTime(2024, 11, 20),
                TimeSlots = new List<TimeSlotViewModel>()
            },
             new DayScheduleViewModel
            {
                Id = "someid2",
                Day = new DateTime(2024, 12, 20),
                TimeSlots = new List<TimeSlotViewModel>()
            }
        };
        mockScheduleService.Setup(s => s.CheckServiceSchedule(serviceId))
            .ReturnsAsync(expectedModel);

        var result = await controller.CheckSchedule(serviceId) as ViewResult;
        var model = result!.Model;

        Assert.That(result, Is.Not.EqualTo(null));
        Assert.That(result, Is.InstanceOf<ViewResult>());
        Assert.That(expectedModel, Is.EqualTo(model));
    }
}
