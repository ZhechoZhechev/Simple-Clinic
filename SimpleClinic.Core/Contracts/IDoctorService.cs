namespace SimpleClinic.Core.Contracts;

using SimpleClinic.Core.Models;
/// <summary>
/// DoctorService interface
/// </summary>
public interface IDoctorService
{
    public Task<IEnumerable<FirstThreeDoctorsViewModel>> GetFirstThreeDoctors();

    public Task<DoctorDetailsViewModel> DoctorDetails(string id);

    public Task<bool> DoctorExistsById(string id);
}
