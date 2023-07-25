namespace SimpleClinic.Infrastructure.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Common.Constants.DataConstants.MedicalHistoryConstants;

/// <summary>
/// Patients medical history
/// </summary>
public class MedicalHistory
{

    /// <summary>
    /// Constructor
    /// </summary>
    public MedicalHistory()
    {
        this.Id = Guid.NewGuid().ToString();
        this.Medicaments = new HashSet<Medicament>();
    }

    /// <summary>
    /// Identifier
    /// </summary>
    [Key]
    public string Id { get; set; }

    /// <summary>
    /// Surgeries if any
    /// </summary>
    [StringLength(SurgeryDescrMaxLength)]
    public string? Surgery { get; set; }

    /// <summary>
    /// Medical conditions if any
    /// </summary>
    [StringLength(MedicalConditionsDescrMaxLength)]
    public string? MedicalConditions { get; set; }

    /// <summary>
    /// Patient identifier
    /// </summary>
    [Required]
    [ForeignKey(nameof(Patient))]
    public string PatientId { get; set; }
    public Patient Patient { get; set; }

    /// <summary>
    /// Medicaments patient is on
    /// </summary>
    public ICollection<Medicament> Medicaments { get; set; }
}