namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure;

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

        this.accountService = new AccountService(this.context);
    }

    [Test]
    public async Task AddMedicalHistory_Should_AddMedicalHistoryAndSetFormsCompleted()
    {
        var userId = "test userId";

        var model = new MedicalHistoryViewModel()
        {
            Surgery = "test surgery",
            MedicalConditions = "test med conditions",
            PatientId = userId
        };

        await accountService.AddMedicalHistory(model, userId);

        var medicalHistory = await context.MedicalHistories.FirstOrDefaultAsync();
        var patient = await context.Patients.FindAsync(userId);

        Assert.IsNotNull(medicalHistory);
        Assert.True(patient.FormsCompleted == true);
    }
}
