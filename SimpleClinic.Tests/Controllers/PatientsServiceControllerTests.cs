namespace SimpleClinic.Tests.Controllers;

using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SimpleClinic.Areas.Patient.Controllers;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;

[TestFixture]
internal class PatientsServiceControllerTests
{
    private Mock<IServiceService> mockServiceService;
    private ServiceController controller;

    [SetUp]
    public void Setup() 
    {
        mockServiceService = new Mock<IServiceService>();
        controller = new ServiceController(mockServiceService.Object);
    }

    [Test]
    public async Task All_Returns_View_With_Model() 
    {
        var currentPage = 1;
        var servicesPerPage = 3;
        var expectedTotalServicesCount = 3;
        var expectedServices = new List<ServiceServiceModel>
        {
            new ServiceServiceModel()
            {
                Id = "serviceid1",
                Name = "servicename1",
                EquipmentPicture = "servicepicture1.jpg",
                Price = "300"
            },
            new ServiceServiceModel()
            {
                Id = "serviceid2",
                Name = "servicename2",
                EquipmentPicture = "servicepicture2.jpg",
                Price = "333"
            },
        };

        mockServiceService.Setup(s => s.All(currentPage, servicesPerPage))
            .ReturnsAsync(new ServiceQueryServiceModel
            {
                TotalServicesCount = expectedTotalServicesCount,
                Services = expectedServices
            });

        var queryModel = new AllServicesPaginationModel
        {
            CurrentPage = currentPage,
            ServicesPerPage = servicesPerPage
        };

        var result = await controller.All(queryModel) as ViewResult;
        var model = result.Model as AllServicesPaginationModel;

        Assert.That(result, Is.InstanceOf<ViewResult>());
        Assert.That(model, Is.Not.EqualTo(null));
        Assert.Equals(expectedTotalServicesCount, model!.TotalServicesCount);
        Assert.Equals(expectedServices, model.Services);
    }
}
