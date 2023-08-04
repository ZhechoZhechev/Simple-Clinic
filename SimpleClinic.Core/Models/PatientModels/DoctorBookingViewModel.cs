namespace SimpleClinic.Core.Models.PatientModels;

public class DoctorBookingViewModel
{
    public string Id { get; set; }

    public string DocFirstName { get; set; }

    public string DocLastName { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
