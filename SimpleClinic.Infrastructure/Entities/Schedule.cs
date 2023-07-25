namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;

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
    [Required]
    public DateTime Day { get; set; }

    /// <summary>
    /// Timeslots
    /// </summary>
    public ICollection<TimeSlot> TimeSlots { get; set; }
}