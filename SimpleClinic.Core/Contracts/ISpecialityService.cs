namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure.Entities;


public interface ISpecialityService
{
    public Task<IEnumerable<SpecialityViewModel>> GetAllSpecialities();

    public Task<Speciality> AddCustomSpeciality(string customSpecialityNa);

    public Task<IEnumerable<SpecialityViewModel>> GetAllSpecialitiesWithDoctorsCount();

}
