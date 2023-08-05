namespace SimpleClinic.Core.Models.PatientModels;

using System.ComponentModel.DataAnnotations;


public class DoctorAppointmentViewModel
{
    /// <summary>
    /// What day is the booking for
    /// </summary>
    public DateTime Day { get; set; }

    /// <summary>
    /// Identifier of the doctor
    /// </summary>
    [Required]
    public string DoctorId { get; set; }

    /// <summary>
    /// Timeslot for the appointment
    /// </summary>
    [Required]
    public string TimeSlotId { get; set; }

    /// <summary>
    /// When it is made
    /// </summary>
    [Required]
    public DateTime BookingDateTime { get; set; }
}
