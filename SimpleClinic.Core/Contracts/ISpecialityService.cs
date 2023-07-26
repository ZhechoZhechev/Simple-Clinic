namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models;
using SimpleClinic.Infrastructure.Entities;


public interface ISpecialityService
{
    Task<IEnumerable<SpecialityViewModel>> GetAllSpecialities();
    Task<IEnumerable<string>> GetAllSpecialityNames();

    Task<Speciality> AddCustomSpeciality(string customSpecialityNa);

    Task<IEnumerable<SpecialityViewModel>> GetAllSpecialitiesWithDoctorsCount();

}
