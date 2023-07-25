using System.ComponentModel.DataAnnotations;

namespace SimpleClinic.Infrastructure.Entities;

/// <summary>
/// TimeSlots for schedule
/// </summary>
public class TimeSlot
{
    /// <summary>
    /// Constructor
    /// </summary>
    public TimeSlot()
    {
        this.Id = Guid.NewGuid().ToString();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// StartTime
    /// </summary>
    [Required]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// EndTime
    /// </summary>
    [Required]
    public DateTime EndTime { get; set; }

    /// <summary>
    /// If slot is available
    /// </summary>
    [Required]
    public bool IsAvailable { get; set; } 
}