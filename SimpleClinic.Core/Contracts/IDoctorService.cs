namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models;

public interface IDoctorService
{
    public Task<IEnumerable<FirstThreeDoctorsViewModel>> GetFirstThreeDoctors();

    public Task<DoctorDetailsViewModel> DoctorDetails(string id);
}
