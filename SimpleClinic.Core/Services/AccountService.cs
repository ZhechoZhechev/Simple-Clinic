namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;
using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AccountService : IAccountService
{
    private readonly SimpleClinicDbContext context;

    public AccountService(SimpleClinicDbContext context)
    {
        this.context = context;
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

    public async Task<string> GetRoleId(string userId)
    {
        var roleId = await context.UserRoles
            .Where(r => r.UserId == userId)
            .Select(r => r.RoleId)
            .FirstOrDefaultAsync();

        return roleId;
    }
}
