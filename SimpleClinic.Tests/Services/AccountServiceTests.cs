namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure;
using static DatabaseSeeder;
public class AccountServiceTests
{
    private DbContextOptions<SimpleClinicDbContext> DbContextOptions;
    private SimpleClinicDbContext context;
    private IAccountService accountService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        this.DbContextOptions = new DbContextOptionsBuilder<SimpleClinicDbContext>()
            .UseInMemoryDatabase(databaseName: "SimpleClinicInMemory" + Guid.NewGuid().ToString())
            .Options;

        this.context = new SimpleClinicDbContext(this.DbContextOptions);

        this.context.Database.EnsureCreated();

        SeedDatabase(this.context);

        this.accountService = new AccountService(this.context);
    }

    [Test]
    public async Task AddMedicalHistory_Should_AddMedicalHistoryAndSetFormsCompleted()
    {
        var userId = await context.Patients.Where(x => x.FirstName == "Pesho")
            .Select(x => x.Id).FirstOrDefaultAsync();

        var model = new MedicalHistoryViewModel()
        {
            Surgery = "test surgery",
            MedicalConditions = "test med conditions",
            PatientId = userId
        };

        await accountService.AddMedicalHistory(model, userId!);

        var medicalHistory = await context.MedicalHistories.FirstOrDefaultAsync();
        var patient = await context.Patients.FindAsync(userId);

        Assert.IsNotNull(medicalHistory);
        Assert.True(patient.FormsCompleted);
    }

    [Test]
    public async Task AddNextOfKin_Should_AddNextOfKin()
    {
        var userId = await context.Patients.Where(x => x.FirstName == "Pesho")
            .Select(x => x.Id).FirstOrDefaultAsync();

        var user = context.Patients.FindAsync(userId);

        var model = new NextOfKinViewModel()
        {
            Name = "test name",
            PhoneNumber = "1234567890",
            Address = "some address",
            PatientId = userId
        };

        await accountService.AddNextOfKin(model, userId!);

        var nextOfKin = await context.NextOfKins.FirstOrDefaultAsync();

        Assert.IsNotNull(nextOfKin);
        Assert.That(user.Result!.NextOfKinId == nextOfKin!.Id);
    }

    [Test]
    public async Task GetIsFormFilled_Should_ReturnCorrectValue()
    {
        var userId = await context.Patients.Where(x => x.FirstName == "Pesho")
        .Select(x => x.Id).FirstOrDefaultAsync();

        var isFormFilled = await accountService.GetIsFormFilled(userId);

        Assert.True(isFormFilled);
    }

    [Test]
    public async Task GetRoleId_Should_ReturnRoleId()
    {

        var userId = await context.Patients.Where(x => x.FirstName == "Pesho")
        .Select(x => x.Id).FirstOrDefaultAsync();

        var roleId = await accountService.GetRoleId(userId);

        Assert.AreEqual(roleId, "TestRoleId");
    }
}
