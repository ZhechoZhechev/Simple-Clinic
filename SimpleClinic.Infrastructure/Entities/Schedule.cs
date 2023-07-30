namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/// <summary>
/// Schedule for service and doctor appoitment
/// </summary>
public class Schedule
{
    /// <summary>
    /// Constructor
    /// </summary>
    public Schedule()
    {
        this.Id = Guid.NewGuid().ToString();
        this.TimeSlots = new HashSet<TimeSlot>();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// Day
    /// </summary>
    public DateTime? Day { get; set; }

    /// <summary>
    /// Identifier of the doctor
    /// </summary>
    [ForeignKey(nameof(Doctor))]
    public string? DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    /// <summary>
    /// Identifier of the service
    /// </summary>
    [ForeignKey(nameof(Service))]
    public string? ServiceId { get; set; }
    public Service Service { get; set; }

    /// <summary>
    /// Timeslots
    /// </summary>
    public ICollection<TimeSlot> TimeSlots { get; set; }
}