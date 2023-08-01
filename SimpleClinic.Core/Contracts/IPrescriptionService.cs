namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure.Entities;

public interface IPrescriptionService
{
    Task<Medicament> GetOrCreateMedicament(string medicamentName, int medicamentQuantity);

    Task SavePrescription(PrescriptionViewModel model);
}
