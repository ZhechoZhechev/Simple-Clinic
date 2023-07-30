namespace SimpleClinic.Core.Models.DoctorModels;

public class DayScheduleViewModel
{
    public DayScheduleViewModel()
    {
        TimeSlots = new List<TimeSlotViewModel>();
    }

    public DateTime Day { get; set; }
    public List<TimeSlotViewModel> TimeSlots { get; set; }
}
