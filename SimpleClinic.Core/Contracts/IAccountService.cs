namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models.PatientModels;

public interface IAccountService
{
    public Task<string> GetRoleId(string userId);

    public Task<bool> GetIsFormFilled(string userId);

    public Task AddNextOfKin(NextOfKinViewModel model, string userId);
}
