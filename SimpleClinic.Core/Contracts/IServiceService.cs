using SimpleClinic.Core.Models;

namespace SimpleClinic.Core.Contracts;

public interface IServiceService
{
    Task<IEnumerable<FirstThreeServicesViewModel>> GetFirstThreeServices();
}
