namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations;

using static SimpleClinic.Common.Constants.DataConstants.PrescriptionConstants;
using static SimpleClinic.Common.Constants.DataConstants.MedicamentConstants;

public class PrescriptionViewModel
{
    /// <summary>
    /// Identifier
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Doctors identifier
    /// </summary>
    [Required]
    public string DoctorId { get; set; }

    /// <summary>
    /// Patients identifier
    /// </summary>
    [Required]
    public string PatientId { get; set; }

    /// <summary>
    /// Issued
    /// </summary>
    [Required]
    public DateTime PrescriptionDate { get; set; }

    /// <summary>
    /// Medicament name
    /// </summary>
    [Required]
    [StringLength(NameMaxLength, MinimumLength = NameMinLength)]
    public string MedicamentName { get; set; }

    /// <summary>
    /// Quantity per day in milligrams
    /// </summary>
    [Required]
    public int MedicamentQuantity { get; set; }

    /// <summary>
    /// Instrictions
    /// </summary>
    [StringLength(InstructionsMaxLength, MinimumLength = InstructionsMinLength)]
    public string? Instructions { get; set; }
}
