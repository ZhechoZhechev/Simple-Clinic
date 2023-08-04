namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Models.PatientModels;

public interface IPrescriptionService
{
    Task SavePrescription(PrescriptionViewModel model, string doctorId);

    Task<List<PatientAllPrescriptionsViewModel>> GetAllPrescriptionsForPatient(string patientId);
}
