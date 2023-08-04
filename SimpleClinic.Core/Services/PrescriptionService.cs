namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;
using System.Collections.Generic;

public class PrescriptionService : IPrescriptionService
{
    private readonly SimpleClinicDbContext context;

    public PrescriptionService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task<List<PatientAllPrescriptionsViewModel>> GetAllPrescriptionsForPatient(string patientId)
    {
        var model = await context.Prescriptions
            .Where(x => x.PatientId == patientId)
            .Select(p => new PatientAllPrescriptionsViewModel 
            {
                DoctorNames = $"{p.Doctor.FirstName} {p.Doctor.LastName}",
                DoctorSpeciality = p.Doctor.Speciality.Name,
                PrescriptionDate = p.PrescriptionDate,
                Medicament = p.Medicament.Name,
                MedicamentQantity = p.Medicament.QuantityPerDayMilligrams.ToString(),
                Instructions = p.Instructions
            })
            .ToListAsync();

        return model;
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
