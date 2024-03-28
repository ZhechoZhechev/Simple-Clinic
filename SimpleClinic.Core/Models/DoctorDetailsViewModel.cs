namespace SimpleClinic.Core.Models;

public class DoctorDetailsViewModel : FirstThreeDoctorsViewModel
{
    public string ShortBio { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string PricePerHour { get; set; } = null!;

}
