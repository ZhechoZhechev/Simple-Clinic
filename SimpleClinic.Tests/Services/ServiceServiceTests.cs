namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure;
using static DatabaseSeeder;

[TestFixture]
internal class ServiceServiceTests
{
    private DbContextOptions<SimpleClinicDbContext> dbContextOptions;
    private SimpleClinicDbContext context;
    private ServiceService serviceService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        dbContextOptions = new DbContextOptionsBuilder<SimpleClinicDbContext>()
            .UseInMemoryDatabase(databaseName: "SimpleClinicInMemory" + Guid.NewGuid().ToString())
            .Options;

        context = new SimpleClinicDbContext(dbContextOptions);

        context.Database.EnsureCreated();

        SeedDatabase(this.context);

        serviceService = new ServiceService(context);
    }

    [Test]
    public async Task All_Returns_Correct_Collection()
    {
        var currentPage = 1;
        var servicePerPage = 6;
        var totalServicesCount = context.Services.Count();

        var servicesModel = await serviceService.All(currentPage, servicePerPage);

        Assert.That(servicesModel.TotalServicesCount, Is.EqualTo(totalServicesCount));
        Assert.That(servicesModel.Services[3].Name, Is.EqualTo(services[0].Name));
        Assert.That(servicesModel.Services[4].Name, Is.EqualTo(services[1].Name));
        Assert.That(servicesModel.Services[5].Name, Is.EqualTo(service.Name));
    }

    [Test]
    public async Task GetAllServicesForSchedule_Returns_Correct_Collection()
    {
        var testSerices = await context.Services.ToListAsync();

        var actualResult = await serviceService.GetAllServicesForSchedule();

        Assert.That(actualResult, Is.Not.EqualTo(null));
        Assert.That(testSerices.Count, Is.EqualTo(actualResult.Count));
        Assert.That(testSerices[3].Id, Is.EqualTo(actualResult[3].Id));
        Assert.That(testSerices[3].Name, Is.EqualTo(actualResult[3].ServiceName));
        Assert.That(testSerices[4].Id, Is.EqualTo(actualResult[4].Id));
        Assert.That(testSerices[4].Name, Is.EqualTo(actualResult[4].ServiceName));
        Assert.That(testSerices[5].Id, Is.EqualTo(actualResult[5].Id));
        Assert.That(testSerices[5].Name, Is.EqualTo(actualResult[5].ServiceName));
    }

    [Test]
    public async Task GetFirstThreeServices_Returns_Correct_Collection()
    {
        var testSerices = await context.Services.Take(3).ToListAsync();

        var actualResult = await serviceService.GetFirstThreeServices();

        Assert.That(actualResult, Is.Not.EqualTo(null));
        Assert.That(testSerices.Count, Is.EqualTo(actualResult.Count));
        Assert.That(testSerices[0].Price, Is.EqualTo(actualResult[0].Price));
        Assert.That(testSerices[0].Name, Is.EqualTo(actualResult[0].Name));
        Assert.That(testSerices[0].EquipmentPicture, Is.EqualTo(actualResult[0].EquipmentPicture));
        Assert.That(testSerices[1].Price, Is.EqualTo(actualResult[1].Price));
        Assert.That(testSerices[1].Name, Is.EqualTo(actualResult[1].Name));
        Assert.That(testSerices[1].EquipmentPicture, Is.EqualTo(actualResult[1].EquipmentPicture));
        Assert.That(testSerices[2].Price, Is.EqualTo(actualResult[2].Price));
        Assert.That(testSerices[2].Name, Is.EqualTo(actualResult[2].Name));
        Assert.That(testSerices[2].EquipmentPicture, Is.EqualTo(actualResult[2].EquipmentPicture));

    }
}
