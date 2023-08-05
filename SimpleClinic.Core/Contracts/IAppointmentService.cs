namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Models.PatientModels;


public interface IAppointmentService
{
    Task CreateAppointment(string timeSlotId, string patientId);

    Task<List<DoctorBookingViewModel>> GetDoctorAppointmentsForPatient(string patientId);

    Task<List<PatientAppointmentViewModel>> GetPatientAppointmentsForDoctor(string doctorId);

    Task CancelDocAppointment(string id);

    Task CancelPatientAppointment(string id);

}
