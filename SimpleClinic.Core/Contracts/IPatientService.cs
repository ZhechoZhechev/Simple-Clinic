namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models.DoctorModels;

public interface IPatientService
{
    Task<List<PatientViewModel>> GetAllPatients();
}
