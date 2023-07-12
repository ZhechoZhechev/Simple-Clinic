using System.ComponentModel.DataAnnotations;

namespace SimpleClinic.Core.Models;

public class PatientRegistrationViewModel : RegisterViewModel
{
    /// <summary>
    /// If patient has health insurance
    /// </summary>
    [Required]
    public bool HasInsurance { get; set; }

    /// <summary>
    /// Date of birth
    /// </summary>
    [Required]
    public DateTime DateOfBirth { get; set; }
}
