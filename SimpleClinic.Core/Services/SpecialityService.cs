namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;

public class SpecialityService : ISpecialityService
{
    private readonly SimpleClinicDbContext context;

    public SpecialityService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task<Speciality> AddCustomSpeciality(string customSpecialityName)
    {
        var newSpec = new Speciality()
        {
            Name = customSpecialityName
        };

        await context.Specialities.AddAsync(newSpec);
        await context.SaveChangesAsync();

        return newSpec;
    }

    public async Task<IEnumerable<SpecialityViewModel>> GetAllSpecialities()
    {
        var specList = await context.Specialities
            .Select(x => new SpecialityViewModel()
            {
                Id = x.Id,
                Name = x.Name
            })
            .ToListAsync();

        return specList;
    }

    public async Task<IEnumerable<SpecialityViewModel>> GetAllSpecialitiesWithDoctorsCount()
    {
        var model = await context.Specialities
            .Select(x => new SpecialityViewModel()
            {
                Name = x.Name,
                DoctorsCount = x.Doctors.Count()
            })
            .ToListAsync();

        return model;
    }
}
