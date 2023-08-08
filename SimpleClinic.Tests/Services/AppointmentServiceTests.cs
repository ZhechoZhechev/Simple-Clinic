namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;
using static DatabaseSeeder;



public class AppointmentServiceTests
{
    private DbContextOptions<SimpleClinicDbContext> DbContextOptions;
    private SimpleClinicDbContext context;
    private IAppointmentService appointmentService;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        this.DbContextOptions = new DbContextOptionsBuilder<SimpleClinicDbContext>()
            .UseInMemoryDatabase(databaseName: "SimpleClinicInMemory" + Guid.NewGuid().ToString())
            .Options;

        this.context = new SimpleClinicDbContext(this.DbContextOptions);

        this.context.Database.EnsureCreated();

        SeedDatabase(this.context);

        this.appointmentService = new AppointmentService(this.context);
    }

    [Test]
    public async Task CancelDocAppointment_Should_CancelAppointmentAndFreeTimeSlot()
    {
        var docAppointment = await context.DoctorAppointments
            .FirstOrDefaultAsync();
        var timeSlot = docAppointment!.TimeSlot;
        var docAppointmentId = docAppointment.Id;

        await appointmentService.CancelDocAppointment(docAppointmentId);

        Assert.IsTrue(timeSlot.IsAvailable);
        Assert.IsFalse(docAppointment.IsActive);
    }

    [Test]
    public async Task CancelPatientAppointment_Should_CancelAppointment()
    {
        var docAppointment = await context.DoctorAppointments
            .FirstOrDefaultAsync();
        docAppointment!.IsActive = true;
        var timeSlot = docAppointment!.TimeSlot;
        timeSlot.IsAvailable = false;
        var appointmentId = docAppointment.Id;

        await appointmentService.CancelPatientAppointment(appointmentId);

        Assert.IsFalse(timeSlot.IsAvailable);
        Assert.IsFalse(docAppointment.IsActive);
    }

    [Test]
    public async Task CancelServiceAppointment_Should_CancelAppointmentAndFreeTimeSlot()
    {
        var serviceAppointment = await context.ServiceAppointments
            .FirstOrDefaultAsync();
        var timeSlot = serviceAppointment!.TimeSlot;
        timeSlot.IsAvailable = false;
        var appointmentId = serviceAppointment.Id;

        await appointmentService.CancelServiceAppointment(appointmentId);

        Assert.IsTrue(timeSlot.IsAvailable);
        Assert.IsFalse(serviceAppointment.IsActive);
    }

    [Test]
    public async Task CreateAppointment_Should_CreateAppointmentAndBookTimeSlot()
    {
        var timeSlot = new TimeSlot { Id = "timeSlotId", IsAvailable = true };
        var schedule = new Schedule { TimeSlots = new[] { timeSlot }, DoctorId = "doctorId", Day = DateTime.Today };
        context.TimeSlots.Add(timeSlot);
        context.Schedules.Add(schedule);
        await context.SaveChangesAsync();

        await appointmentService.CreateAppointment("timeSlotId", "patientId");

        var createdAppointment = await context.DoctorAppointments.FirstOrDefaultAsync(a => a.TimeSlotId == "timeSlotId");
        Assert.IsNotNull(createdAppointment);
        Assert.IsFalse(timeSlot.IsAvailable);
    }

    [Test]
    public async Task CreateServiceAppointment_Should_CreateAppointmentAndBookTimeSlot()
    {
        var timeSlot = new TimeSlot { Id = "serviceTimeSlotId", IsAvailable = true };
        var schedule = new Schedule { TimeSlots = new[] { timeSlot }, ServiceId = "serviceId", Day = DateTime.Today };
        context.TimeSlots.Add(timeSlot);
        context.Schedules.Add(schedule);
        await context.SaveChangesAsync();

        await appointmentService.CreateServiceAppointment("serviceTimeSlotId", "patientId");

        var createdAppointment = await context.ServiceAppointments.FirstOrDefaultAsync(a => a.TimeSlotId == "serviceTimeSlotId");
        Assert.IsNotNull(createdAppointment);
        Assert.IsFalse(timeSlot.IsAvailable);
    }

    [Test]
    public async Task GetDoctorAppointmentsForPatient_Should_ReturnCorrectColletion()
    {
        var patientId = await context.Patients.Where(x => x.FirstName == "Pesho")
            .Select(x => x.Id).FirstOrDefaultAsync();
        var expectedList = await context.DoctorAppointments
            .Where(x => x.IsActive == true && x.BookingDateTime >= DateTime.Today
            && x.PatientId == patientId)
            .Select(x => new DoctorBookingViewModel()
            {
                Id = x.Id,
                DocFirstName = x.Doctor.FirstName,
                DocLastName = x.Doctor.LastName,
                BookingDate = x.BookingDateTime,
                StartTime = x.TimeSlot.StartTime,
                EndTime = x.TimeSlot.EndTime
            })
            .ToListAsync();

        var actualList = await appointmentService.GetDoctorAppointmentsForPatient(patientId!);

        Assert.AreEqual(expectedList.Count, actualList.Count);

        foreach (var appointment in actualList)
        {
            var doctorBookingViewModel = expectedList.FirstOrDefault(x => x.Id == appointment.Id);

            Assert.IsNotNull(doctorBookingViewModel);
            Assert.AreEqual(appointment.DocFirstName, doctorBookingViewModel!.DocFirstName);
            Assert.AreEqual(appointment.DocLastName, doctorBookingViewModel.DocLastName);
            Assert.AreEqual(appointment.BookingDate, doctorBookingViewModel.BookingDate);
            Assert.AreEqual(appointment.StartTime, doctorBookingViewModel.StartTime);
            Assert.AreEqual(appointment.EndTime, doctorBookingViewModel.EndTime);
        }
    }

    [Test]
    public async Task GetPatientAppointmentsForDoctor_Should_ReturnCorrectColletion() 
    {
        var doctorId = await context.Doctors.Where(x => x.FirstName == "Joro")
            .Select(x => x.Id).FirstOrDefaultAsync();
        var expectedList = await context.DoctorAppointments
            .Where(x => x.DoctorId == doctorId
            && x.IsActive == true
            && x.BookingDateTime >= DateTime.Today)
            .Select(x => new PatientAppointmentViewModel()
            {
                Id = x.Id,
                PatientFirstName = x.Patient.FirstName,
                PatientLastName = x.Patient.LastName,
                PatientEmail = x.Patient.Email,
                BookingDate = x.BookingDateTime,
                StartTime = x.TimeSlot.StartTime,
                EndTime = x.TimeSlot.EndTime
            })
            .ToListAsync();

        var actualList = await appointmentService.GetPatientAppointmentsForDoctor(doctorId!);

        Assert.AreEqual(expectedList.Count, actualList.Count);

        foreach (var appointment in actualList)
        {
            var patientBookingViewModel = expectedList.FirstOrDefault(x => x.Id == appointment.Id);

            Assert.IsNotNull(patientBookingViewModel);
            Assert.AreEqual(appointment.PatientFirstName, patientBookingViewModel!.PatientFirstName);
            Assert.AreEqual(appointment.PatientLastName, patientBookingViewModel.PatientLastName);
            Assert.AreEqual(appointment.PatientEmail, patientBookingViewModel.PatientEmail);
            Assert.AreEqual(appointment.BookingDate, patientBookingViewModel.BookingDate);
            Assert.AreEqual(appointment.StartTime, patientBookingViewModel.StartTime);
            Assert.AreEqual(appointment.EndTime, patientBookingViewModel.EndTime);
        }
    }

    [Test]
    public async Task GetServiceAppointmentsForPatient_Should_ReturnCorrectColletion() 
    {
        var patientId = await context.Patients.Where(x => x.FirstName == "Pesho")
            .Select(x => x.Id).FirstOrDefaultAsync();
        var serviceAppointment = await context.ServiceAppointments.FirstOrDefaultAsync(x => x.PatientId == patientId);
        serviceAppointment!.IsActive = true;
        var expectedList = await context.ServiceAppointments
            .Where(x => x.PatientId == patientId
            && x.IsActive == true
            && x.BookingDateTime >= DateTime.Today)
            .Select(x => new ServiceBookingViewModel()
            {
                Id = x.Id,
                ServiceName = x.Service.Name,
                BookingDate = x.BookingDateTime,
                StartTime = x.TimeSlot.StartTime,
                EndTime = x.TimeSlot.EndTime
            })
            .ToListAsync();

        var actualList = await appointmentService.GetServiceAppointmentsForPatient(patientId);

        Assert.AreEqual(expectedList.Count, actualList.Count);

        foreach (var appointment in actualList)
        {
            var serviceBookingViewModel = expectedList.FirstOrDefault(x => x.Id == appointment.Id);

            Assert.IsNotNull(serviceBookingViewModel);
            Assert.AreEqual(appointment.ServiceName, serviceBookingViewModel!.ServiceName);
            Assert.AreEqual(appointment.BookingDate, serviceBookingViewModel.BookingDate);
            Assert.AreEqual(appointment.StartTime, serviceBookingViewModel.StartTime);
            Assert.AreEqual(appointment.EndTime, serviceBookingViewModel.EndTime);
        }
    }
}
