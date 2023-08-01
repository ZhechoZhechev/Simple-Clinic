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



    public async Task SavePrescription(PrescriptionViewModel model, string doctorId)
    {

        var prescription = new Prescription()
        {
            DoctorId = doctorId,
            PatientId = model.PatientId,
            MedicamentId = model.MedicamentId,
            Instructions = model.Instructions,
            PrescriptionDate = model.PrescriptionDate
        };

        await context.Prescriptions.AddAsync(prescription);
        await context.SaveChangesAsync();
    }
}
