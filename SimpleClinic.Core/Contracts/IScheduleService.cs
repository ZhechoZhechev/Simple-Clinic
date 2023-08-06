namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models.DoctorModels;

public interface IScheduleService
{
    Task<bool> AddDoctorScheduleAsync(string doctorId, DateTime day, List<TimeSlotViewModel> timeSlots);

    Task<bool> IfDayScheduleExists(DateTime day, string doctorId);

    Task<bool> IfDayServiceScheduleExists(DateTime day, string serviceId);

    Task<List<DayScheduleViewModel>> CheckSchedule(string doctorId);

    Task<List<DayScheduleViewModel>> CheckServiceSchedule(string serviceId);

    Task<DayScheduleViewModel> GetDoctorScheduleAsync(DateTime day, string doctorId);

    Task<DayScheduleViewModel> GetServiceScheduleAsync(DateTime day, string serviceId);

    Task<List<DateTime>> GetAvailableDates(string doctorId);

    Task<List<DateTime>> GetAvailableDatesService(string serviceId);

    Task<bool> AddServiceScheduleAsync(string serviceId, DateTime day, List<TimeSlotViewModel> timeSlots);
}
