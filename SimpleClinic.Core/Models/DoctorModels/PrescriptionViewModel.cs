namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;

using static SimpleClinic.Common.Constants.DataConstants.PrescriptionConstants;

public class PrescriptionViewModel
{

    public PrescriptionViewModel()
    {
        this.PrescriptionDate = DateTime.MinValue;
    }

    /// <summary>
    /// Identifier
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Doctors identifier
    /// </summary>
    public string? DoctorId { get; set; }

    /// <summary>
    /// Patients identifier
    /// </summary>
    [Required]
    public string PatientId { get; set; }

    /// <summary>
    /// Medicament identifier
    /// </summary>
    [Required]
    public string MedicamentId { get; set; }

    /// <summary>
    /// Issued
    /// </summary>
    [Required]
    public DateTime PrescriptionDate { get; set; }


    /// <summary>
    /// Instrictions
    /// </summary>
    [StringLength(InstructionsMaxLength, MinimumLength = InstructionsMinLength)]
    public string? Instructions { get; set; }
}
