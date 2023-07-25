using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SimpleClinic.Infrastructure.Entities;

/// <summary>
/// Doctor appointment
/// </summary>
public class DoctorAppointment
{
    /// <summary>
    /// Constructor
    /// </summary>
    public DoctorAppointment()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// Identifier of the doctor
    /// </summary>
    [Required]
    [ForeignKey(nameof(Doctor))]
    public string DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    /// <summary>
    /// Timeslot for the appointment
    /// </summary>
    [Required]
    [ForeignKey(nameof(TimeSlot))]
    public string TimeSlotId { get; set; }
    public TimeSlot TimeSlot { get; set; }

    /// <summary>
    /// When it is made
    /// </summary>
    [Required]
    public DateTime BookingDateTime { get; set; }
}