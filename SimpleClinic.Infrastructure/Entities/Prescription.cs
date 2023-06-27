namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.Constants.DataConstants.PrescriptionConstants;

/// <summary>
/// Perscription
/// </summary>
public class Prescription
{
    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// Doctors identifier
    /// </summary>
    [Required]
    [ForeignKey(nameof(Doctor))]
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }

    /// <summary>
    /// Patients identifier
    /// </summary>
    [Required]
    [ForeignKey(nameof(Patient))]
    public int PatientId { get; set; }
    public Patient Patient { get; set; }

    /// <summary>
    /// Issued
    /// </summary>
    [Required]
    public DateTime PrescriptionDate { get; set; }

    /// <summary>
    /// Prescribed medicament
    /// </summary>
    [Required]
    [ForeignKey(nameof(Medicament))]
    public int MedicamentId { get; set; }
    public Medicament Medicament { get; set; }

    /// <summary>
    /// Instrictions
    /// </summary>
    [StringLength(InstructionsMaxLength)]
    public string? Instructions { get; set; }
}