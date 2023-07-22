namespace SimpleClinic.Core.Models;

public class FirstThreeDoctorsViewModel
{
    public string Id { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; } = null!;

    public string ProfilePictureFilename { get; set; }

    public string Speciality { get; set; }
}
