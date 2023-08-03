namespace SimpleClinic.Core.Contracts;

public interface IAppointmentService
{
    Task CreateAppointment(string timeSlotId, string patientId);
}
