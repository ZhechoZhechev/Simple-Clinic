namespace SimpleClinic.Core.Contracts;

public interface IAccountService
{
    public Task<string> GetRoleId(string userId);
}
