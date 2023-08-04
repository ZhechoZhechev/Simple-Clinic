namespace SimpleClinic.Core.Models.PatientModels;

using System.ComponentModel.DataAnnotations;

public class PatientAllPrescriptionsViewModel
{
    [Required]
    public string DoctorNames { get; set; }
 
    public string DoctorSpeciality { get; set; }

    public DateTime PrescriptionDate { get; set; }

    public string Medicament{ get; set; }

    public string MedicamentQantity { get; set; }

    public string? Instructions { get; set; }
}
