namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure.Entities;
using SimpleClinic.Infrastructure;


[TestFixture]
internal class MedicamentServiceTests
{
    private DbContextOptions<SimpleClinicDbContext> dbContextOptions;
    private SimpleClinicDbContext context;
    private MedicamentService medicamentService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        dbContextOptions = new DbContextOptionsBuilder<SimpleClinicDbContext>()
            .UseInMemoryDatabase(databaseName: "SimpleClinicInMemory" + Guid.NewGuid().ToString())
            .Options;

        context = new SimpleClinicDbContext(dbContextOptions);

        context.Database.EnsureCreated();

        medicamentService = new MedicamentService(context);
    }

    [Test]
    public async Task AddMedicamentAsync_Should_AddNewMedicament()
    {
        var viewModel = new MedicamentViewModel
        {
            Name = "Test Medicament",
            QuantityPerDayMilligrams = 50
        };

        await medicamentService.AddMedicamentAsync(viewModel);

        var addedMedicament = await context.Medicaments.FirstOrDefaultAsync(m => m.Name == viewModel.Name);
        Assert.NotNull(addedMedicament);
        Assert.AreEqual(viewModel.Name, addedMedicament.Name);
        Assert.AreEqual(viewModel.QuantityPerDayMilligrams, addedMedicament.QuantityPerDayMilligrams);
    }

    [Test]
    public async Task GetAllMedicaments_Should_ReturnMedicamentsMatchingSearchTerm()
    {
        var searchTerm = "est";
        var medicament1 = new Medicament { Name = "Test Medicament 1", QuantityPerDayMilligrams = 100 };
        var medicament2 = new Medicament { Name = "Test Medicament 2", QuantityPerDayMilligrams = 150 };
        var medicament3 = new Medicament { Name = "Test Medicament 3", QuantityPerDayMilligrams = 200 };
        context.Medicaments.AddRange(medicament1, medicament2, medicament3);
        await context.SaveChangesAsync();

        var result = await medicamentService.GetAllMedicaments(searchTerm);

        Assert.NotNull(result);
        Assert.AreEqual(4, result.Count);
        Assert.IsTrue(result.All(m => m.Name.Contains(searchTerm)));
    }
}

