using Microsoft.EntityFrameworkCore;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;

namespace SimpleClinic.Core.Services;

public class ScheduleService : IScheduleService
{
    private readonly SimpleClinicDbContext context;

    public ScheduleService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    
}
