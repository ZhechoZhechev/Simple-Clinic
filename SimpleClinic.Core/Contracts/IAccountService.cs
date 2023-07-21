namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure.Entities;


public interface IAccountService
{
    public Task<string> GetRoleId(string userId);

    public Task <IEnumerable<SpecialityViewModel>> GetAllSpecialities();

    public Task<Speciality> AddCustomSpeciality(string customSpecialityNa);

}
