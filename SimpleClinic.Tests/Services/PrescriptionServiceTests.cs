namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure.Entities;
using SimpleClinic.Infrastructure;
using static DatabaseSeeder;


[TestFixture]
internal class PrescriptionServiceTests
{
    private DbContextOptions<SimpleClinicDbContext> dbContextOptions;
    private SimpleClinicDbContext context;
    private PrescriptionService prescriptionService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        dbContextOptions = new DbContextOptionsBuilder<SimpleClinicDbContext>()
            .UseInMemoryDatabase(databaseName: "SimpleClinicInMemory" + Guid.NewGuid().ToString())
            .Options;

        context = new SimpleClinicDbContext(dbContextOptions);

        context.Database.EnsureCreated();

        SeedDatabase(this.context);

        prescriptionService = new PrescriptionService(context);
    }

    [Test]
    public async Task GetAllPrescriptionsForPatient_Should_ReturnPrescriptionsForPatient()
    {
        var patientId = patient.Id;
        var doctor = doctors[0];
        var medicament = new Medicament { Name = "Test Medicament", QuantityPerDayMilligrams = 50 };
        var prescription1 = new Prescription { Doctor = doctor, PatientId = patientId, Medicament = medicament, PrescriptionDate = DateTime.Now, Instructions = "Take once a day" };
        var prescription2 = new Prescription { Doctor = doctor, PatientId = patientId, Medicament = medicament, PrescriptionDate = DateTime.Now, Instructions = "Take twice a day" };
        context.Prescriptions.AddRange(prescription1, prescription2);
        await context.SaveChangesAsync();
        var doctorSpecialityName = await context.Doctors
            .Include(s => s.Speciality)
            .Where(x => x.Id == doctor.Id)
            .Select(s => s.Speciality.Name)
            .FirstOrDefaultAsync();

        var result = await prescriptionService.GetAllPrescriptionsForPatient(patientId);

        Assert.That(result, Is.Not.EqualTo(null));
        Assert.That(result.Count, Is.EqualTo(2));
        Assert.That($"{doctor.FirstName} {doctor.LastName}", Is.EqualTo(result[0].DoctorNames));
        Assert.That(doctorSpecialityName, Is.EqualTo(result[0].DoctorSpeciality));
        Assert.That(prescription1.PrescriptionDate.Date, Is.EqualTo(result[0].PrescriptionDate.Date));
        Assert.That(result[0].Medicament, Is.EqualTo(medicament.Name));
        Assert.That(medicament.QuantityPerDayMilligrams.ToString(), Is.EqualTo(result[0].MedicamentQantity));
        Assert.That(prescription1.Instructions, Is.EqualTo(result[1].Instructions));
    }

    [Test]
    public async Task SavePrescription_Should_AddNewPrescription()
    {
        var doctorId = "SomeTestDoctorId";
        var patientId = "SomeTestPatientId";
        var medicamentId = "SomeTestMedicamentId";
        var model = new PrescriptionViewModel
        {
            PatientId = patientId,
            MedicamentId = medicamentId,
            Instructions = "Take once a day, twice per night",
            PrescriptionDate = DateTime.Now
        };

        await prescriptionService.SavePrescription(model, doctorId);

        var addedPrescription = await context.Prescriptions.FirstOrDefaultAsync(p => p.PatientId == patientId);
        Assert.That(addedPrescription, Is.Not.EqualTo(null));
        Assert.That(doctorId, Is.EqualTo(addedPrescription!.DoctorId));
        Assert.That(patientId, Is.EqualTo(addedPrescription.PatientId));
        Assert.That(medicamentId, Is.EqualTo(addedPrescription.MedicamentId));
        Assert.That(model.Instructions, Is.EqualTo(addedPrescription.Instructions));
        Assert.That(model.PrescriptionDate, Is.EqualTo(addedPrescription.PrescriptionDate));
    }
}
