namespace SimpleClinic.Core.Models.PatientModels;

using SimpleClinic.Infrastructure.Entities;

public class AppointmentViewModel
{
    public Service? Service { get; set; }

    public Doctor? Doctor { get; set; }

    public TimeSlot TimeSlot { get; set; }

    public Patient Patient { get; set; }

    public DateTime BookingDateTime { get; set; }
}
