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

    public async Task AddOrUpdateDoctorSchedule(string doctorId, DateTime startDate, DateTime endDate, List<DayScheduleViewModel> days)
    {
        var doctor = await context.Doctors.Include(d => d.Schedules)
                                     .FirstOrDefaultAsync(d => d.Id == doctorId);

        if (doctor == null)
            throw new ArgumentException("Doctor not found");

        for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
        {
            var daySchedule = days.FirstOrDefault(d => d.Day.Date == date);
            if (daySchedule == null)
                continue;

            var existingSchedule = doctor.Schedules.FirstOrDefault(s => s.Day.Date == date);

            if (existingSchedule == null)
            {
                // Create a new schedule if it doesn't exist
                existingSchedule = new Schedule 
                {
                    Id = doctorId,
                    Day = date,
                    TimeSlots = (ICollection<TimeSlot>)daySchedule.TimeSlots 
                };
                doctor.Schedules.Add(existingSchedule);
            }
            else
            {
                // Update the existing schedule with new time slots
                existingSchedule.TimeSlots.Clear();
                foreach (var timeSlot in daySchedule.TimeSlots)
                {
                    existingSchedule.TimeSlots.Add(new TimeSlot
                    {
                        StartTime = timeSlot.StartTime,
                        EndTime = timeSlot.StartTime.AddHours(1),
                        IsAvailable = true // You can set this value based on user input if needed
                    });
                }
            }
        }

        await context.SaveChangesAsync();
    }
}
