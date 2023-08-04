namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
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

    public async Task CancelDocAppointment(string id)
    {
        var appToCancel = await context.DoctorAppointments
            .FindAsync(id);

        if (appToCancel != null) 
        {
            return;
        }
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
            BookingDateTime = bookingDate,
        };

        timeSlot!.IsAvailable = false;

        await context.DoctorAppointments.AddAsync(newBooking);
        await context.SaveChangesAsync();
    }

    public async Task<List<DoctorBookingViewModel>> GetDoctorAppointmentsForPatient(string patientId)
    {
        var doctorBookings = await context.DoctorAppointments
            .Where(x => x.PatientId == patientId)
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

        return doctorBookings;
    }
}
