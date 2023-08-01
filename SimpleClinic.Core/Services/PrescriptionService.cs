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



    public async Task SavePrescription(PrescriptionViewModel model)
    {

        var prescription = new Prescription()
        {
            DoctorId = model.DoctorId,
            PatientId = model.PatientId,
            PrescriptionDate = model.PrescriptionDate,
            MedicamentId = model.MedicamentId,
            Instructions = model.Instructions
        };

        await context.Prescriptions.AddAsync(prescription);
        await context.SaveChangesAsync();
    }
}
