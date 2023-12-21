namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;
using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure;
using static DatabaseSeeder;

[TestFixture]
internal class DoctorServiceTests
{

    private DbContextOptions<SimpleClinicDbContext> DbContextOptions;
    private SimpleClinicDbContext context;
    private IDoctorService doctorService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        this.DbContextOptions = new DbContextOptionsBuilder<SimpleClinicDbContext>()
            .UseInMemoryDatabase(databaseName: "SimpleClinicInMemory" + Guid.NewGuid().ToString())
            .Options;

        this.context = new SimpleClinicDbContext(this.DbContextOptions);

        this.context.Database.EnsureCreated();

        SeedDatabase(this.context);

        this.doctorService = new DoctorService(this.context);
    }

    [Test]
    public async Task DoctorExistsById_Exists_ReturnsTrue()
    {
        var doctorId = doctors.First().Id;

        var result = await doctorService.DoctorExistsById(doctorId);

        Assert.That(result, Is.EqualTo(true));
    }

    [Test]
    public async Task DoctorExistsById_NotExists_ReturnsFalse()
    {
        var doctorId = "nonExistentDoctorId";

        var result = await doctorService.DoctorExistsById(doctorId);

        Assert.That(result, Is.EqualTo(false));
    }

    [Test]
    public async Task DoctorDetails_ReturnsCorrectDetails()
    {
        var doctor = doctors.First();

        var doctorSepcName = await context.Doctors
            .Include(s => s.Speciality)
            .Where(x => x.Id == doctor.Id)
            .Select(x => x.Speciality.Name).FirstOrDefaultAsync();

        var actualModel = new DoctorDetailsViewModel()
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            ProfilePictureFilename = doctor.ProfilePictureFilename,
            Speciality = doctorSepcName,
            PricePerHour = doctor.PricePerAppointment.ToString(),
            ShortBio = doctor.Biography
        };

        var expectedModel = await doctorService.DoctorDetails(doctor.Id);

        Assert.That(actualModel.Id, Is.EqualTo(expectedModel.Id));
        Assert.That(actualModel.FirstName, Is.EqualTo(expectedModel.FirstName));
        Assert.That(actualModel.LastName, Is.EqualTo(expectedModel.LastName));
        Assert.That(actualModel.ProfilePictureFilename, Is.EqualTo(expectedModel.ProfilePictureFilename));
        Assert.That(actualModel.Speciality, Is.EqualTo(expectedModel.Speciality));
        Assert.That(actualModel.PricePerHour, Is.EqualTo(expectedModel.PricePerHour));
        Assert.That(actualModel.ShortBio, Is.EqualTo(expectedModel.ShortBio));
    }

    [Test]
    public async Task GetFirstThreeDoctors_ReturnsThreeDoctors()
    {
        var result = await doctorService.GetFirstThreeDoctors();

        Assert.That(result, Is.Not.EqualTo(null));
        Assert.That(3, Is.EqualTo(result.Count()));
    }

    [Test]
    public async Task All_ReturnsCorrectDoctors_PerSpeciality()
    {
        var speciality = "Family medicine";
        var searchTerm = "";
        var currentPage = 1;
        var doctorsPerPage = 1;
        var actualResult = await context.Doctors
            .Include(x => x.Speciality)
            .FirstOrDefaultAsync(x => x.Speciality.Name == speciality);

        var result = await doctorService.All(speciality, searchTerm, currentPage, doctorsPerPage);
        var doctorInModel = result.Doctors.FirstOrDefault();

        Assert.That(result, Is.Not.EqualTo(null));
        Assert.That(result.TotalDoctorsCount, Is.EqualTo(1));
        Assert.That(doctorInModel!.Fullname, Is.EqualTo($"{actualResult.FirstName} {actualResult.LastName}"));
        Assert.That(doctorInModel.Id, Is.EqualTo(actualResult.Id));
        Assert.That(doctorInModel.Speciality, Is.EqualTo(actualResult.Speciality.Name));
        Assert.That(doctorInModel.OfficePhoneNumber, Is.EqualTo(actualResult.OfficePhoneNumber));
    }

    [Test]
    public async Task All_ReturnsCorrectDoctors_PerSearchTerm()
    {
        var speciality = "";
        var searchTerm = "oro";
        var currentPage = 1;
        var doctorsPerPage = 1;
        var actualResult = await context.Doctors
            .Include(x => x.Speciality)
            .Where(t =>
                    t.FirstName.Contains(searchTerm.ToLower()) ||
                    t.LastName.Contains(searchTerm.ToLower()) ||
                    t.Speciality.Name.Contains(searchTerm.ToLower())
                    )
                    .FirstOrDefaultAsync();

        var result = await doctorService.All(speciality, searchTerm, currentPage, doctorsPerPage);
        var doctorInModel = result.Doctors.FirstOrDefault();

        Assert.That(result, Is.Not.EqualTo(null));
        Assert.That(result.TotalDoctorsCount, Is.EqualTo(1));
        Assert.That(doctorInModel!.Fullname, Is.EqualTo($"{actualResult.FirstName} {actualResult.LastName}"));
        Assert.That(doctorInModel.Id, Is.EqualTo(actualResult.Id));
        Assert.That(doctorInModel.Speciality, Is.EqualTo(actualResult.Speciality.Name));
        Assert.That(doctorInModel.OfficePhoneNumber, Is.EqualTo(actualResult.OfficePhoneNumber));
    }

    [Test]
    public async Task DoctorDetailsForPatient_ReturnsCorrectDetails()
    {
        var doctor = doctors.First();

        var doctorSepcName = await context.Doctors
            .Include(s => s.Speciality)
            .Where(x => x.Id == doctor.Id)
            .Select(x => x.Speciality.Name).FirstOrDefaultAsync();

        var actualModel = new DoctorDetailsViewModel()
        {
            Id = doctor.Id,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            ProfilePictureFilename = doctor.ProfilePictureFilename,
            Speciality = doctorSepcName,
            PricePerHour = doctor.PricePerAppointment.ToString(),
            ShortBio = doctor.Biography
        };

        var expectedModel = await doctorService.DetailsForPatient(doctor.Id);

        Assert.That(actualModel.Id, Is.EqualTo(expectedModel.Id));
        Assert.That($"{actualModel.FirstName} {actualModel.LastName}", Is.EqualTo(expectedModel.Fullname));
        Assert.That(actualModel.ProfilePictureFilename, Is.EqualTo(expectedModel.ProfilePictureFilename));
        Assert.That(actualModel.Speciality, Is.EqualTo(expectedModel.Speciality));
        Assert.That(actualModel.PricePerHour, Is.EqualTo(expectedModel.PricePerHour));
        Assert.That(actualModel.ShortBio, Is.EqualTo(expectedModel.Biography));
    }
}
