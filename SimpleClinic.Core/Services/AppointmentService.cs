namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;
using System.Threading.Tasks;

public class AppointmentService : IAppointmentService
{
    private readonly SimpleClinicDbContext context;

    public AppointmentService(SimpleClinicDbContext context)
    {
        this.context = context;
    }
    public async Task CreateAppointment(string timeSlotId, string patientId)
    {
        var timeSlot = await context.TimeSlots.FindAsync(timeSlotId);

        var doctorId = await context.Schedules
            .Where(s => s.TimeSlots.Any(ts => ts.Id == timeSlotId))
            .Select(s => s.DoctorId)
            .FirstOrDefaultAsync();

        var bookingDate = await context.Schedules
            .Where(s => s.TimeSlots.Any(ts => ts.Id == timeSlotId))
            .Select(s => s.Day)
            .FirstOrDefaultAsync();

        var newBooking = new DoctorAppointment()
        {
            DoctorId = doctorId!,
            TimeSlotId = timeSlotId,
            PatientId = patientId,
            BookingDateTime = bookingDate
        };

        timeSlot!.IsAvailable = false;

        await context.DoctorAppointments.AddAsync(newBooking);
        await context.SaveChangesAsync();
    }

}
