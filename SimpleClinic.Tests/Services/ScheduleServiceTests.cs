namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure;

[TestFixture]
internal class ScheduleServiceTests
{
    private DbContextOptions<SimpleClinicDbContext> dbContextOptions;
    private SimpleClinicDbContext context;
    private ScheduleService scheduleService;
    private string doctorId;
    private string doctorId1;
    private string serviceId;
    private string serviceId1;
    private DateTime day;
    private DateTime day1;
    private List<TimeSlotViewModel> listTimeSlots;
    private List<TimeSlotViewModel> listTimeSlots1;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        dbContextOptions = new DbContextOptionsBuilder<SimpleClinicDbContext>()
            .UseInMemoryDatabase(databaseName: "SimpleClinicInMemory" + Guid.NewGuid().ToString())
            .Options;

        context = new SimpleClinicDbContext(dbContextOptions);

        context.Database.EnsureCreated();

        scheduleService = new ScheduleService(context);
    }

    [SetUp]
    public void Setup()
    {
        serviceId = "TestServiceId";
        doctorId = "TestDoctorId";
        day = DateTime.Today;
        listTimeSlots = new List<TimeSlotViewModel>()
        {
            new TimeSlotViewModel()
            {
                StartTime = new DateTime(2023, 10, 13, 8, 0, 0),
                EndTime = new DateTime(2023, 10, 13, 9, 0, 0),
                IsAvailable = true
            },
            new TimeSlotViewModel()
            {
                StartTime = new DateTime(2023, 10, 13, 9, 0, 0),
                EndTime = new DateTime(2023, 10, 13, 10, 0, 0),
                IsAvailable = true
            }
        };

        serviceId1 = "TestServiceId1";
        doctorId1 = "TestDoctorId1";
        day1 = DateTime.Today;
        listTimeSlots1 = new List<TimeSlotViewModel>()
        {
            new TimeSlotViewModel()
            {
                StartTime = new DateTime(2023, 10, 13, 8, 0, 0),
                EndTime = new DateTime(2023, 10, 13, 9, 0, 0),
                IsAvailable = true
            },
            new TimeSlotViewModel()
            {
                StartTime = new DateTime(2023, 10, 13, 9, 0, 0),
                EndTime = new DateTime(2023, 10, 13, 10, 0, 0),
                IsAvailable = true
            }
        };

    }


    [Test]
    public async Task AddDoctorScheduleAsync_Add_ScheduleCorrectly()
    {
        await scheduleService.AddDoctorScheduleAsync(doctorId, day, listTimeSlots);

        var actualSchedule = await context.Schedules.FirstOrDefaultAsync();
        Assert.That(actualSchedule, Is.Not.EqualTo(null));
        Assert.That(actualSchedule!.DoctorId, Is.EqualTo(doctorId));
        Assert.That(actualSchedule.Day, Is.EqualTo(day));

        var timeSlot = actualSchedule.TimeSlots.ToList();
        Assert.That(timeSlot.Count, Is.EqualTo(2));
        Assert.That(timeSlot[0].StartTime, Is.EqualTo(new DateTime(2023, 10, 13, 8, 0, 0)));
        Assert.That(timeSlot[0].EndTime, Is.EqualTo(new DateTime(2023, 10, 13, 9, 0, 0)));
    }

    [Test]
    public async Task GetAvailableDates_Returns_Correct_Collection()
    {
        await scheduleService.AddDoctorScheduleAsync(doctorId1, day1, listTimeSlots1);

        var dates = await scheduleService.GetAvailableDates(doctorId1);

        Assert.That(dates, Is.Not.EqualTo(null));
        Assert.That(dates.Count, Is.EqualTo(1));
        Assert.That(dates[0].Date, Is.EqualTo(day1));
    }

    [Test]
    public async Task CheckSchedule_Returns_Correct_Collection()
    {

        var doctorId = "TestDoctorId";
        var day = DateTime.Today;
        var listTimeSlots = new List<TimeSlotViewModel>()
        {
            new TimeSlotViewModel()
            {
                StartTime = new DateTime(2023, 10, 13, 8, 0, 0),
                EndTime = new DateTime(2023, 10, 13, 9, 0, 0),
                IsAvailable = true
            },
            new TimeSlotViewModel()
            {
                StartTime = new DateTime(2023, 10, 13, 9, 0, 0),
                EndTime = new DateTime(2023, 10, 13, 10, 0, 0),
                IsAvailable = true
            }
        };

        await scheduleService.AddDoctorScheduleAsync(doctorId, day, listTimeSlots);

        var scheduleDoctor1 = await scheduleService.CheckSchedule(doctorId);

        Assert.That(scheduleDoctor1, Is.Not.EqualTo(null));
        Assert.That(scheduleDoctor1.Count, Is.EqualTo(2));
        Assert.That(scheduleDoctor1[0].Day, Is.EqualTo(day));
        Assert.That(scheduleDoctor1[0].TimeSlots.Count, Is.EqualTo(listTimeSlots.Count));

    }

    [Test]
    public async Task GetDoctorScheduleAsync_Returns_Correct_Data() 
    {
        var scheduleModel = await scheduleService.GetDoctorScheduleAsync(day, doctorId);

        Assert.That(scheduleModel, Is.Not.EqualTo(null));
        Assert.That(scheduleModel.Day, Is.EqualTo(day));
        Assert.That(scheduleModel.TimeSlots.Count(), Is.EqualTo(listTimeSlots.Count));
    }

    [Test]
    public async Task IfDayScheduleExists_Returns_True() 
    {
        var exists = await scheduleService.IfDayScheduleExists(day, doctorId);

        Assert.That(exists, Is.EqualTo(true));
    }

    [Test]
    public async Task IfDayScheduleExists_Returns_False()
    {
        var doctorId = "RandomDoctorId";
        var day = new DateTime(2045, 10, 11);

        var exists = await scheduleService.IfDayScheduleExists(day, doctorId);

        Assert.That(exists, Is.EqualTo(false));
    }

    [Test]
    public async Task AddServiceScheduleAsync_Add_ScheduleCorrectly()
    {
        await scheduleService.AddServiceScheduleAsync(serviceId, day, listTimeSlots);

        var actualSchedule = await context.Schedules.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
        Assert.That(actualSchedule, Is.Not.EqualTo(null));
        Assert.That(actualSchedule!.ServiceId, Is.EqualTo(serviceId));
        Assert.That(actualSchedule.Day, Is.EqualTo(day));

        var timeSlot = actualSchedule.TimeSlots.ToList();
        Assert.That(timeSlot.Count, Is.EqualTo(2));
        Assert.That(timeSlot[0].StartTime, Is.EqualTo(new DateTime(2023, 10, 13, 8, 0, 0)));
        Assert.That(timeSlot[0].EndTime, Is.EqualTo(new DateTime(2023, 10, 13, 9, 0, 0)));
    }

    [Test]
    public async Task IfDayServiceScheduleExists_Returns_True()
    {
        var exists = await scheduleService.IfDayServiceScheduleExists(day, serviceId);

        Assert.That(exists, Is.EqualTo(true));
    }

    [Test]
    public async Task IfDayServiceScheduleExists_Returns_False()
    {
        var serviceId = "RandomServiceId";
        var day = new DateTime(2045, 10, 11);

        var exists = await scheduleService.IfDayServiceScheduleExists(day, serviceId);

        Assert.That(exists, Is.EqualTo(false));
    }

    [Test]
    public async Task CheckServiceSchedule_Returns_Correct_Collection()
    {
        var serviceId1 = "TestServiceId1";
        var day = DateTime.Today;
        var listTimeSlots1 = new List<TimeSlotViewModel>()
        {
            new TimeSlotViewModel()
            {
                StartTime = new DateTime(2023, 10, 13, 8, 0, 0),
                EndTime = new DateTime(2023, 10, 13, 9, 0, 0),
                IsAvailable = true
            },
            new TimeSlotViewModel()
            {
                StartTime = new DateTime(2023, 10, 13, 9, 0, 0),
                EndTime = new DateTime(2023, 10, 13, 10, 0, 0),
                IsAvailable = true
            }
        };

        await scheduleService.AddServiceScheduleAsync(serviceId1, day, listTimeSlots1);

        var scheduleService1 = await scheduleService.CheckServiceSchedule(serviceId1);

        Assert.That(scheduleService1, Is.Not.EqualTo(null));
        Assert.That(scheduleService1.Count, Is.EqualTo(1));
        Assert.That(scheduleService1[0].Day, Is.EqualTo(day));
        Assert.That(scheduleService1[0].TimeSlots.Count, Is.EqualTo(listTimeSlots1.Count));
    }

    [Test]
    public async Task GetAvailableDatesService_Returns_Correct_Collection()
    {
        await scheduleService.AddServiceScheduleAsync(serviceId1, day1, listTimeSlots1);

        var dates = await scheduleService.GetAvailableDatesService(serviceId1);

        Assert.That(dates, Is.Not.EqualTo(null));
        Assert.That(dates.Count, Is.EqualTo(2));
        Assert.That(dates[0].Date, Is.EqualTo(day1));
    }

    [Test]
    public async Task GetServiceScheduleAsync_Returns_Correct_Data()
    {
        var scheduleModel = await scheduleService.GetServiceScheduleAsync(day, serviceId);

        Assert.That(scheduleModel, Is.Not.EqualTo(null));
        Assert.That(scheduleModel.Day, Is.EqualTo(day));
        Assert.That(scheduleModel.TimeSlots.Count(), Is.EqualTo(listTimeSlots.Count));
    }
}
