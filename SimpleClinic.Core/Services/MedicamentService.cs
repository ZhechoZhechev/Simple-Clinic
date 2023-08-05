namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Infrastructure;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure.Entities;

public class MedicamentService : IMedicamentService
{
    private readonly SimpleClinicDbContext context;

    public MedicamentService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task AddMedicamentAsync(MedicamentViewModel viewModel)
    {
        var model = new Medicament()
        {
            Id = viewModel.Id,
            Name = viewModel.Name,
            QuantityPerDayMilligrams = viewModel.QuantityPerDayMilligrams
        };

        await context.Medicaments.AddAsync(model);
        await context.SaveChangesAsync();
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
