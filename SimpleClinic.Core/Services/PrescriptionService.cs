namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;


public class PrescriptionService : IPrescriptionService
{
    private readonly SimpleClinicDbContext context;

    public PrescriptionService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task<Medicament> GetOrCreateMedicament(string medicamentName, int medicamentQuantity)
    {
        var existingMedicament = await context.Medicaments
            .FirstOrDefaultAsync(n => n.Name == medicamentName);

        if (existingMedicament == null)
        {
            var newMedicament = new Medicament()
            {
                Name = medicamentName,
                QuantityPerDayMilligrams = medicamentQuantity
            };

            await context.Medicaments.AddAsync(newMedicament);
            await context.SaveChangesAsync();
        }

        return existingMedicament;
    }

    public async Task SavePrescription(PrescriptionViewModel model)
    {
        var medicament = await GetOrCreateMedicament(model.MedicamentName, model.MedicamentQuantity);

        var prescription = new Prescription()
        {
            DoctorId = model.DoctorId,
            PatientId = model.PatientId,
            PrescriptionDate = model.PrescriptionDate,
            MedicamentId = medicament.Id,
            Instructions = model.Instructions
        };

        await context.Prescriptions.AddAsync(prescription);
        await context.SaveChangesAsync();
    }
}
