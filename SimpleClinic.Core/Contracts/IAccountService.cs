using SimpleClinic.Core.Models;

namespace SimpleClinic.Core.Contracts;

public interface IAccountService
{
    public Task<string> GetRoleId(string userId);

    public Task<IEnumerable<SpecialityViewModel>> GetAllSpecialities();

}
