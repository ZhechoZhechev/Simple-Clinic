namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;


public class ScheduleService : IScheduleService
{
    private readonly SimpleClinicDbContext context;

    public ScheduleService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task<bool> AddDoctorScheduleAsync(string doctorId, DateTime day, List<TimeSlotViewModel> timeSlots)
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

    public async Task<List<DateTime>> GetAvailableDates(string doctorId)
    {
        var dates = await context.Schedules
            .Where(x => x.DoctorId == doctorId)
            .Select(d => d.Day)
            .ToListAsync();

        return dates;
    }

    public async Task<List<DayScheduleViewModel>> CheckSchedule(string doctorId)
    {
        var schedule = await context.Schedules
            .Where(x => x.DoctorId == doctorId)
            .Select(x => new DayScheduleViewModel() 
            {
                Id = x.Id,
                Day = x.Day,
                TimeSlots = x.TimeSlots
                .Select(ts => new TimeSlotViewModel() 
                {
                    StartTime = ts.StartTime,
                    EndTime = ts.StartTime.AddHours(1),
                    IsAvailable = ts.IsAvailable
                })
                .ToList()
            })
            .ToListAsync();

        return schedule;
    }

    public async Task<DayScheduleViewModel> GetDoctorScheduleAsync(DateTime day, string doctorId)
    {
        var schedule = await context.Schedules
            .Include(t => t.TimeSlots)
            .Where(x => x.DoctorId == doctorId && x.Day == day)
            .FirstOrDefaultAsync();

        if (schedule != null) 
        {
            return new DayScheduleViewModel()
            {
                Id = schedule.Id,
                Day = day,
                TimeSlots = schedule.TimeSlots
                .Select(ts => new TimeSlotViewModel()
                {
                    StartTime = ts.StartTime,
                    EndTime = ts.StartTime.AddHours(1),
                    IsAvailable = ts.IsAvailable
                })
                .ToList()
            };
            
        }

        return null;
    }

    public async Task<bool> IfDayScheduleExists(DateTime day, string doctorId)
    {
        var schedule = await context.Schedules
            .Where(x => x.DoctorId == doctorId && x.Day == day)
            .FirstOrDefaultAsync();

        if (schedule == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
