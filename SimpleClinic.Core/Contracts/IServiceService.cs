using SimpleClinic.Core.Models;
using SimpleClinic.Core.Models.DoctorModels;
using SimpleClinic.Core.Models.PatientModels;

namespace SimpleClinic.Core.Contracts;

public interface IServiceService
{
    Task<List<FirstThreeServicesViewModel>> GetFirstThreeServices();

    Task<ServiceQueryServiceModel> All(int currentPage = 1, int servicesPerPage = 1);

    Task<List<AllServicesForScheduleViewModel>> GetAllServicesForSchedule();

    Task AddServiceAsync(ServiceViewModel service);

    Task<ServiceViewModel> GetServiceForEditing(string id);
}
