namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure;
using static DatabaseSeeder;


[TestFixture]
internal class PatientServiceTests
{
    private DbContextOptions<SimpleClinicDbContext> dbContextOptions;
    private SimpleClinicDbContext context;
    private PatientService patientService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        dbContextOptions = new DbContextOptionsBuilder<SimpleClinicDbContext>()
            .UseInMemoryDatabase(databaseName: "SimpleClinicInMemory" + Guid.NewGuid().ToString())
            .Options;

        context = new SimpleClinicDbContext(dbContextOptions);

        context.Database.EnsureCreated();

        SeedDatabase(this.context);

        patientService = new PatientService(context);
    }

    [Test]
    public async Task GetAllPatients_ReturnsCorrectPatients_PerSearchTerm()
    {
        var searchTerm = "esho";
        var currentPage = 1;
        var doctorsPerPage = 1;
        var actualResult = await context.Patients
            .Where(t =>
                    t.FirstName.Contains(searchTerm.ToLower()) ||
                    t.LastName.Contains(searchTerm.ToLower())
                    )
                    .FirstOrDefaultAsync();

        var expectedResult = await patientService.GetAllPatients(searchTerm);
        var patient = expectedResult.First();

        Assert.That(expectedResult, Is.Not.EqualTo(null));
        Assert.That(expectedResult.Count(), Is.EqualTo(1));
        Assert.That(patient.FullName, Is.EqualTo($"{actualResult.FirstName} {actualResult.LastName}"));
        Assert.That(patient.Id, Is.EqualTo(actualResult.Id));
    }
}
