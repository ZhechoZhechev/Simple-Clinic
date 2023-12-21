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

        Assert.Equals(servicesModel.TotalServicesCount, totalServicesCount);
        Assert.Equals(servicesModel.Services[3].Name, services[0].Name);
        Assert.Equals(servicesModel.Services[4].Name, services[1].Name);
        Assert.Equals(servicesModel.Services[5].Name, service.Name);
    }

    [Test]
    public async Task GetAllServicesForSchedule_Returns_Correct_Collection()
    {
        var testSerices = await context.Services.ToListAsync();

        var actualResult = await serviceService.GetAllServicesForSchedule();

        Assert.That(actualResult, Is.Not.EqualTo(null));
        Assert.Equals(testSerices.Count, actualResult.Count);
        Assert.Equals(testSerices[3].Id, actualResult[3].Id);
        Assert.Equals(testSerices[3].Name, actualResult[3].ServiceName);
        Assert.Equals(testSerices[4].Id, actualResult[4].Id);
        Assert.Equals(testSerices[4].Name, actualResult[4].ServiceName);
        Assert.Equals(testSerices[5].Id, actualResult[5].Id);
        Assert.Equals(testSerices[5].Name, actualResult[5].ServiceName);
    }

    [Test]
    public async Task GetFirstThreeServices_Returns_Correct_Collection()
    {
        var testSerices = await context.Services.Take(3).ToListAsync();

        var actualResult = await serviceService.GetFirstThreeServices();

        Assert.That(actualResult, Is.Not.EqualTo(null));
        Assert.Equals(testSerices.Count, actualResult.Count);
        Assert.Equals(testSerices[0].Price, actualResult[0].Price);
        Assert.Equals(testSerices[0].Name, actualResult[0].Name);
        Assert.Equals(testSerices[0].EquipmentPicture, actualResult[0].EquipmentPicture);
        Assert.Equals(testSerices[1].Price, actualResult[1].Price);
        Assert.Equals(testSerices[1].Name, actualResult[1].Name);
        Assert.Equals(testSerices[1].EquipmentPicture, actualResult[1].EquipmentPicture);
        Assert.Equals(testSerices[2].Price, actualResult[2].Price);
        Assert.Equals(testSerices[2].Name, actualResult[2].Name);
        Assert.Equals(testSerices[2].EquipmentPicture, actualResult[2].EquipmentPicture);

    }
}
