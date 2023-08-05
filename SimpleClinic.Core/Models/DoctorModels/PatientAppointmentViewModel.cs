namespace SimpleClinic.Core.Models.DoctorModels;

public class PatientAppointmentViewModel
{

    public string Id { get; set; }

    public string PatientFirstName { get; set; }

    public string PatientLastName { get; set; }

    public string PatientEmail { get; set; }

    public DateTime BookingDate { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
