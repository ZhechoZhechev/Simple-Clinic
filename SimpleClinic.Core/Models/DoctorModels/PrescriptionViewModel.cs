namespace SimpleClinic.Core.Models.DoctorModels;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using SimpleClinic.Infrastructure.Entities;
using static SimpleClinic.Common.Constants.DataConstants.PrescriptionConstants;

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
    /// Prescribed medicament
    /// </summary>
    [Required]
    public string MedicamenName { get; set; }

    /// <summary>
    /// Instrictions
    /// </summary>
    [StringLength(InstructionsMaxLength, MinimumLength = InstructionsMinLength)]
    public string? Instructions { get; set; }
}
