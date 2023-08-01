namespace SimpleClinic.Core.Services;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure;
using SimpleClinic.Core.Models.DoctorModels;
using Microsoft.EntityFrameworkCore;

public class MedicamentService : IMedicamentService
{
    private readonly SimpleClinicDbContext context;

    public MedicamentService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task<List<MedicamentViewModel>> GetAllMedicaments(string searchTerm)
    {

        var patientsQuery = context.Medicaments.AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            patientsQuery = patientsQuery
             .Where(t =>
              t.Name.Contains(searchTerm.ToLower()));
        }

        var patients = await patientsQuery
            .Select(p => new MedicamentViewModel()
            {
                Id = p.Id,
                Name = p.Name,
                QuantityPerDayMilligrams = p.QuantityPerDayMilligrams,
            })
            .ToListAsync();


        return patients;
    }
}
