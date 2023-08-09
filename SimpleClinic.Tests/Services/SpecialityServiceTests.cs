namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;
using static DatabaseSeeder;


public class SpecialityServiceTests
{

    private DbContextOptions<SimpleClinicDbContext> dbContextOptions;
    private SimpleClinicDbContext context;
    private SpecialityService specialityService;


    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        dbContextOptions = new DbContextOptionsBuilder<SimpleClinicDbContext>()
            .UseInMemoryDatabase(databaseName: "SimpleClinicInMemory" + Guid.NewGuid().ToString())
            .Options;

        context = new SimpleClinicDbContext(dbContextOptions);

        context.Database.EnsureCreated();

        specialityService = new SpecialityService(context);
    }

    [Test]

    public async Task AddCustomSpeciality_Adds_Speciality_With_Given_Name() 
    {
        var specialityName = "TestName";

        await specialityService.AddCustomSpeciality(specialityName);

        var addedSpeciality = await context.Specialities
            .FirstOrDefaultAsync(s => s.Name == specialityName);

        Assert.NotNull(addedSpeciality);
        Assert.AreEqual(specialityName, addedSpeciality!.Name);
    }

    [Test]
    public async Task GetAllSpecialities_Should_Return_AllSpecialities()
    {
        var specialities = await context.Specialities.ToListAsync();

        var result = await specialityService.GetAllSpecialities();

        Assert.NotNull(result);
        Assert.AreEqual(specialities.Count, result.Count());

        foreach (var speciality in specialities)
        {
            var matchingResult = result.FirstOrDefault(r => r.Id == speciality.Id && r.Name == speciality.Name);
            Assert.NotNull(matchingResult);
        }
    }

    [Test]
    public async Task GetAllSpecialitiesWithDoctorsCount_Should_Return_Specialities_With_Doctors_Count()
    {
        context.Specialities.RemoveRange(context.Specialities);
        await context.SaveChangesAsync();
        var specialities = new List<Speciality>
        {
            new Speciality { Id = 1, Name = "Speciality 1", Doctors = new List<Doctor> { doctors[0] } },
            new Speciality { Id = 2, Name = "Speciality 2", Doctors = new List<Doctor> { doctors[1], doctors[2] } },
        };
        context.Specialities.AddRange(specialities);
        await context.SaveChangesAsync();

        var result = await specialityService.GetAllSpecialitiesWithDoctorsCount();

        Assert.NotNull(result);
        Assert.AreEqual(specialities.Count, result.Count());

        foreach (var speciality in specialities)
        {
            var matchingResult = result.FirstOrDefault(r => r.Name == speciality.Name && r.DoctorsCount == speciality.Doctors.Count);
            Assert.NotNull(matchingResult);
        }
    }

    [Test]
    public async Task GetAllSpecialityNames_Returns_Correct_Data() 
    {
        context.Specialities.RemoveRange(context.Specialities);
        await context.SaveChangesAsync();
        var specialities = new List<Speciality>
        {
            new Speciality { Id = 1, Name = "Speciality1", Doctors = new List<Doctor>() },
            new Speciality { Id = 2, Name = "Speciality2", Doctors = new List<Doctor>() },
            new Speciality { Id = 3, Name = "Speciality3", Doctors = new List<Doctor>() },
        };
        foreach (var speciality  in specialities)
        {
            context.Specialities.Add(speciality);
            await context.SaveChangesAsync();
        }
        

        var result = await specialityService.GetAllSpecialityNames();

        Assert.NotNull(result);
        for (int i = 0; i < specialities.Count; i++)
        {
            Assert.AreEqual(specialities[i].Name, result[i]);
        }
    }
}
