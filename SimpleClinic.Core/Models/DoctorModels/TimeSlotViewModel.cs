namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;
public class TimeSlotViewModel
{
    public string? Id { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }

    [Required]
    public bool IsAvailable { get; set; }
}