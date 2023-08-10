namespace SimpleClinic.Tests.Controllers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;

using Moq;
using NUnit.Framework;

using SimpleClinic.Controllers;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;

[TestFixture]
public class HomeControllerTests
{
    private HomeController controller;
    private Mock<ISpecialityService> mockSpecialityService;
    private Mock<IDoctorService> mockDoctorService;
    private Mock<IServiceService> mockServiceService;
    private Mock<IConfiguration> mockConfiguration;

    [SetUp]
    public void Setup()
    {
        mockSpecialityService = new Mock<ISpecialityService>();
        mockDoctorService = new Mock<IDoctorService>();
        mockServiceService = new Mock<IServiceService>();
        mockConfiguration = new Mock<IConfiguration>();

        controller = new HomeController(
            mockSpecialityService.Object,
            mockDoctorService.Object,
            mockServiceService.Object,
            mockConfiguration.Object);

        controller.TempData = new TempDataDictionary(new DefaultHttpContext(), Mock.Of<ITempDataProvider>());
    }

    [Test]
    public void Index_Returns_ViewResult()
    {

        var result = controller.Index();


        Assert.IsInstanceOf<ViewResult>(result);
    }

    [Test]
    public void Departments_Returns_ViewResult()
    {

        var result = controller.Departments();


        Assert.IsInstanceOf<ViewResult>(result);
    }

    [Test]
    public void Contacts_Returns_ViewResult()
    {

        var result = controller.Contacts();


        Assert.IsInstanceOf<ViewResult>(result);
    }

    [Test]
    public async Task Services_Returns_View_With_Model()
    {
        var expectedModel = new List<FirstThreeServicesViewModel>
    {
        new FirstThreeServicesViewModel { Name = "Service 1", EquipmentPicture = "picture1", Price = 200 },
        new FirstThreeServicesViewModel {Name = "Service 2", EquipmentPicture = "picture2", Price = 300  },
        new FirstThreeServicesViewModel {Name = "Service 3", EquipmentPicture = "picture3", Price = 400  }
    };

        mockServiceService.Setup(s => s.GetFirstThreeServices())
            .ReturnsAsync(expectedModel);

        var result = await controller.Services() as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ViewResult>(result);

        var model = result.Model as List<FirstThreeServicesViewModel>;
        Assert.AreEqual(expectedModel, model);
    }

    [Test]
    public async Task Services_Redirects_To_Error_Page() 
    {
        mockServiceService.Setup(s => s.GetFirstThreeServices())
            .ThrowsAsync(new Exception());

        var result = await controller.Services() as RedirectToActionResult; 

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        Assert.AreEqual("Home", result.ControllerName);
        Assert.AreEqual("Error", result.ActionName);
    }

    [Test]
    public async Task Doctors_Returns_View_With_Model()
    {
        var expectedModel = new List<FirstThreeDoctorsViewModel>
    {
        new FirstThreeDoctorsViewModel { FirstName = "Doc1", LastName = "Doc1", ProfilePictureFilename = "doc1.jpg", Speciality ="doc1Speciality" },
        new FirstThreeDoctorsViewModel { FirstName = "Doc2", LastName = "Doc2", ProfilePictureFilename = "doc2.jpg", Speciality ="doc2Speciality" },
        new FirstThreeDoctorsViewModel { FirstName = "Doc3", LastName = "Doc3", ProfilePictureFilename = "doc3.jpg", Speciality ="doc3Speciality" }
    };

        mockDoctorService.Setup(s => s.GetFirstThreeDoctors())
            .ReturnsAsync(expectedModel);

        var result = await controller.Doctors() as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ViewResult>(result);

        var model = result.Model as List<FirstThreeDoctorsViewModel>;
        Assert.AreEqual(expectedModel, model);
    }

    [Test]
    public async Task Doctors_Redirects_To_Error_Page() 
    {
        mockDoctorService.Setup(d => d.GetFirstThreeDoctors())
            .ThrowsAsync(new Exception());

        var result = await controller.Doctors() as RedirectToActionResult; 
        
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        Assert.AreEqual("Home", result.ControllerName);
        Assert.AreEqual("Error", result.ActionName);
    }

    [Test]
    public async Task DoctorDetails_Existing_Doctor_Returns_ViewWith_Model()
    {
        var expectedModel = new DoctorDetailsViewModel()
        {
            Id = "Doc1Id",
            FirstName = "Doc1",
            LastName = "Doc1",
            ProfilePictureFilename = "doc1.jpg",
            Speciality = "doc1Speciality",
            ShortBio = "some short bio",
            PricePerHour = "666"
        };
        mockDoctorService.Setup(d => d.DoctorExistsById("Doc1Id")).ReturnsAsync(true);
        mockDoctorService.Setup(d => d.DoctorDetails("Doc1Id")).ReturnsAsync(expectedModel);

        var result = await controller.DoctorDetails("Doc1Id") as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ViewResult>(result);

        var model = result.Model as DoctorDetailsViewModel;
        Assert.IsNotNull(model);
        Assert.AreEqual(expectedModel, model);
    }

    [Test]
    public async Task DoctorDetails_Non_Existing_Doctor_Redirects_To_Doctors()
    {
        mockDoctorService.Setup(d => d.DoctorExistsById("nosuchId")).ReturnsAsync(false);

        var result = await controller.DoctorDetails("nosuchId") as RedirectToActionResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        Assert.AreEqual("Home", result.ControllerName);
        Assert.AreEqual("Doctors", result.ActionName);
    }

    [Test]
    public async Task DoctorDetails_Exception_Redirects_To_Error() 
    {
        mockDoctorService.Setup(d => d.DoctorExistsById("existingIdThrowingError")).ReturnsAsync(true);
        mockDoctorService.Setup(d => d.DoctorDetails("existingIdThrowingError")).ThrowsAsync(new Exception());

        var result = await controller.DoctorDetails("existingIdThrowingError") as RedirectToActionResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        Assert.AreEqual("Home", result.ControllerName);
        Assert.AreEqual("Error", result.ActionName);
    }

    [Test]
    public async Task AllDepartments_Returns_View_With_Model()
    {
        var expectedModel = new List<SpecialityViewModel>
        {
            new SpecialityViewModel {Id = 1, Name = "SpecName1", DoctorsCount = 3},
            new SpecialityViewModel {Id = 2, Name = "SpecName2", DoctorsCount = 4},
            new SpecialityViewModel {Id = 3, Name = "SpecName3", DoctorsCount = 5},
        };
        mockSpecialityService.Setup(s => s.GetAllSpecialitiesWithDoctorsCount()).ReturnsAsync(expectedModel);

        var result = await controller.AllDepartments() as ViewResult;

        Assert.IsNotNull(result);
        Assert.IsInstanceOf<ViewResult>(result);

        var model = result.ViewData.Model as List<SpecialityViewModel>;
        Assert.IsNotNull(model);
        CollectionAssert.AreEqual(expectedModel, model);
    }

    [Test]
    public async Task AllDepartments_Redirects_To_Error_Page() 
    {
        mockSpecialityService.Setup(s => s.GetAllSpecialitiesWithDoctorsCount())
            .ThrowsAsync(new Exception());

        var result = await controller.AllDepartments() as RedirectToActionResult; 
        
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<RedirectToActionResult>(result);
        Assert.AreEqual("Home", result.ControllerName);
        Assert.AreEqual("Error", result.ActionName);
    }

    [Test]
    [TestCase(400, "Error404")]
    [TestCase(404, "Error404")]
    [TestCase(401, "Error401")]
    public void Error_ReturnsCorrectView(int statusCode, string expectedViewName)
    {

        var result = controller.Error(statusCode) as ViewResult;

        Assert.IsNotNull(result);
        Assert.AreEqual(expectedViewName, result.ViewName);
    }
}