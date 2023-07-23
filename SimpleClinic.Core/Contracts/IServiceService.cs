using SimpleClinic.Core.Models;

namespace SimpleClinic.Core.Contracts;

public interface IServiceService
{
    public Task<IEnumerable<FirstThreeServicesViewModel>> GetFirstThreeServices();
}
