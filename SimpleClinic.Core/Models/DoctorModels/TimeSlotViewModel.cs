namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;

public class TimeSlotViewModel
{
    [Display(Name = "Start Time")]
    [DataType(DataType.Time)]
    [Range(typeof(DateTime), "08:00", "15:00", ErrorMessage = "Start time must be between 08:00 and 15:00.")]
    public DateTime StartTime { get; set; }

    [Display(Name = "End Time")]
    [DataType(DataType.Time)]
    public DateTime EndTime => StartTime.AddHours(1);

    public bool IsAvailable { get; set; }
}