namespace SimpleClinic.Core.Models.PatientModels;

using System.ComponentModel;

public class AllDoctorsQueryModel
{
    public AllDoctorsQueryModel()
    {
        this.Doctors = new HashSet<DoctorServiceModel>();
    }

    public int DoctorsPerPage = 3;

    public int CurrentPage { get; set; } = 1;

    public string Specialty { get; set; } = null!;

    [DisplayName("Search by text")]
    public string SearchTerm { get; set; } = null!;

    public int TotalDoctorsCount { get; set; }

    public IEnumerable<string> Specialities { get; set; } = null!;

    public IEnumerable<DoctorServiceModel> Doctors { get; set; }
}
