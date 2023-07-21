namespace SimpleClinic.Core.Services;

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure;

public class HomeService : IHomeService
{
    private readonly SimpleClinicDbContext context;

    public HomeService(SimpleClinicDbContext context)
    {
        this.context = context;
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
