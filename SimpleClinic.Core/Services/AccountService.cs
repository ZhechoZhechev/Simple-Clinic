namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;


public class AccountService : IAccountService
{
    private readonly SimpleClinicDbContext context;

    public AccountService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    public async Task AddNextOfKin(NextOfKinViewModel model, string userId)
    {
        var nextOfKin = new NextOfKin()
        {
            Name = model.Name,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            PatientId = userId
        };

        await context.NextOfKins.AddAsync(nextOfKin);
        await context.SaveChangesAsync();
    }

    public async Task<bool> GetIsFormFilled(string userId)
    {
        var isFormfilled = await context.Patients
            .Where(x => x.Id == userId)
            .Select(x => x.FormsCompleted)
            .FirstOrDefaultAsync();
         
        return isFormfilled;
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
