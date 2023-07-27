namespace SimpleClinic.Core.Models.PatientModels;

public class ServiceQueryServiceModel
{
    public ServiceQueryServiceModel()
    {
        this.Services = new HashSet<ServiceServiceModel>();
    }

    public int TotalServicesCount { get; set; }

    public IEnumerable<ServiceServiceModel> Services { get; set; }
}
