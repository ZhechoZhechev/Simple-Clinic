namespace SimpleClinic.Tests.Services;

using Microsoft.EntityFrameworkCore;

using NUnit.Framework;

using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Services;
using SimpleClinic.Infrastructure;

[TestFixture]
public class ScheduleServiceTests
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
        day = new DateTime(2023, 10, 13);
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
        day1 = new DateTime(2023, 11, 13);
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
        Assert.IsNotNull(actualSchedule);
        Assert.AreEqual(actualSchedule!.DoctorId, doctorId);
        Assert.AreEqual(actualSchedule.Day, day);

        var timeSlot = actualSchedule.TimeSlots.ToList();
        Assert.AreEqual(timeSlot.Count, 2);
        Assert.AreEqual(timeSlot[0].StartTime, new DateTime(2023, 10, 13, 8, 0, 0));
        Assert.AreEqual(timeSlot[0].EndTime, new DateTime(2023, 10, 13, 9, 0, 0));
    }

    [Test]
    public async Task GetAvailableDates_Returns_Correct_Collection()
    {
        await scheduleService.AddDoctorScheduleAsync(doctorId1, day1, listTimeSlots1);

        var dates = await scheduleService.GetAvailableDates(doctorId1);

        Assert.IsNotNull(dates);
        Assert.AreEqual(dates.Count, 2);
        Assert.AreEqual(dates[0].Date, day1);
    }

    [Test]
    public async Task CheckSchedule_Returns_Correct_Collection()
    {
        await scheduleService.AddDoctorScheduleAsync(doctorId1, day1, listTimeSlots1);

        var scheduleDoctor1 = await scheduleService.CheckSchedule(doctorId1);

        Assert.IsNotNull(scheduleDoctor1);
        Assert.AreEqual(scheduleDoctor1.Count, 1);
        Assert.AreEqual(scheduleDoctor1[0].Day, day1);
        Assert.AreEqual(scheduleDoctor1[0].TimeSlots.Count, listTimeSlots1.Count);

    }

    [Test]
    public async Task GetDoctorScheduleAsync_Returns_Correct_Data() 
    {
        var scheduleModel = await scheduleService.GetDoctorScheduleAsync(day, doctorId);

        Assert.IsNotNull(scheduleModel);
        Assert.AreEqual(scheduleModel.Day, day);
        Assert.AreEqual(scheduleModel.TimeSlots.Count(), listTimeSlots.Count);
    }

    [Test]
    public async Task IfDayScheduleExists_Returns_True() 
    {
        var exists = await scheduleService.IfDayScheduleExists(day, doctorId);

        Assert.IsTrue(exists);
    }

    [Test]
    public async Task IfDayScheduleExists_Returns_False()
    {
        var doctorId = "RandomDoctorId";
        var day = new DateTime(2045, 10, 11);

        var exists = await scheduleService.IfDayScheduleExists(day, doctorId);

        Assert.IsFalse(exists);
    }

    [Test]
    public async Task AddServiceScheduleAsync_Add_ScheduleCorrectly()
    {
        await scheduleService.AddServiceScheduleAsync(serviceId, day, listTimeSlots);

        var actualSchedule = await context.Schedules.FirstOrDefaultAsync(x => x.ServiceId == serviceId);
        Assert.IsNotNull(actualSchedule);
        Assert.AreEqual(actualSchedule!.ServiceId, serviceId);
        Assert.AreEqual(actualSchedule.Day, day);

        var timeSlot = actualSchedule.TimeSlots.ToList();
        Assert.AreEqual(timeSlot.Count, 2);
        Assert.AreEqual(timeSlot[0].StartTime, new DateTime(2023, 10, 13, 8, 0, 0));
        Assert.AreEqual(timeSlot[0].EndTime, new DateTime(2023, 10, 13, 9, 0, 0));
    }

    [Test]
    public async Task IfDayServiceScheduleExists_Returns_True()
    {
        var exists = await scheduleService.IfDayServiceScheduleExists(day, serviceId);

        Assert.IsTrue(exists);
    }

    [Test]
    public async Task IfDayServiceScheduleExists_Returns_False()
    {
        var serviceId = "RandomServiceId";
        var day = new DateTime(2045, 10, 11);

        var exists = await scheduleService.IfDayServiceScheduleExists(day, serviceId);

        Assert.IsFalse(exists);
    }

    [Test]
    public async Task CheckServiceSchedule_Returns_Correct_Collection()
    {
        await scheduleService.AddServiceScheduleAsync(serviceId1, day1, listTimeSlots1);

        var scheduleService1 = await scheduleService.CheckServiceSchedule(serviceId1);

        Assert.IsNotNull(scheduleService1);
        Assert.AreEqual(scheduleService1.Count, 1);
        Assert.AreEqual(scheduleService1[0].Day, day1);
        Assert.AreEqual(scheduleService1[0].TimeSlots.Count, listTimeSlots1.Count);
    }

    [Test]
    public async Task GetAvailableDatesService_Returns_Correct_Collection()
    {
        await scheduleService.AddServiceScheduleAsync(serviceId1, day1, listTimeSlots1);

        var dates = await scheduleService.GetAvailableDatesService(serviceId1);

        Assert.IsNotNull(dates);
        Assert.AreEqual(dates.Count, 2);
        Assert.AreEqual(dates[0].Date, day1);
    }

    [Test]
    public async Task GetServiceScheduleAsync_Returns_Correct_Data()
    {
        var scheduleModel = await scheduleService.GetServiceScheduleAsync(day, serviceId);

        Assert.IsNotNull(scheduleModel);
        Assert.AreEqual(scheduleModel.Day, day);
        Assert.AreEqual(scheduleModel.TimeSlots.Count(), listTimeSlots.Count);
    }
}
