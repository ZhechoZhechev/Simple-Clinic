using Microsoft.EntityFrameworkCore;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;

namespace SimpleClinic.Core.Services;

public class PatientService : IPatientService
{
    private readonly SimpleClinicDbContext context;

    public PatientService(SimpleClinicDbContext context)
    {
        this.context = context;
    }
    public async Task<List<PatientViewModel>> GetAllPatients()
    {
        var patients = await context.Patients
            .Select(p => new PatientViewModel() 
            {
                Id = p.Id,
                FullName = $"{p.FirstName} {p.LastName}"

            })
            .ToListAsync();

        return patients;
    }
}
