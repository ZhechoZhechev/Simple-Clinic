namespace SimpleClinic.Core.Models.PatientModels;

public class ServiceBookingViewModel
{
    public string Id { get; set; }

    public string ServiceName { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}

