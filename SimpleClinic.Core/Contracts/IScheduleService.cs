namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models.DoctorModels;

public interface IScheduleService
{
    Task<bool> AddOrUpdateDoctorScheduleAsync(string doctorId, DateTime day, List<TimeSlotViewModel> timeSlots);

    Task<bool> IfDayScheduleExists(DateTime day, string doctorId);

    Task<List<DayScheduleViewModel>> CheckSchedule(string doctorId);
}
