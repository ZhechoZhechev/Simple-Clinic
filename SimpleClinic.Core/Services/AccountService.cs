namespace SimpleClinic.Core.Services;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

using SimpleClinic.Core.Contracts;
using SimpleClinic.Core.Models.PatientModels;
using SimpleClinic.Infrastructure;
using SimpleClinic.Infrastructure.Entities;

/// <summary>
/// Services used for creating account controller
/// </summary>
public class AccountService : IAccountService
{
    private readonly SimpleClinicDbContext context;

    public AccountService(SimpleClinicDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Add medical history for patients
    /// </summary>
    /// <param name="model"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task AddMedicalHistory(MedicalHistoryViewModel model, string userId)
    {
        var medicalHistory = new MedicalHistory()
        {
            Surgery = model.Surgery,
            MedicalConditions = model.MedicalConditions,
            PatientId = userId
        };

        var patient = await context.Patients
            .FindAsync(userId);

        if (patient != null) 
        {
            patient.FormsCompleted = true;
            patient.MedicalHistoryId = medicalHistory.Id;
        }
            

        await context.MedicalHistories.AddAsync(medicalHistory);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Adds next of kin information to patients
    /// </summary>
    /// <param name="model"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task AddNextOfKin(NextOfKinViewModel model, string userId)
    {
        var patient = await context.Patients
            .FindAsync(userId);

        var nextOfKin = new NextOfKin()
        {
            Name = model.Name,
            PhoneNumber = model.PhoneNumber,
            Address = model.Address,
            PatientId = userId
        };

        if (patient != null) 
        {
            patient.NextOfKinId = nextOfKin.Id;
        }

        await context.NextOfKins.AddAsync(nextOfKin);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// Checks if form for next of kin and medical history been filled previously
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<bool> GetIsFormFilled(string userId)
    {
        var isFormfilled = await context.Patients
            .Where(x => x.Id == userId)
            .Select(x => x.FormsCompleted)
            .FirstOrDefaultAsync();
         
        return isFormfilled;
    }

    /// <summary>
    /// Gets a rolle ID for a certain user ID
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<string> GetRoleId(string userId)
    {
        var roleId = await context.UserRoles
            .Where(u => u.UserId == userId)
            .Select(r => r.RoleId)
            .FirstOrDefaultAsync();

        return roleId;
    }


}
