namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;

public class DayScheduleViewModel
{
    public DayScheduleViewModel()
    {
        TimeSlots = new List<TimeSlotViewModel>();
    }

    public string Id { get; set; }

    [Required]
    public DateTime Day { get; set; }

    public List<TimeSlotViewModel> TimeSlots { get; set; }
}
