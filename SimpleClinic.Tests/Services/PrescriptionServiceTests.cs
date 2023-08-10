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

        prescriptionService = new PrescriptionService(context);
    }

    [Test]
    public async Task GetAllPrescriptionsForPatient_Should_ReturnPrescriptionsForPatient()
    {
        var patientId = patient.Id;
        var doctor = doctors[0];
        var medicament = new Medicament { Name = "Test Medicament", QuantityPerDayMilligrams = 50 };
        var prescription1 = new Prescription { Doctor = doctor, PatientId = patientId, Medicament = medicament, PrescriptionDate = DateTime.Now.AddDays(1), Instructions = "Take once a day" };
        var prescription2 = new Prescription { Doctor = doctor, PatientId = patientId, Medicament = medicament, PrescriptionDate = DateTime.Now, Instructions = "Take twice a day" };
        context.Prescriptions.AddRange(prescription1, prescription2);
        await context.SaveChangesAsync();
        var doctorSpecialityName = await context.Doctors
            .Include(s => s.Speciality)
            .Where(x => x.Id == doctor.Id)
            .Select(s => s.Speciality.Name)
            .FirstOrDefaultAsync();

        var result = await prescriptionService.GetAllPrescriptionsForPatient(patientId);

        Assert.NotNull(result);
        Assert.AreEqual(2, result.Count);
        Assert.AreEqual($"{doctor.FirstName} {doctor.LastName}", result[0].DoctorNames);
        Assert.AreEqual(doctorSpecialityName, result[0].DoctorSpeciality);
        Assert.AreEqual(prescription1.PrescriptionDate, result[0].PrescriptionDate);
        Assert.AreEqual(medicament.Name, result[0].Medicament);
        Assert.AreEqual(medicament.QuantityPerDayMilligrams.ToString(), result[0].MedicamentQantity);
        Assert.AreEqual(prescription1.Instructions, result[0].Instructions);
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
        Assert.NotNull(addedPrescription);
        Assert.AreEqual(doctorId, addedPrescription!.DoctorId);
        Assert.AreEqual(patientId, addedPrescription.PatientId);
        Assert.AreEqual(medicamentId, addedPrescription.MedicamentId);
        Assert.AreEqual(model.Instructions, addedPrescription.Instructions);
        Assert.AreEqual(model.PrescriptionDate, addedPrescription.PrescriptionDate);
    }
}
