namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;

public class DayScheduleViewModel
{
    [Display(Name = "Day")]
    [DataType(DataType.Date)]
    public DateTime Day { get; set; }

    [Display(Name = "Time Slots")]
    public List<TimeSlotViewModel> TimeSlots { get; set; }
}