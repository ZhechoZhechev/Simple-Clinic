namespace SimpleClinic.Core.Models.PatientModels;

public class AllServicesPaginationModel
{
    public AllServicesPaginationModel()
    {
        this.Services = new HashSet<ServiceServiceModel>();
    }

    public int ServicesPerPage = 3;

    public int CurrentPage { get; set; } = 1;

    public int TotalServicesCount { get; set; }

    public IEnumerable<ServiceServiceModel> Services { get; set; }
}

