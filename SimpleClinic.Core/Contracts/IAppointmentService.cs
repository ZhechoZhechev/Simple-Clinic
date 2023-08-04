using SimpleClinic.Core.Models.PatientModels;

namespace SimpleClinic.Core.Contracts;

public interface IAppointmentService
{
    Task CreateAppointment(string timeSlotId, string patientId);

    Task<List<DoctorBookingViewModel>> GetDoctorAppointmentsForPatient(string patientId);

    Task CancelDocAppointment(string id);
}
