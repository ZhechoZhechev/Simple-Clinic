namespace SimpleClinic.Core.Models.PatientModels;

public class ServiceQueryServiceModel
{
    public ServiceQueryServiceModel()
    {
        this.Services = new List<ServiceServiceModel>();
    }

    public int TotalServicesCount { get; set; }

    public List<ServiceServiceModel> Services { get; set; }
}
