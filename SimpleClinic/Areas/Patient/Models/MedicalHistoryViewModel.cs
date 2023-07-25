namespace SimpleClinic.Areas.Patient.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

using static SimpleClinic.Common.Constants.DataConstants.MedicalHistoryConstants;

/// <summary>
/// Medical history view model
/// </summary>
public class MedicalHistoryViewModel
{
    /// <summary>
    /// Surgeries if any
    /// </summary>
    [StringLength(SurgeryDescrMaxLength, MinimumLength =SurgeryDescrMinLength)]
    [DisplayName("If any surgeries, describe them, please")]
    public string? Surgery { get; set; }

    /// <summary>
    /// Medical conditions if any
    /// </summary>
    [StringLength(MedicalConditionsDescrMaxLength, MinimumLength = MedicalConditionsDescrMinLength)]
    [DisplayName("If any medical conditions, describe them, please")]

    public string? MedicalConditions { get; set; }

    /// <summary>
    /// Patient identifier
    /// </summary>
    [Required]
    public string PatientId { get; set; }
}
