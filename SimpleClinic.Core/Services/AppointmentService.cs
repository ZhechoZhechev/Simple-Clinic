namespace SimpleClinic.Core.Services;

using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;

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
            var timeSlot = await context.TimeSlots
            .FindAsync(appToCancel.TimeSlotId);

            timeSlot!.IsAvailable = true;
            appToCancel.IsActive = false;

            await context.SaveChangesAsync();
        }
    }

    public async Task CancelPatientAppointment(string id)
    {
        var appToCancel = await context.DoctorAppointments
            .FindAsync(id);

        if (appToCancel != null)
        {
            appToCancel.IsActive = false;

            await context.SaveChangesAsync();
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
            IsActive = true
        };

        timeSlot!.IsAvailable = false;

        await context.DoctorAppointments.AddAsync(newBooking);
        await context.SaveChangesAsync();
    }

    public async Task<List<DoctorBookingViewModel>> GetDoctorAppointmentsForPatient(string patientId)
    {
        var doctorBookings = await context.DoctorAppointments
            .Where(x => x.PatientId == patientId
            && x.IsActive == true
            && x.BookingDateTime >= DateTime.Today)
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

    public async Task<List<PatientAppointmentViewModel>> GetPatientAppointmentsForDoctor(string doctorId)
    {
        var doctorBookings = await context.DoctorAppointments
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

        return doctorBookings;
    }
}
