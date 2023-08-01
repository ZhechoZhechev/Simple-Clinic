namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models.DoctorModels;

public interface IPrescriptionService
{
    Task SavePrescription(PrescriptionViewModel model, string doctorId);
}
