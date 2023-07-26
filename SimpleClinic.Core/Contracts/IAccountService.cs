namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models.PatientModels;

public interface IAccountService
{
    Task<string> GetRoleId(string userId);

    Task<bool> GetIsFormFilled(string userId);

    Task AddNextOfKin(NextOfKinViewModel model, string userId);

    Task AddMedicalHistory(MedicalHistoryViewModel model, string userId);
}
