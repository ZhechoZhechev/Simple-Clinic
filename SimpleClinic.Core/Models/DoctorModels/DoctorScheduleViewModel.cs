namespace SimpleClinic.Core.Models.DoctorModels;

public class DoctorScheduleViewModel
{
    public DoctorScheduleViewModel()
    {
        this.TimeSlots = new List<TimeSlotViewModel>();
    }

    public DateTime? Day { get; set; }
    public List<TimeSlotViewModel> TimeSlots { get; set; }
}
