namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure;


public class PatientService : IPatientService
{
    private readonly SimpleClinicDbContext context;

    public PatientService(SimpleClinicDbContext context)
    {
        this.context = context;
    }
    public async Task<List<PatientViewModel>> GetAllPatients(string searchTerm)
    {

        var patientsQuery = context.Patients.AsQueryable();

        if(!string.IsNullOrWhiteSpace(searchTerm)) 
        {
            patientsQuery = patientsQuery
             .Where(t => 
              t.FirstName.Contains(searchTerm.ToLower()) ||
              t.LastName.Contains(searchTerm.ToLower()));
        }

        var patients = await patientsQuery
            .Select(p => new PatientViewModel() 
            {
                Id = p.Id,
                FullName = $"{p.FirstName} {p.LastName}"
            })
            .ToListAsync();


        return patients;
    }
}
