using System.Globalization;

namespace SimpleClinic.Core.Models.DoctorModels;

public class DoctorScheduleViewModel
{
    public DoctorScheduleViewModel()
    {
        Day = DateTime.Today;
        TimeSlots = new List<TimeSlotViewModel>();

        var startTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 8, 0, 0);

        while (startTime.Hour < 16)
        {
            TimeSlots.Add(new TimeSlotViewModel
            {
                StartTime = startTime,
                IsAvailable = false
            });

            startTime = startTime.AddHours(1);
        }
    }

    public DateTime? Day { get; set; }
    public List<TimeSlotViewModel> TimeSlots { get; set; }
}
