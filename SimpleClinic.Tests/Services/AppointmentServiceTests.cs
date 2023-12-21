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


[TestFixture]
internal class AppointmentServiceTests
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

        Assert.That(timeSlot.IsAvailable, Is.EqualTo(true));
        Assert.That(docAppointment.IsActive, Is.EqualTo(false));
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

        Assert.That(timeSlot.IsAvailable, Is.EqualTo(false));
        Assert.That(docAppointment.IsActive, Is.EqualTo(false));
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

        Assert.That(timeSlot.IsAvailable, Is.EqualTo(true));
        Assert.That(serviceAppointment.IsActive, Is.EqualTo(false));
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
        Assert.That(createdAppointment,Is.Not.EqualTo(null));
        Assert.That(timeSlot.IsAvailable, Is.EqualTo(false));
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
        Assert.That(createdAppointment, Is.Not.EqualTo(null));
        Assert.That(timeSlot.IsAvailable, Is.EqualTo(false));
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

        Assert.That(expectedList.Count, Is.EqualTo(actualList.Count));

        foreach (var appointment in actualList)
        {
            var doctorBookingViewModel = expectedList.FirstOrDefault(x => x.Id == appointment.Id);

            Assert.That(doctorBookingViewModel, Is.Not.EqualTo(null));
            Assert.That(appointment.DocFirstName, Is.EqualTo(doctorBookingViewModel!.DocFirstName));
            Assert.That(appointment.DocLastName, Is.EqualTo(doctorBookingViewModel.DocLastName));
            Assert.That(appointment.BookingDate, Is.EqualTo(doctorBookingViewModel.BookingDate));
            Assert.That(appointment.StartTime, Is.EqualTo(doctorBookingViewModel.StartTime));
            Assert.That(appointment.EndTime, Is.EqualTo(doctorBookingViewModel.EndTime));
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

        Assert.That(expectedList.Count, Is.EqualTo(actualList.Count));

        foreach (var appointment in actualList)
        {
            var patientBookingViewModel = expectedList.FirstOrDefault(x => x.Id == appointment.Id);

            Assert.That(patientBookingViewModel, Is.Not.EqualTo(null));
            Assert.That(appointment.PatientFirstName, Is.EqualTo(patientBookingViewModel!.PatientFirstName));
            Assert.That(appointment.PatientLastName, Is.EqualTo(patientBookingViewModel.PatientLastName));
            Assert.That(appointment.PatientEmail, Is.EqualTo(patientBookingViewModel.PatientEmail));
            Assert.That(appointment.BookingDate, Is.EqualTo(patientBookingViewModel.BookingDate));
            Assert.That(appointment.StartTime, Is.EqualTo(patientBookingViewModel.StartTime));
            Assert.That(appointment.EndTime, Is.EqualTo(patientBookingViewModel.EndTime));
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

        Assert.That(expectedList.Count, Is.EqualTo(actualList.Count));

        foreach (var appointment in actualList)
        {
            var serviceBookingViewModel = expectedList.FirstOrDefault(x => x.Id == appointment.Id);

            Assert.That(serviceBookingViewModel, Is.Not.EqualTo(null));
            Assert.That(appointment.ServiceName, Is.EqualTo(serviceBookingViewModel!.ServiceName));
            Assert.That(appointment.BookingDate, Is.EqualTo(serviceBookingViewModel.BookingDate));
            Assert.That(appointment.StartTime, Is.EqualTo(serviceBookingViewModel.StartTime));
            Assert.That(appointment.EndTime, Is.EqualTo(serviceBookingViewModel.EndTime));
        }
    }

    [Test]
    public async Task GetAppointmentById_Should_Return_Docotor_Appointment() 
    {
        var expectedModel = await context.DoctorAppointments
            .Where(a => a.Id == docAppointment.Id)
            .Select(a => new AppointmentViewModel() 
            {
                Doctor = a.Doctor,
                TimeSlot = a.TimeSlot,
                Patient = a.Patient,
                BookingDateTime = a.BookingDateTime
            })
            .FirstOrDefaultAsync();

        var actualModel = await appointmentService.GetAppointmentById(docAppointment.Id);

        Assert.That(actualModel, Is.Not.EqualTo(null));
        Assert.That(expectedModel.Doctor.Id, Is.EqualTo(actualModel.Doctor.Id));
        Assert.That(expectedModel.TimeSlot.Id, Is.EqualTo(actualModel.TimeSlot.Id));
        Assert.That(expectedModel.Patient.Id, Is.EqualTo(actualModel.Patient.Id));
        Assert.That(expectedModel.BookingDateTime, Is.EqualTo(actualModel.BookingDateTime));
    }

    [Test]
    public async Task GetAppointmentById_Should_Return_Service_Appointment()
    {
        var expectedModel = await context.ServiceAppointments
            .Where(a => a.Id == serviceAppointment.Id)
            .Select(a => new AppointmentViewModel()
            {
                Service = a.Service,
                TimeSlot = a.TimeSlot,
                Patient = a.Patient,
                BookingDateTime = a.BookingDateTime
            })
            .FirstOrDefaultAsync();

        var actualModel = await appointmentService.GetAppointmentById(serviceAppointment.Id);

        Assert.That(actualModel, Is.Not.EqualTo(null));
        Assert.That(expectedModel.Service.Id, Is.EqualTo(actualModel.Service.Id));
        Assert.That(expectedModel.TimeSlot.Id, Is.EqualTo(actualModel.TimeSlot.Id));
        Assert.That(expectedModel.Patient.Id, Is.EqualTo(actualModel.Patient.Id));
        Assert.That(expectedModel.BookingDateTime, Is.EqualTo(actualModel.BookingDateTime));
    }
}
