using Microsoft.EntityFrameworkCore;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;

namespace SimpleClinic.Core.Services;

public class ScheduleService : IScheduleService
{
    private readonly SimpleClinicDbContext context;

    public ScheduleService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task<bool> AddOrUpdateDoctorScheduleAsync(string doctorId, DateTime day, List<TimeSlotViewModel> timeSlots)
    {
        var schedule = new Schedule
        {
            DoctorId = doctorId,
            Day = day,
            TimeSlots = new List<TimeSlot>()
        };

        await context.Schedules.AddAsync(schedule);

        foreach (var timeSlot in timeSlots)
        {
            if (timeSlot.IsAvailable)
            {
                var newTimeSlot = new TimeSlot
                {
                    StartTime = timeSlot.StartTime,
                    EndTime = timeSlot.StartTime.AddHours(1),
                    IsAvailable = true
                };
                schedule.TimeSlots.Add(newTimeSlot);
            }
        }

        await context.SaveChangesAsync();

        return true;
    }

    public Task<bool> IfDayScheduleExists(DateTime day)
    {
        throw new NotImplementedException();
    }
}
