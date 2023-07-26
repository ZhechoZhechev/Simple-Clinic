namespace SimpleClinic.Core.Models.PatientModels;

public class DoctorQueryServiceModel
{
    public DoctorQueryServiceModel()
    {
        this.Doctors = new HashSet<DoctorServiceModel>();
    }

    public int TotalDoctorsCount { get; set; }

    public IEnumerable<DoctorServiceModel> Doctors { get; set; }
}
