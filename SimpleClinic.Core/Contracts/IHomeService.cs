namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models;

public interface IHomeService
{
    public Task<IEnumerable<SpecialityViewModel>> GetAllSpecialitiesWithDoctorsCount();

}
