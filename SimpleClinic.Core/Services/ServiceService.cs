namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure;

public class ServiceService : IServiceService
{
    private readonly SimpleClinicDbContext context;

    public ServiceService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<FirstThreeServicesViewModel>> GetFirstThreeServices()
    {
        var model = await context.Services.Take(3).
            Select(s => new FirstThreeServicesViewModel() 
            {
                Name = s.Name,
                Price = s.Price,
                EquipmentPicture = s.EquipmentPicture
            })
            .ToListAsync();

        return model;
    }
}
