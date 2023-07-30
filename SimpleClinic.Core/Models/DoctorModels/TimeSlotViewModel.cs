namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;
public class TimeSlotViewModel
{
    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public bool IsAvailable { get; set; }
}