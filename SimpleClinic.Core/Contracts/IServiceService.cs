using SimpleClinic.Core.Models;
using SimpleClinic.Core.Models.PatientModels;

namespace SimpleClinic.Core.Contracts;

public interface IServiceService
{
    Task<IEnumerable<FirstThreeServicesViewModel>> GetFirstThreeServices();

    Task<ServiceQueryServiceModel> All(int currentPage = 1, int servicesPerPage = 1);
}
